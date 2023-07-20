// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DateTimePickerResources
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.LocalizedResources;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides access to the localized resources used by the DatePicker and TimePicker.
  /// </summary>
  [SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Justification = "Making this class static breaks its use as a resource in generic.xaml.")]
  public class DateTimePickerResources
  {
    /// <summary>Gets the localized DatePicker title string.</summary>
    public static string DatePickerTitle => ControlResources.DatePickerTitle;

    /// <summary>Gets the localized TimePicker title string.</summary>
    public static string TimePickerTitle => ControlResources.TimePickerTitle;
  }
}
