// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MemoryCounter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using Microsoft.Phone.Info;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Coding4Fun.Toolkit.Controls
{
  public class MemoryCounter : Control, IDisposable
  {
    private const float ByteToMega = 1048576f;
    private DispatcherTimer _timer;
    private bool _threwException;
    public static readonly DependencyProperty UpdateIntervalProperty = DependencyProperty.Register(nameof (UpdateInterval), typeof (int), typeof (MemoryCounter), new PropertyMetadata((object) 1000, new PropertyChangedCallback(MemoryCounter.OnUpdateIntervalChanged)));
    public static readonly DependencyProperty CurrentMemoryProperty = DependencyProperty.Register(nameof (CurrentMemory), typeof (string), typeof (MemoryCounter), new PropertyMetadata((object) "0"));
    public static readonly DependencyProperty PeakMemoryProperty = DependencyProperty.Register(nameof (PeakMemory), typeof (string), typeof (MemoryCounter), new PropertyMetadata((object) "0"));

    public MemoryCounter()
    {
      if (Debugger.IsAttached)
      {
        this.DefaultStyleKey = (object) typeof (MemoryCounter);
        ((FrameworkElement) this).DataContext = (object) this;
        ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.ControlLoaded);
        ((FrameworkElement) this).Unloaded += new RoutedEventHandler(this.ControlUnloaded);
      }
      else
        this.StopAndHide();
    }

    public int UpdateInterval
    {
      get => (int) ((DependencyObject) this).GetValue(MemoryCounter.UpdateIntervalProperty);
      set => ((DependencyObject) this).SetValue(MemoryCounter.UpdateIntervalProperty, (object) value);
    }

    private static void OnUpdateIntervalChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      if (!Debugger.IsAttached)
        return;
      MemoryCounter memoryCounter = (MemoryCounter) o;
      if (memoryCounter == null || memoryCounter._timer == null)
        return;
      memoryCounter._timer.Interval = TimeSpan.FromMilliseconds((double) (int) e.NewValue);
    }

    public string CurrentMemory
    {
      get => (string) ((DependencyObject) this).GetValue(MemoryCounter.CurrentMemoryProperty);
      set => ((DependencyObject) this).SetValue(MemoryCounter.CurrentMemoryProperty, (object) value);
    }

    public string PeakMemory
    {
      get => (string) ((DependencyObject) this).GetValue(MemoryCounter.PeakMemoryProperty);
      set => ((DependencyObject) this).SetValue(MemoryCounter.PeakMemoryProperty, (object) value);
    }

    private void TimerTick(object sender, EventArgs e)
    {
      if (Debugger.IsAttached)
      {
        if (!this._threwException)
          goto label_3;
      }
      this.StopAndHide();
label_3:
      try
      {
        this.CurrentMemory = ((float) DeviceStatus.ApplicationCurrentMemoryUsage / 1048576f).ToString("#.00");
        this.PeakMemory = ((float) DeviceStatus.ApplicationPeakMemoryUsage / 1048576f).ToString("#.00");
      }
      catch (Exception ex)
      {
        this._threwException = true;
        this._timer.Stop();
      }
    }

    private void StopAndHide()
    {
      if (this._timer != null)
        this._timer.Stop();
      ((UIElement) this).Visibility = (Visibility) 1;
    }

    private void ControlLoaded(object sender, RoutedEventArgs e)
    {
      if (ApplicationSpace.IsDesignMode)
        return;
      this._timer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromMilliseconds((double) this.UpdateInterval)
      };
      this._timer.Tick += new EventHandler(this.TimerTick);
      this._timer.Start();
      Frame rootFrame = ApplicationSpace.RootFrame;
      if (rootFrame == null)
        return;
      rootFrame.Navigated -= new NavigatedEventHandler(this.FrameNavigated);
      rootFrame.Navigated += new NavigatedEventHandler(this.FrameNavigated);
    }

    private void ControlUnloaded(object sender, RoutedEventArgs e) => this.Dispose();

    private void FrameNavigated(object sender, NavigationEventArgs e)
    {
      if (!e.IsNavigationInitiator)
        return;
      this.Dispose();
    }

    public void Dispose()
    {
      Frame rootFrame = ApplicationSpace.RootFrame;
      if (rootFrame != null)
        rootFrame.Navigated -= new NavigatedEventHandler(this.FrameNavigated);
      if (this._timer == null)
        return;
      this._timer.Stop();
      this._timer.Tick -= new EventHandler(this.TimerTick);
      this._timer = (DispatcherTimer) null;
    }
  }
}
