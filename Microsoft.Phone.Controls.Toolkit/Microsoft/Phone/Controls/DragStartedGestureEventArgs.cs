// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DragStartedGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>The event args used in the DragStarted event.</summary>
  public class DragStartedGestureEventArgs : GestureEventArgs
  {
    internal DragStartedGestureEventArgs(Point gestureOrigin, Orientation direction)
      : base(gestureOrigin, gestureOrigin)
    {
      this.Direction = direction;
    }

    /// <summary>
    /// The direction of the drag gesture, as determined by the initial drag change.
    /// </summary>
    public Orientation Direction { get; private set; }
  }
}
