// WindowsPhone.MVVM.Tombstone.ApplicationState

//using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowsPhone.MVVM.Tombstone
{
  internal class ApplicationState
  {
    internal static void Save(Page page)
    {
      foreach (PropertyInfo tombstoneProperty in 
                ApplicationState.GetTombstoneProperties(
                    ((FrameworkElement) page).DataContext))
      {
        string key = ApplicationState.GetKey(tombstoneProperty);
        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
          PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };
        object obj = tombstoneProperty.GetValue(((FrameworkElement) page).DataContext, 
            (object[]) null);

                //RnD
        //page.State[key] = (object) JsonConvert.SerializeObject(obj,
        //    Formatting.None, settings);
      }
    }

    internal static void Restore(Page page)
    {
      foreach (PropertyInfo tombstoneProperty in 
                ApplicationState.GetTombstoneProperties(
                    ((FrameworkElement) page).DataContext))
      {
        if (tombstoneProperty.GetValue(((FrameworkElement) page).DataContext,
            (object[]) null) == null)
        {
          string key = ApplicationState.GetKey(tombstoneProperty);

             /*
            if (page.State.ContainsKey(key))
            {
                tombstoneProperty.SetValue(((FrameworkElement)page).DataContext,
                    JsonConvert.DeserializeObject((string)page.State[key],
                    tombstoneProperty.PropertyType), (object[])null);
            }
              */
        }
      }
    }

    private static string GetKey(PropertyInfo Prop) => "tshelper.viewmodel." 
            + Prop.Name;

    private static IEnumerable<PropertyInfo> GetTombstoneProperties(object ViewModel)
    {
      if (ViewModel == null)
        return (IEnumerable<PropertyInfo>) new List<PropertyInfo>();

      //RnD
            IEnumerable<PropertyInfo> tombstoneProperties
                      = default;/*((IEnumerable<PropertyInfo>)
                ViewModel.GetType().GetProperties()).Where<PropertyInfo>(
                    (Func<PropertyInfo, bool>) (
                    p => p.GetCustomAttributes(typeof (TombstoneAttribute), 
                    false).Length > 0));*/
      foreach (PropertyInfo propertyInfo in tombstoneProperties)
      {
        if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
          throw new TombstoneException(string.Format(
              "Cannot restore value of property {0}. " +
              "Make sure the getter and setter are public", 
              (object) propertyInfo.Name));
      }
      return tombstoneProperties;
    }
  }
}
