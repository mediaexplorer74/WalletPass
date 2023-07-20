// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MovementMonitor
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Coding4Fun.Toolkit.Controls
{
  public class MovementMonitor
  {
    protected Rectangle Monitor;
    private double _xOffsetStartValue;
    private double _yOffsetStartValue;

    public event EventHandler<MovementMonitorEventArgs> Movement;

    public void MonitorControl(Panel panel)
    {
      Rectangle rectangle = new Rectangle();
      ((Shape) rectangle).Fill = (Brush) new SolidColorBrush(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0));
      this.Monitor = rectangle;
      ((DependencyObject) this.Monitor).SetValue(Grid.RowSpanProperty, (object) 2147483646);
      ((DependencyObject) this.Monitor).SetValue(Grid.ColumnSpanProperty, (object) 2147483646);
      ((UIElement) this.Monitor).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.MonitorManipulationStarted);
      ((UIElement) this.Monitor).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.MonitorManipulationDelta);
      ((PresentationFrameworkCollection<UIElement>) panel.Children).Add((UIElement) this.Monitor);
    }

    private void MonitorManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      if (this.Movement != null)
        this.Movement((object) this, new MovementMonitorEventArgs()
        {
          X = this._xOffsetStartValue + e.CumulativeManipulation.Translation.X,
          Y = this._yOffsetStartValue + e.CumulativeManipulation.Translation.Y
        });
      e.Handled = true;
    }

    private void MonitorManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this._xOffsetStartValue = e.ManipulationOrigin.X;
      this._yOffsetStartValue = e.ManipulationOrigin.Y;
      if (this.Movement != null)
        this.Movement((object) this, new MovementMonitorEventArgs()
        {
          X = this._xOffsetStartValue,
          Y = this._yOffsetStartValue
        });
      e.Handled = true;
    }
  }
}
