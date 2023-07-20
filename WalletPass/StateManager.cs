// WalletPass.StateManager

//using Microsoft.Phone.Controls;

using Windows.UI.Xaml.Controls;

namespace WalletPass
{
  public static class StateManager
  {
    public static void SaveState
    (
      this Page phoneApplicationPage,
      string key, 
      object value
    )
    {
     // if (phoneApplicationPage.State.ContainsKey(key))
     //   phoneApplicationPage.State.Remove(key);
     // phoneApplicationPage.State.Add(key, value);
    }

    public static void SaveStateAll(Page phoneApplicationPage)
    {
      phoneApplicationPage.SaveState("_tempPassClassKey", (object) App._tempPassClass);
      phoneApplicationPage.SaveState("_tempPassGroupKey", (object) App._tempPassGroup);
      phoneApplicationPage.SaveState("_settingsKey", (object) App._settings);
      phoneApplicationPage.SaveState("_updatesKey", (object) App._updates);
      phoneApplicationPage.SaveState("_archivedKey", (object) App._archived);
      phoneApplicationPage.SaveState("_pkPassKey", (object) App._pkPass);
      phoneApplicationPage.SaveState("_pkPassGroupKey", (object) App._pkPassGroup);
      phoneApplicationPage.SaveState("_infoPageKey", (object) App._infoPage);
      phoneApplicationPage.SaveState("_pageEntryKey", (object) App._pageEntry);
      phoneApplicationPage.SaveState("_pageArchiveKey", (object) App._pageArchive);
      phoneApplicationPage.SaveState("_rightGestureKey", (object) App._rightGesture);
      phoneApplicationPage.SaveState("_groupItemIndexKey", (object) App._groupItemIndex);
      phoneApplicationPage.SaveState("_pageEntryKey", (object) App._pageEntry);
      phoneApplicationPage.SaveState("_colorPageKey", (object) App._colorPage);
      phoneApplicationPage.SaveState("_colorPageTypeKey", (object) App._colorPageType);
    }

        public static T LoadState<T>(this Page phoneApplicationPage, string key)
        {
            return default(T);//phoneApplicationPage.State.ContainsKey(key) 
                //? 
                //(T)phoneApplicationPage.State[key] 
                //: default(T);
        }

        public static void LoadStateAll(Page phoneApplicationPage)
    {
      App._tempPassClass = phoneApplicationPage.LoadState<ClasePass>("_tempPassClassKey");
      App._tempPassGroup = phoneApplicationPage.LoadState<ClaseGroup<ClasePass>>("_tempPassGroupKey");
      App._settings = phoneApplicationPage.LoadState<bool>("_settingsKey");
      App._archived = phoneApplicationPage.LoadState<bool>("_archivedKey");
      App._updates = phoneApplicationPage.LoadState<bool>("_updatesKey");
      App._pkPass = phoneApplicationPage.LoadState<bool>("_pkPassKey");
      App._pkPassGroup = phoneApplicationPage.LoadState<bool>("_pkPassGroupKey");
      App._infoPage = phoneApplicationPage.LoadState<bool>("_infoPageKey");
      App._pageEntry = phoneApplicationPage.LoadState<bool>("_pageEntryKey");
      App._pageArchive = phoneApplicationPage.LoadState<bool>("_pageArchiveKey");
      App._rightGesture = phoneApplicationPage.LoadState<bool>("_rightGestureKey");
      App._groupItemIndex = phoneApplicationPage.LoadState<int>("_groupItemIndexKey");
      App._pageEntry = phoneApplicationPage.LoadState<bool>("_pageEntryKey");
      App._colorPage = phoneApplicationPage.LoadState<string>("_colorPageKey");
      App._colorPageType = phoneApplicationPage.LoadState<int>("_colorPageTypeKey");
    }
  }
}
