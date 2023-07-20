// Decompiled with JetBrains decompiler
// Type: WalletPass.BackgroundTasks
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace WalletPass
{
  public class BackgroundTasks
  {
    public async void RegisterRelevantDateTask()
    {
      bool taskRegistered = false;
      string taskName = "WPRelevantDateTask";
      foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> allTask in (IEnumerable<KeyValuePair<Guid, IBackgroundTaskRegistration>>) BackgroundTaskRegistration.AllTasks)
      {
        if (allTask.Value.Name == taskName)
        {
          taskRegistered = true;
          break;
        }
      }
      if (taskRegistered)
        return;
      BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
      BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
      builder.put_Name(taskName);
      builder.put_TaskEntryPoint("BackgroundTask.RelevantDateBackgroundTask");
      builder.SetTrigger((IBackgroundTrigger) new TimeTrigger(15U, false));
      builder.Register();
    }

    public void UnRegisterRelevantDateTask()
    {
      KeyValuePair<Guid, IBackgroundTaskRegistration> keyValuePair = BackgroundTaskRegistration.AllTasks.FirstOrDefault<KeyValuePair<Guid, IBackgroundTaskRegistration>>((Func<KeyValuePair<Guid, IBackgroundTaskRegistration>, bool>) (kvp => kvp.Value.Name == "WPRelevantDateTask"));
      if (keyValuePair.Value == null)
        return;
      keyValuePair.Value.Unregister(true);
    }

    public async void RegisterLocationTask()
    {
      bool taskRegistered = false;
      string taskName = "WPLocationTask";
      foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> allTask in (IEnumerable<KeyValuePair<Guid, IBackgroundTaskRegistration>>) BackgroundTaskRegistration.AllTasks)
      {
        if (allTask.Value.Name == taskName)
        {
          taskRegistered = true;
          break;
        }
      }
      if (taskRegistered)
        return;
      BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
      BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
      builder.put_Name(taskName);
      builder.put_TaskEntryPoint("BackgroundTask.LocationBackgroundTask");
      builder.SetTrigger((IBackgroundTrigger) new LocationTrigger((LocationTriggerType) 0));
      builder.Register();
    }

    public void UnRegisterLocationTask()
    {
      KeyValuePair<Guid, IBackgroundTaskRegistration> keyValuePair = BackgroundTaskRegistration.AllTasks.FirstOrDefault<KeyValuePair<Guid, IBackgroundTaskRegistration>>((Func<KeyValuePair<Guid, IBackgroundTaskRegistration>, bool>) (kvp => kvp.Value.Name == "WPLocationTask"));
      if (keyValuePair.Value == null)
        return;
      keyValuePair.Value.Unregister(true);
    }

    public void RegisterPushNotificationTask()
    {
      bool flag = false;
      string str = "WPPushNotificationTask";
      foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> allTask in (IEnumerable<KeyValuePair<Guid, IBackgroundTaskRegistration>>) BackgroundTaskRegistration.AllTasks)
      {
        if (allTask.Value.Name == str)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      BackgroundTaskBuilder backgroundTaskBuilder = new BackgroundTaskBuilder();
      backgroundTaskBuilder.put_Name(str);
      backgroundTaskBuilder.put_TaskEntryPoint("BackgroundTask.PushNotificationBackgroundTask");
      backgroundTaskBuilder.SetTrigger((IBackgroundTrigger) new PushNotificationTrigger());
      backgroundTaskBuilder.Register();
    }

    public void UnRegisterPushNotificationTask()
    {
      KeyValuePair<Guid, IBackgroundTaskRegistration> keyValuePair = BackgroundTaskRegistration.AllTasks.FirstOrDefault<KeyValuePair<Guid, IBackgroundTaskRegistration>>((Func<KeyValuePair<Guid, IBackgroundTaskRegistration>, bool>) (kvp => kvp.Value.Name == "WPPushNotificationTask"));
      if (keyValuePair.Value == null)
        return;
      keyValuePair.Value.Unregister(true);
    }

    public async void RegisterMSWalletTask()
    {
      bool taskRegistered = false;
      string taskName = "WPMSWalletTask";
      foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> allTask in (IEnumerable<KeyValuePair<Guid, IBackgroundTaskRegistration>>) BackgroundTaskRegistration.AllTasks)
      {
        if (allTask.Value.Name == taskName)
        {
          taskRegistered = true;
          break;
        }
      }
      if (taskRegistered)
        return;
      BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
      BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
      builder.put_Name(taskName);
      builder.put_TaskEntryPoint("BackgroundTask.MSWalletBackgroundTask");
      builder.SetTrigger((IBackgroundTrigger) new TimeTrigger(15U, false));
      builder.Register();
    }

    public void UnRegisterMSWalletTask()
    {
      KeyValuePair<Guid, IBackgroundTaskRegistration> keyValuePair = BackgroundTaskRegistration.AllTasks.FirstOrDefault<KeyValuePair<Guid, IBackgroundTaskRegistration>>((Func<KeyValuePair<Guid, IBackgroundTaskRegistration>, bool>) (kvp => kvp.Value.Name == "WPMSWalletTask"));
      if (keyValuePair.Value == null)
        return;
      keyValuePair.Value.Unregister(true);
    }
  }
}
