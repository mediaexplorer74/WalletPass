// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.IDateTimePickerPage
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Represents an interface for DatePicker/TimePicker to use for communicating with a picker page.
  /// </summary>
  public interface IDateTimePickerPage
  {
    /// <summary>
    /// Gets or sets the DateTime to show in the picker page and to set when the user makes a selection.
    /// </summary>
    DateTime? Value { get; set; }
  }
}
