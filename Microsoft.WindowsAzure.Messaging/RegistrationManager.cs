// Microsoft.WindowsAzure.Messaging.RegistrationManager

using Microsoft.WindowsAzure.Messaging.Http;
using Microsoft.WindowsAzure.Messaging.Http.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.Messaging
{
  internal class RegistrationManager : IDisposable
  {
    private readonly ServiceBusClient client;
    private readonly LocalStorageManager localStorageManager;

    public RegistrationManager(
      string connectionString,
      LocalStorageManager storagManager,
      params IHttpHandler[] additionalHandlers)
    {
      this.client = new ServiceBusClient(connectionString, additionalHandlers);
      this.localStorageManager = storagManager;
    }

    public async Task<T> RegisterAsync<T>(T registration) where T : Registration
    {
      if (this.localStorageManager.IsRefreshNeeded)
      {
        string refreshChannelUri = string.IsNullOrEmpty(this.localStorageManager.ChannelUri) ? registration.ChannelUri : this.localStorageManager.ChannelUri;
        string continuationToken = (string) null;
        do
        {
          ListWithContinuation<Registration> registrations = await this.GetRegistrationsForChannelAsync(registration.NotificationHubPath, refreshChannelUri, continuationToken: continuationToken);
          continuationToken = registrations.ContinuationToken;
        }
        while (continuationToken != null);
        this.localStorageManager.RefreshFinished(refreshChannelUri);
      }
      StoredRegistrationEntry cached = this.localStorageManager.GetRegistration(registration.Name);
      if (cached != null)
      {
        registration.RegistrationId = cached.RegistrationId;
      }
      else
      {
        Registration registrationIdAsync1 = await this.CreateRegistrationIdAsync((Registration) registration);
      }
      try
      {
        T createdReg = await this.UpsertRegistration<T>(registration);
        return createdReg;
      }
      catch (RegistrationGoneException ex)
      {
         Debug.WriteLine(ex.Message);
      }
      Registration registrationIdAsync2 = await this.CreateRegistrationIdAsync((Registration) registration);
      return await this.UpsertRegistration<T>(registration);
    }

    public async Task<T> UpdateRegistrationAsync<T>(T registration) where T : Registration
    {
      StoredRegistrationEntry cached = this.localStorageManager.GetRegistration(registration.Name);
      if (cached == null)
        throw new RegistrationNotFoundException(registration.Name);
      T obj;
      try
      {
        registration.RegistrationId = cached.RegistrationId;
        registration.ETag = "*";
        Registration updated = (Registration) await this.client.UpdateRegistrationAsync<T>(registration);
        updated.NotificationHubPath = registration.NotificationHubPath;
        this.localStorageManager.UpdateRegistration<Registration>(registration.Name, ref updated);
        obj = (T) updated;
      }
      catch (Exception ex)
      {
        throw HttpUtilities.ConvertToRegistrationException(ex);
      }
      return obj;
    }

    public async Task<Registration> GetRegistrationAsync(
      string notificationHubPath,
      string registrationName)
    {
      if (string.IsNullOrWhiteSpace(notificationHubPath))
        throw new ArgumentNullException(nameof (notificationHubPath));
      StoredRegistrationEntry cached = !string.IsNullOrWhiteSpace(registrationName) ? this.localStorageManager.GetRegistration(registrationName) : throw new ArgumentNullException(nameof (registrationName));
      if (cached == null)
        return (Registration) null;
      try
      {
        Registration registation = await WindowsRuntimeSystemExtensions.AsTask<Registration>(this.client.GetRegistrationAsync(notificationHubPath, cached.RegistrationId));
        if (registation != null)
        {
          registation.NotificationHubPath = notificationHubPath;
          this.localStorageManager.UpdateRegistration<Registration>(registrationName, ref registation);
        }
        else
          this.localStorageManager.DeleteRegistration(registrationName);
        return registation;
      }
      catch (Exception ex)
      {
        throw HttpUtilities.ConvertToRegistrationException(ex);
      }
    }

    public async Task<ListWithContinuation<Registration>> GetRegistrationsForChannelAsync(
      string notificationHubPath,
      string channelUri,
      int top = 0,
      string continuationToken = null)
    {
      ListWithContinuation<Registration> registrationsForChannelAsync;
      try
      {
        ListWithContinuation<Registration> registrations = await this.client.ListRegistrationsAsync(notificationHubPath, channelUri, top, continuationToken);
        for (int index = 0; index < registrations.Count; ++index)
        {
          registrations[index].NotificationHubPath = notificationHubPath;
          this.localStorageManager.UpdateRegistration(registrations[index]);
        }
        registrationsForChannelAsync = registrations;
      }
      catch (Exception ex)
      {
        throw HttpUtilities.ConvertToRegistrationException(ex);
      }
      return registrationsForChannelAsync;
    }

    public async Task DeleteRegistrationAsync(string notificationHubPath, string registrationName)
    {
      StoredRegistrationEntry cached = !string.IsNullOrWhiteSpace(registrationName) ? this.localStorageManager.GetRegistration(registrationName) : throw new ArgumentNullException(nameof (registrationName));
      if (cached == null)
        return;
      try
      {
        await this.client.DeleteRegistrationAsync(notificationHubPath, cached.RegistrationId, "*");
        this.localStorageManager.DeleteRegistration(registrationName);
      }
      catch (Exception ex)
      {
        Exception registrationException = HttpUtilities.ConvertToRegistrationException(ex);
        if (!(registrationException is RegistrationNotFoundException))
          throw registrationException;
      }
    }

    public async Task DeleteRegistrationsForChannelAsync(
      string notificationHubPath,
      string channelUri)
    {
      string continuationToken = (string) null;
      do
      {
        ListWithContinuation<Registration> registrations = await this.GetRegistrationsForChannelAsync(notificationHubPath, channelUri, continuationToken: continuationToken);
        continuationToken = registrations.ContinuationToken;
        foreach (Registration item in (List<Registration>) registrations)
        {
          await this.client.DeleteRegistrationAsync(notificationHubPath, item.RegistrationId, "*");
          this.localStorageManager.DeleteRegistration(item);
        }
      }
      while (continuationToken != null);
    }

    public async Task UpdateChannelUriAsync(string notificationHubPath, string newChannelUri)
    {
      try
      {
        string channelUri = this.localStorageManager.ChannelUri;
        if (string.IsNullOrEmpty(channelUri))
        {
          this.localStorageManager.DeleteAllRegistrations();
          string continuationToken = (string) null;
          do
          {
            ListWithContinuation<Registration> registrations = await this.client.ListRegistrationsAsync(notificationHubPath, newChannelUri, continuationToken: continuationToken);
            continuationToken = registrations.ContinuationToken;
            foreach (Registration registration in (List<Registration>) registrations)
              this.localStorageManager.UpdateRegistration(registration);
          }
          while (continuationToken != null);
        }
        else if (!channelUri.Equals(newChannelUri))
        {
          this.localStorageManager.DeleteAllRegistrations();
          IEnumerable<Registration> updated = await this.client.UpdateChannelUriAsync(notificationHubPath, channelUri, newChannelUri);
          foreach (Registration registration in updated)
            this.localStorageManager.UpdateRegistration(registration);
        }
        this.localStorageManager.ChannelUri = newChannelUri;
      }
      catch (Exception ex)
      {
        throw HttpUtilities.ConvertToRegistrationException(ex);
      }
    }

    public void Dispose()
    {
      if (this.client != null)
        this.client.Dispose();
      if (this.localStorageManager == null)
        return;
      this.localStorageManager.Dispose();
    }

    private async Task<Registration> CreateRegistrationIdAsync(Registration registration)
    {
      Registration registrationIdAsync;
      try
      {
        registration.ETag = "*";
        registration.RegistrationId = await this.client.CreateRegistrationIdAsync(registration.NotificationHubPath);
        this.localStorageManager.UpdateRegistration<Registration>(registration.Name, ref registration);
        registrationIdAsync = registration;
      }
      catch (Exception ex)
      {
        Exception innerException = ex is AggregateException ? ((Exception) ((AggregateException) ex).Flatten()).InnerException : ex;
        if (innerException is WindowsAzureException windowsAzureException && windowsAzureException.ErrorCode == 404)
          throw new NotificationHubNotFoundException(innerException.Message, innerException);
        throw HttpUtilities.ConvertToRegistrationException(ex);
      }
      return registrationIdAsync;
    }

    private async Task<T> UpsertRegistration<T>(T registration) where T : Registration
    {
      T obj;
      try
      {
        Registration created = (Registration) await this.client.CreateOrUpdateRegistrationAsync<T>(registration);
        created.NotificationHubPath = registration.NotificationHubPath;
        this.localStorageManager.UpdateRegistration<Registration>(registration.Name, ref created);
        obj = (T) created;
      }
      catch (Exception ex)
      {
        Exception exception = ex is AggregateException ? ((Exception) ((AggregateException) ex).Flatten()).InnerException : ex;
        if (exception is WindowsAzureException windowsAzureException && windowsAzureException.ErrorCode == 404)
          throw new NotificationHubNotFoundException(exception.Message, exception);
        throw HttpUtilities.ConvertToRegistrationException(exception);
      }
      return obj;
    }
  }
}
