// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.Pushpin
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>Represents a pushpin on the map.</summary>
  [ContentProperty("Content")]
  public sealed class Pushpin : MapChildControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.Pushpin" /> class.
    /// </summary>
    public Pushpin() => ((Control) this).DefaultStyleKey = (object) typeof (Pushpin);
  }
}
