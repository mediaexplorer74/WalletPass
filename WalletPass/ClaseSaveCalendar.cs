// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseSaveCalendar
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Threading.Tasks;
using WalletPass.Resources;
using Windows.ApplicationModel.Appointments;

namespace WalletPass
{
  internal class ClaseSaveCalendar
  {
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
              case "PKTransitTypeAir":
                appointment.put_Subject(AppResources.CalendarSubjectTypeTransitAir + " (" + this.correctText(item.organizationName) + ")");
                break;
              case "PKTransitTypeBoat":
                appointment.put_Subject(AppResources.CalendarSubjectTypeTransitBoat + " (" + this.correctText(item.organizationName) + ")");
                break;
              case "PKTransitTypeBus":
                appointment.put_Subject(AppResources.CalendarSubjectTypeTransitBus + " (" + this.correctText(item.organizationName) + ")");
                break;
              case "PKTransitTypeGeneric":
                appointment.put_Subject(AppResources.CalendarSubjectTypeTransitGeneric + " (" + this.correctText(item.organizationName) + ")");
                break;
              case "PKTransitTypeTrain":
                appointment.put_Subject(AppResources.CalendarSubjectTypeTransitTrain + " (" + this.correctText(item.organizationName) + ")");
                break;
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

    public async Task<bool> editAppointment(
      string appointmentID,
      AppointmentCalendar currentCalendar,
      DateTime newDate)
    {
      try
      {
        Appointment targetAppt = await currentCalendar.GetAppointmentAsync(appointmentID);
        if (targetAppt != null)
        {
          targetAppt.put_StartTime((DateTimeOffset) newDate);
          await currentCalendar.SaveAppointmentAsync(targetAppt);
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
