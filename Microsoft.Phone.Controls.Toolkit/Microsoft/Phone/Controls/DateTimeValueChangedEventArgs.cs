// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DateTimeValueChangedEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides data for the DatePicker and TimePicker's ValueChanged event.
  /// </summary>
  public class DateTimeValueChangedEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the DateTimeValueChangedEventArgs class.
    /// </summary>
    /// <param name="oldDateTime">Old DateTime value.</param>
    /// <param name="newDateTime">New DateTime value.</param>
    public DateTimeValueChangedEventArgs(DateTime? oldDateTime, DateTime? newDateTime)
    {
      this.OldDateTime = oldDateTime;
      this.NewDateTime = newDateTime;
    }

    /// <summary>Gets or sets the old DateTime value.</summary>
    public DateTime? OldDateTime { get; private set; }

    /// <summary>Gets or sets the new DateTime value.</summary>
    public DateTime? NewDateTime { get; private set; }
  }
}
