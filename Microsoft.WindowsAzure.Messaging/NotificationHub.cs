// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.NotificationHub
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using Microsoft.WindowsAzure.Messaging.Http.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.Messaging
{
  public sealed class NotificationHub : Entity
  {
    internal const string DefaultListenSasRuleName = "DefaultListenSharedAccessSignature";
    internal const string DefaultFullSasRuleName = "DefaultFullSharedAccessSignature";
    private readonly RegistrationManager registrationManager;

    public NotificationHub(string notificationHubPath, string connectionString)
      : this(notificationHubPath, connectionString, new IHttpHandler[0])
    {
    }

    internal NotificationHub(
      string notificationHubPath,
      string connectionString,
      params IHttpHandler[] additionalHandlers)
      : base(connectionString)
    {
      if (string.IsNullOrWhiteSpace(notificationHubPath))
        throw new ArgumentNullException(nameof (notificationHubPath));
      if (string.IsNullOrWhiteSpace(connectionString))
        throw new ArgumentNullException(nameof (connectionString));
      this.Path = notificationHubPath;
      this.registrationManager = new RegistrationManager(this.Connection, new LocalStorageManager(notificationHubPath), additionalHandlers);
    }

    public string Path { get; private set; }

    public async Task<Registration> RegisterNativeAsync(string channelUri) => await this.RegisterNativeAsync(channelUri, (IEnumerable<string>) null);

    public async Task<Registration> RegisterNativeAsync(string channelUri, IEnumerable<string> tags)
    {
      if (string.IsNullOrWhiteSpace(channelUri))
        throw new ArgumentNullException(nameof (channelUri));
      Registration registration = new Registration(this.Path, channelUri, tags);
      return await this.registrationManager.RegisterAsync<Registration>(registration);
    }

    public async Task<TemplateRegistration> RegisterTemplateAsync(
      string channelUri,
      string xmlTemplate,
      string templateName)
    {
      return await this.RegisterTemplateAsync(channelUri, xmlTemplate, templateName, (IEnumerable<string>) null);
    }

    public async Task<TemplateRegistration> RegisterTemplateAsync(
      string channelUri,
      string xmlTemplate,
      string templateName,
      IEnumerable<string> tags)
    {
      if (string.IsNullOrWhiteSpace(channelUri))
        throw new ArgumentNullException(nameof (channelUri));
      if (string.IsNullOrWhiteSpace(xmlTemplate))
        throw new ArgumentNullException(nameof (xmlTemplate));
      if (string.IsNullOrWhiteSpace(templateName))
        throw new ArgumentNullException(nameof (templateName));
      TemplateRegistration registration = new TemplateRegistration(this.Path, channelUri, xmlTemplate, templateName, tags, (IDictionary<string, string>) null);
      return await this.registrationManager.RegisterAsync<TemplateRegistration>(registration);
    }

    public async Task UnregisterNativeAsync() => await this.UnregisterTemplateAsync("$Default");

    public async Task UnregisterTemplateAsync(string templateName) => await this.registrationManager.DeleteRegistrationAsync(this.Path, templateName);

    public async Task UnregisterAllAsync(string channelUri) => await this.registrationManager.DeleteRegistrationsForChannelAsync(this.Path, channelUri);

    public async Task<Registration> RegisterAsync(Registration registration)
    {
      if (registration == null)
        throw new ArgumentNullException(nameof (registration));
      if (string.IsNullOrEmpty(registration.ChannelUri))
        throw new ArgumentNullException("ChannelUri");
      if (!string.IsNullOrEmpty(registration.NotificationHubPath) && registration.NotificationHubPath != this.Path)
        throw new ArgumentException("notificationHubPath");
      registration.NotificationHubPath = this.Path;
      return await this.registrationManager.RegisterAsync<Registration>(registration);
    }

    public async Task UnregisterAsync(Registration registration)
    {
      if (registration == null)
        throw new ArgumentNullException(nameof (registration));
      await this.registrationManager.DeleteRegistrationAsync(this.Path, registration.Name);
    }

    protected override void OnInternalDispose()
    {
      if (this.registrationManager == null)
        return;
      this.registrationManager.Dispose();
    }
  }
}
