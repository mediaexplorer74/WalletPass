﻿// Decompiled with JetBrains decompiler
// Type: WPControls.SelectionChangedEventArgs
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using System;

namespace WPControls
{
  public class SelectionChangedEventArgs : EventArgs
  {
    private SelectionChangedEventArgs()
    {
    }

    internal SelectionChangedEventArgs(DateTime dateTime) => this.SelectedDate = dateTime;

    public DateTime SelectedDate { get; private set; }
  }
}
