// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseToastNotifText
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.System.UserProfile;
using Windows.UI.Notifications;

namespace Wallet_Pass
{
  public class ClaseToastNotifText
  {
    private ClasePassBackgroundTaskCollection _passCollection;

    public ClaseToastNotifText() => this.InitializeAll();

    public ClaseToastNotifText(ClasePassBackgroundTaskCollection passCollection) => this._passCollection = passCollection;

    private void InitializeAll()
    {
      List<ClasePassBackgroundTask> passBackgroundTaskList = IO.LoadDataPassesBackgroundTask();
      this._passCollection = new ClasePassBackgroundTaskCollection();
      for (int index = 0; index < passBackgroundTaskList.Count; ++index)
        this._passCollection.Add(passBackgroundTaskList[index]);
    }

    public string returnHeaderText(string serialNumber)
    {
      ClasePassBackgroundTask passBackgroundTask = this._passCollection.returnPass(serialNumber);
      CultureInfo cultureInfo = new CultureInfo(GlobalizationPreferences.Languages[0]);
      CultureInfo threadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
      CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
      string str = passBackgroundTask.relevantDate.Year != 1 ? passBackgroundTask.organizationName + " - (" + passBackgroundTask.relevantDate.ToString("t", (IFormatProvider) CultureInfo.DefaultThreadCurrentCulture) + ")" : passBackgroundTask.organizationName;
      CultureInfo.DefaultThreadCurrentCulture = threadCurrentCulture;
      return str;
    }

    public string returnBodyText(string serialNumber)
    {
      string str1 = "";
      ClasePassBackgroundTask passBackgroundTask = this._passCollection.returnPass(serialNumber);
      string str2;
      switch (passBackgroundTask.type)
      {
        case "boardingPass":
          switch (passBackgroundTask.transitType)
          {
            case "PKTransitTypeAir":
              str1 += this.localizedText("CalendarSubjectTypeTransitAir", GlobalizationPreferences.Languages[0]);
              break;
            case "PKTransitTypeBoat":
              str1 += this.localizedText("CalendarSubjectTypeTransitBoat", GlobalizationPreferences.Languages[0]);
              break;
            case "PKTransitTypeBus":
              str1 += this.localizedText("CalendarSubjectTypeTransitBus", GlobalizationPreferences.Languages[0]);
              break;
            case "PKTransitTypeGeneric":
              str1 += this.localizedText("CalendarSubjectTypeTransitGeneric", GlobalizationPreferences.Languages[0]);
              break;
            case "PKTransitTypeTrain":
              str1 += this.localizedText("CalendarSubjectTypeTransitTrain", GlobalizationPreferences.Languages[0]);
              break;
          }
          str2 = str1 + " (" + passBackgroundTask.PrimaryFields[0].Label + " -> " + passBackgroundTask.PrimaryFields[1].Label + ")";
          break;
        default:
          if (passBackgroundTask.PrimaryFields.Count > 0)
          {
            str2 = str1 + "(" + this.correctText(passBackgroundTask.organizationName) + ") " + passBackgroundTask.PrimaryFields[0].Value;
            break;
          }
          str2 = str1 + "(" + this.correctText(passBackgroundTask.organizationName) + ")";
          break;
      }
      return str2;
    }

    private string correctText(string Text)
    {
      Text = Text.ToLower();
      Text = Text.Substring(0, 1).ToUpper() + Text.Substring(1);
      for (int index = Text.IndexOf(" "); index != -1; index = Text.IndexOf(" ", index + 1))
        Text = Text.Substring(0, index + 1) + Text.Substring(index + 1, 1).ToUpper() + Text.Substring(index + 2);
      return Text;
    }

    private string localizedText(string text, string culture)
    {
      if (culture.Contains("de"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "Flugticket";
          case "CalendarSubjectTypeTransitBoat":
            return "Ticket für ein Schiff";
          case "CalendarSubjectTypeTransitBus":
            return "Bus Ticket";
          case "CalendarSubjectTypeTransitGeneric":
            return "Ticket für eine Reise";
          case "CalendarSubjectTypeTransitTrain":
            return "Zug Ticket";
        }
      }
      else if (culture.Contains("es"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "Avión";
          case "CalendarSubjectTypeTransitBoat":
            return "Barco";
          case "CalendarSubjectTypeTransitBus":
            return "Autobús";
          case "CalendarSubjectTypeTransitGeneric":
            return "Viaje";
          case "CalendarSubjectTypeTransitTrain":
            return "Tren";
        }
      }
      else if (culture.Contains("fr"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "billet d'avion";
          case "CalendarSubjectTypeTransitBoat":
            return "billet de bateau";
          case "CalendarSubjectTypeTransitBus":
            return "billet de bus";
          case "CalendarSubjectTypeTransitGeneric":
            return "billet de voyage";
          case "CalendarSubjectTypeTransitTrain":
            return "billet de train";
        }
      }
      else if (culture.Contains("it"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "biglietto aereo";
          case "CalendarSubjectTypeTransitBoat":
            return "biglietto della nave";
          case "CalendarSubjectTypeTransitBus":
            return "biglietto dell'autobus";
          case "CalendarSubjectTypeTransitGeneric":
            return "biglietto di viaggio";
          case "CalendarSubjectTypeTransitTrain":
            return "biglietto del treno";
        }
      }
      else if (culture.Contains("nl"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "vliegticket";
          case "CalendarSubjectTypeTransitBoat":
            return "boot ticket";
          case "CalendarSubjectTypeTransitBus":
            return "busticket";
          case "CalendarSubjectTypeTransitGeneric":
            return "ticket reizen";
          case "CalendarSubjectTypeTransitTrain":
            return "treinkaartje";
        }
      }
      else if (culture.Contains("pt"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "bilhete de avião";
          case "CalendarSubjectTypeTransitBoat":
            return "bohete de barco";
          case "CalendarSubjectTypeTransitBus":
            return "passagem de ônibus";
          case "CalendarSubjectTypeTransitGeneric":
            return "bilhete de viagem";
          case "CalendarSubjectTypeTransitTrain":
            return "bilhete de trem";
        }
      }
      else if (culture.Contains("sv"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "Flygplansbiljett";
          case "CalendarSubjectTypeTransitBoat":
            return "Båtbiljett";
          case "CalendarSubjectTypeTransitBus":
            return "Bussbiljett";
          case "CalendarSubjectTypeTransitGeneric":
            return "Resebiljett";
          case "CalendarSubjectTypeTransitTrain":
            return "Tågbiljett";
        }
      }
      else if (culture.Contains("fi"))
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "Lentolippu";
          case "CalendarSubjectTypeTransitBoat":
            return "Laivalippu";
          case "CalendarSubjectTypeTransitBus":
            return "Linja-autolippu";
          case "CalendarSubjectTypeTransitGeneric":
            return "Matkalippu";
          case "CalendarSubjectTypeTransitTrain":
            return "Junalippu";
        }
      }
      else
      {
        switch (text)
        {
          case "CalendarSubjectTypeTransitAir":
            return "Plane ticket";
          case "CalendarSubjectTypeTransitBoat":
            return "Boat ticket";
          case "CalendarSubjectTypeTransitBus":
            return "Bus ticket";
          case "CalendarSubjectTypeTransitGeneric":
            return "Travel ticket";
          case "CalendarSubjectTypeTransitTrain":
            return "Train ticket";
        }
      }
      return "";
    }

    public void toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
    {
      if (args.Reason != null)
        return;
      IO.SaveData(new ClasePassBackgroundTaskCollection(IO.LoadDataPassesBackgroundTask()));
    }
  }
}
