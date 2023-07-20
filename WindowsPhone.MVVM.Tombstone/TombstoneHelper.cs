// WindowsPhone.MVVM.Tombstone.TombstoneHelper

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
//using System.Windows.Navigation;

namespace WindowsPhone.MVVM.Tombstone
{
  public sealed class TombstoneHelper
  {
    private static List<Page> restoredpages = new List<Page>();
    private static bool _hasbeentombstoned = false;

        /*
    public static void Application_Activated(object sender, ActivatedEventArgs e)
    {
      if (e.IsApplicationInstancePreserved)
        return;
      TombstoneHelper._hasbeentombstoned = true;
    }

    public static void page_OnNavigatedTo(Page sender, NavigationEventArgs e)
    {
      if (sender == null || !TombstoneHelper._hasbeentombstoned || TombstoneHelper.restoredpages.Contains(sender))
        return;
      TombstoneHelper.restoredpages.Add(sender);
      ApplicationState.Restore(sender);
    }

    public static void page_OnNavigatedFrom(PhoneApplicationPage sender, 
        NavigationEventArgs e)
    {
      if (e.NavigationMode == 1 || sender == null)
        return;
      ApplicationState.Save(sender);
    }
        */
  }
}
