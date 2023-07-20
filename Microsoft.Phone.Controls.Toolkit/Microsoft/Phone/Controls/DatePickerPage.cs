﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DatePickerPage
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a page used by the DatePicker control that allows the user to choose a date (day/month/year).
  /// </summary>
  public class DatePickerPage : DateTimePickerPageBase
  {
    internal VisualStateGroup VisibilityStates;
    internal VisualState Open;
    internal VisualState Closed;
    internal PlaneProjection PlaneProjection;
    internal Rectangle SystemTrayPlaceholder;
    internal TextBlock HeaderTitle;
    internal LoopingSelector SecondarySelector;
    internal LoopingSelector TertiarySelector;
    internal LoopingSelector PrimarySelector;
    private bool _contentLoaded;

    /// <summary>
    /// Initializes a new instance of the DatePickerPage control.
    /// </summary>
    public DatePickerPage()
    {
      this.InitializeComponent();
      this.PrimarySelector.DataSource = (ILoopingSelectorDataSource) new YearDataSource();
      this.SecondarySelector.DataSource = (ILoopingSelectorDataSource) new MonthDataSource();
      this.TertiarySelector.DataSource = (ILoopingSelectorDataSource) new DayDataSource();
      this.InitializeDateTimePickerPage(this.PrimarySelector, this.SecondarySelector, this.TertiarySelector);
    }

    /// <summary>
    /// Gets a sequence of LoopingSelector parts ordered according to culture string for date/time formatting.
    /// </summary>
    /// <returns>LoopingSelectors ordered by culture-specific priority.</returns>
    protected override IEnumerable<LoopingSelector> GetSelectorsOrderedByCulturePattern()
    {
      string pattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpperInvariant();
      if (DateTimePickerBase.DateShouldFlowRTL())
      {
        char[] charArray = pattern.ToCharArray();
        Array.Reverse((Array) charArray);
        pattern = new string(charArray);
      }
      return DateTimePickerPageBase.GetSelectorsOrderedByCulturePattern(pattern, new char[3]
      {
        'Y',
        'M',
        'D'
      }, new LoopingSelector[3]
      {
        this.PrimarySelector,
        this.SecondarySelector,
        this.TertiarySelector
      });
    }

    /// <summary>Handles changes to the page's Orientation property.</summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnOrientationChanged(OrientationChangedEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      base.OnOrientationChanged(e);
      ((UIElement) this.SystemTrayPlaceholder).Visibility = (1 & e.Orientation) != null ? (Visibility) 0 : (Visibility) 1;
    }

    /// <summary>Sets the selectors and title flow direction.</summary>
    /// <param name="flowDirection">Flow direction to set.</param>
    internal override void SetFlowDirection(FlowDirection flowDirection)
    {
      ((FrameworkElement) this.HeaderTitle).FlowDirection = flowDirection;
      ((FrameworkElement) this.PrimarySelector).FlowDirection = flowDirection;
      ((FrameworkElement) this.SecondarySelector).FlowDirection = flowDirection;
      ((FrameworkElement) this.TertiarySelector).FlowDirection = flowDirection;
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Microsoft.Phone.Controls.Toolkit;component/DateTimePickers/DatePickerPage.xaml", UriKind.Relative));
      this.VisibilityStates = (VisualStateGroup) ((FrameworkElement) this).FindName("VisibilityStates");
      this.Open = (VisualState) ((FrameworkElement) this).FindName("Open");
      this.Closed = (VisualState) ((FrameworkElement) this).FindName("Closed");
      this.PlaneProjection = (PlaneProjection) ((FrameworkElement) this).FindName("PlaneProjection");
      this.SystemTrayPlaceholder = (Rectangle) ((FrameworkElement) this).FindName("SystemTrayPlaceholder");
      this.HeaderTitle = (TextBlock) ((FrameworkElement) this).FindName("HeaderTitle");
      this.SecondarySelector = (LoopingSelector) ((FrameworkElement) this).FindName("SecondarySelector");
      this.TertiarySelector = (LoopingSelector) ((FrameworkElement) this).FindName("TertiarySelector");
      this.PrimarySelector = (LoopingSelector) ((FrameworkElement) this).FindName("PrimarySelector");
    }
  }
}
