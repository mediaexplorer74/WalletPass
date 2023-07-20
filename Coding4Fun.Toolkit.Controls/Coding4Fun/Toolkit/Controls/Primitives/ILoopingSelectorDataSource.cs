// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Primitives.ILoopingSelectorDataSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls.Primitives
{
  public interface ILoopingSelectorDataSource
  {
    object GetNext(object relativeTo);

    object GetPrevious(object relativeTo);

    object SelectedItem { get; set; }

    bool IsEmpty { get; }

    event EventHandler<SelectionChangedEventArgs> SelectionChanged;
  }
}
