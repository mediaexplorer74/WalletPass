// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.ApplicationSpace
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class ApplicationSpace
  {
    public static int ScaleFactor() => Application.Current.Host.Content.ScaleFactor;

    public static Frame RootFrame => Application.Current.RootVisual as Frame;

    public static bool IsDesignMode => DesignerProperties.IsInDesignTool;

    public static Dispatcher CurrentDispatcher => ((DependencyObject) Deployment.Current).Dispatcher;
  }
}
