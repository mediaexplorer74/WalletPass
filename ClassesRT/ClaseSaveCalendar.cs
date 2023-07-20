// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseSaveCalendar
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Storage;

namespace Wallet_Pass
{
  internal class ClaseSaveCalendar
  {
    private AppointmentCalendar currentAppCalendar;
    private AppointmentStore appointmentStore;

    public async Task CreateAppointmentCalendar()
    {
      this.appointmentStore = await AppointmentManager.RequestStoreAsync((AppointmentStoreAccessType) 0);
      if (!((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values).ContainsKey("FirstRun"))
      {
        await this.CheckForAndCreateAppointmentCalendars();
        this.appointmentStore.ChangeTracker.Enable();
        this.appointmentStore.ChangeTracker.Reset();
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["FirstRun"] = (object) false;
      }
      else
        await this.CheckForAndCreateAppointmentCalendars();
    }

    public async Task CheckForAndCreateAppointmentCalendars()
    {
      try
      {
        IReadOnlyList<AppointmentCalendar> appCalendars = await this.appointmentStore.FindAppointmentCalendarsAsync((FindAppointmentCalendarsOptions) 1);
        AppointmentCalendar appCalendar = (AppointmentCalendar) null;
        AppointmentCalendar appointmentCalendarAsync;
        if (appCalendars.Count != 0)
          appointmentCalendarAsync = appCalendars[0];
        else
          appointmentCalendarAsync = await this.appointmentStore.CreateAppointmentCalendarAsync("Wallet Pass Calendar");
        appCalendar = appointmentCalendarAsync;
        appCalendar.put_OtherAppReadAccess((AppointmentCalendarOtherAppReadAccess) 2);
        appCalendar.put_OtherAppWriteAccess((AppointmentCalendarOtherAppWriteAccess) 1);
        appCalendar.put_SummaryCardView((AppointmentSummaryCardView) 1);
        await appCalendar.SaveAsync();
        this.currentAppCalendar = appCalendar;
      }
      catch (Exception ex)
      {
        this.currentAppCalendar = (AppointmentCalendar) null;
      }
    }

    public async Task<bool> saveAppointment(ClasePass item, AppointmentCalendar currentCalendar)
    {
      Appointment appointment = new Appointment();
      bool isAppointmentValid = true;
      bool isRelevantDate = true;
      AppSettings settings = new AppSettings();
      ClaseReminderItems reminders = new ClaseReminderItems();
      try
      {
        if (item.type == "boardingPass")
        {
          if (item.relevantDate == new DateTime(1, 1, 1))
          {
            appointment.put_StartTime((DateTimeOffset) DateTime.Today);
            isRelevantDate = false;
          }
          else
            appointment.put_StartTime((DateTimeOffset) item.relevantDate);
          if (item.expirationDate == new DateTime(1, 1, 1))
            appointment.put_Duration(new TimeSpan(0, 0, 0));
          else
            appointment.put_Duration(item.expirationDate - item.relevantDate);
          appointment.put_Location(item.PrimaryFields[0].Label + " -> " + item.PrimaryFields[1].Label);
        }
        else
        {
          if (item.relevantDate == new DateTime(1, 1, 1))
          {
            appointment.put_StartTime((DateTimeOffset) DateTime.Today);
            isRelevantDate = false;
          }
          else
            appointment.put_StartTime((DateTimeOffset) item.relevantDate);
          if (item.expirationDate == new DateTime(1, 1, 1))
            appointment.put_Duration(new TimeSpan(0, 0, 0));
          else
            appointment.put_Duration(item.expirationDate - item.relevantDate);
        }
        appointment.put_Subject("");
        switch (item.type)
        {
          case "boardingPass":
            switch (item.transitType)
            {
            }
            break;
          case "eventTicket":
            if (item.PrimaryFields.Count > 0)
            {
              appointment.put_Subject(item.PrimaryFields[0].Value);
              break;
            }
            break;
          case "generic":
            if (item.PrimaryFields.Count > 0)
            {
              appointment.put_Subject(item.PrimaryFields[0].Value);
              break;
            }
            break;
        }
        appointment.put_Details(item.organizationName);
        appointment.put_AllDay(false);
        appointment.put_Reminder(new TimeSpan?(new TimeSpan(1, 0, 0, 0)));
        appointment.put_AllowNewTimeProposal(true);
        switch (settings.calendarState)
        {
          case 0:
            appointment.put_BusyStatus((AppointmentBusyStatus) 2);
            break;
          case 1:
            appointment.put_BusyStatus((AppointmentBusyStatus) 1);
            break;
          case 2:
            appointment.put_BusyStatus((AppointmentBusyStatus) 0);
            break;
          case 3:
            appointment.put_BusyStatus((AppointmentBusyStatus) 3);
            break;
          default:
            appointment.put_BusyStatus((AppointmentBusyStatus) 0);
            break;
        }
        if (!settings.calendarAutomaticSave)
        {
          item.idAppointment = await AppointmentManager.ShowEditNewAppointmentAsync(appointment);
          if (string.IsNullOrEmpty(item.idAppointment))
            return false;
        }
        else
        {
          appointment.put_Reminder(new TimeSpan?(settings.calendarReminder == 0 ? TimeSpan.Zero : reminders.listPickerCalendarItemTimeSpan(settings.calendarReminder)));
          if (isRelevantDate)
          {
            await currentCalendar.SaveAppointmentAsync(appointment);
            item.idAppointment = appointment.LocalId;
          }
          else
          {
            item.idAppointment = await AppointmentManager.ShowEditNewAppointmentAsync(appointment);
            if (string.IsNullOrEmpty(item.idAppointment))
              return false;
          }
        }
      }
      catch
      {
        isAppointmentValid = false;
      }
      return isAppointmentValid;
    }

    public async Task<bool> editAppointment(string appointmentID, DateTime newDate)
    {
      await this.CreateAppointmentCalendar();
      if (this.currentAppCalendar == null)
        return false;
      try
      {
        Appointment targetAppt = await this.currentAppCalendar.GetAppointmentAsync(appointmentID);
        if (targetAppt != null && newDate != new DateTime(1, 1, 1))
        {
          targetAppt.put_StartTime((DateTimeOffset) newDate);
          await this.currentAppCalendar.SaveAppointmentAsync(targetAppt);
        }
        return targetAppt != null;
      }
      catch
      {
        return false;
      }
    }

    public async Task<bool> existAppointment(
      string appointmentID,
      AppointmentCalendar currentCalendar)
    {
      try
      {
        Appointment targetAppt = await currentCalendar.GetAppointmentAsync(appointmentID);
        return targetAppt != null;
      }
      catch
      {
        return false;
      }
    }

    public async Task removeAppointment(string appointmentID, AppointmentCalendar currentCalendar) => await currentCalendar.DeleteAppointmentAsync(appointmentID);

    private string correctText(string Text)
    {
      Text = Text.ToLower();
      Text = Text.Substring(0, 1).ToUpper() + Text.Substring(1);
      for (int index = Text.IndexOf(" "); index != -1; index = Text.IndexOf(" ", index + 1))
        Text = Text.Substring(0, index + 1) + Text.Substring(index + 1, 1).ToUpper() + Text.Substring(index + 2);
      return Text;
    }
  }
}
