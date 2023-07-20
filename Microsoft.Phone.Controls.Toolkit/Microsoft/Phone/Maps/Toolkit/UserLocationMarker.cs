// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.UserLocationMarker
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Represents a marker for the location of a user on the map.
  /// </summary>
  [ContentProperty("Content")]
  public sealed class UserLocationMarker : MapChildControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.UserLocationMarker" /> class.
    /// </summary>
    public UserLocationMarker() => ((Control) this).DefaultStyleKey = (object) typeof (UserLocationMarker);
  }
}
