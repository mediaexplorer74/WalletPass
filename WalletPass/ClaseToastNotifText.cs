// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseToastNotifText
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Notifications;

namespace WalletPass
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
      return passBackgroundTask.relevantDate.Year == 1 ? passBackgroundTask.organizationName : passBackgroundTask.organizationName + " - (" + passBackgroundTask.relevantDate.ToString("t") + ")";
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
              str1 += this.localizedText("CalendarSubjectTypeTransitAir", CultureInfo.CurrentCulture.Name);
              break;
            case "PKTransitTypeBoat":
              str1 += this.localizedText("CalendarSubjectTypeTransitBoat", CultureInfo.CurrentCulture.Name);
              break;
            case "PKTransitTypeBus":
              str1 += this.localizedText("CalendarSubjectTypeTransitBus", CultureInfo.CurrentCulture.Name);
              break;
            case "PKTransitTypeGeneric":
              str1 += this.localizedText("CalendarSubjectTypeTransitGeneric", CultureInfo.CurrentCulture.Name);
              break;
            case "PKTransitTypeTrain":
              str1 += this.localizedText("CalendarSubjectTypeTransitTrain", CultureInfo.CurrentCulture.Name);
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
            return "Avião";
          case "CalendarSubjectTypeTransitBoat":
            return "Barco";
          case "CalendarSubjectTypeTransitBus":
            return "Ônibus";
          case "CalendarSubjectTypeTransitGeneric":
            return "Viagem";
          case "CalendarSubjectTypeTransitTrain":
            return "Trem";
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
