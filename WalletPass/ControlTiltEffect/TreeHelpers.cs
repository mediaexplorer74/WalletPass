// Decompiled with JetBrains decompiler
// Type: ControlTiltEffect.TreeHelpers
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System.Collections.Generic;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
//using System.Windows.Media;

namespace ControlTiltEffect
{
  internal static class TreeHelpers
  {
    public static IEnumerable<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
    {
      for (FrameworkElement parent = node.GetVisualParent(); 
                parent != null; parent = parent.GetVisualParent())
        yield return parent;
    }

        public static FrameworkElement GetVisualParent(this FrameworkElement node)
        {
            return VisualTreeHelper.GetParent((DependencyObject)node) as FrameworkElement;
        }
    }
}
