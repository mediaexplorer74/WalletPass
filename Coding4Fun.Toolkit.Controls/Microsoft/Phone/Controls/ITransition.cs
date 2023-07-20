// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ITransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  internal interface ITransition
  {
    event EventHandler Completed;

    ClockState GetCurrentState();

    TimeSpan GetCurrentTime();

    void Pause();

    void Resume();

    void Seek(TimeSpan offset);

    void SeekAlignedToLastTick(TimeSpan offset);

    void SkipToFill();

    void Begin();

    void Stop();
  }
}
