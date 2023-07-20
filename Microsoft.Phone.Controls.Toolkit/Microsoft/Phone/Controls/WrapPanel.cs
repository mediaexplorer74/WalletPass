// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.WrapPanel
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Positions child elements sequentially from left to right or top to
  /// bottom.  When elements extend beyond the panel edge, elements are
  /// positioned in the next row or column.
  /// </summary>
  /// <QualityBand>Mature</QualityBand>
  public class WrapPanel : Panel
  {
    /// <summary>
    /// A value indicating whether a dependency property change handler
    /// should ignore the next change notification.  This is used to reset
    /// the value of properties without performing any of the actions in
    /// their change handlers.
    /// </summary>
    private bool _ignorePropertyChange;
    /// <summary>
    /// Identifies the
    /// <see cref="P:System.Windows.Controls.WrapPanel.ItemHeight" />
    /// dependency property.
    /// </summary>
    /// <value>
    /// The identifier for the
    /// <see cref="P:System.Windows.Controls.WrapPanel.ItemHeight" />
    /// dependency property
    /// </value>
    public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof (ItemHeight), typeof (double), typeof (WrapPanel), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(WrapPanel.OnItemHeightOrWidthPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:System.Windows.Controls.WrapPanel.ItemWidth" />
    /// dependency property.
    /// </summary>
    /// <value>
    /// The identifier for the
    /// <see cref="P:System.Windows.Controls.WrapPanel.ItemWidth" />
    /// dependency property.
    /// </value>
    public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof (ItemWidth), typeof (double), typeof (WrapPanel), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(WrapPanel.OnItemHeightOrWidthPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:System.Windows.Controls.WrapPanel.Orientation" />
    /// dependency property.
    /// </summary>
    /// <value>
    /// The identifier for the
    /// <see cref="P:System.Windows.Controls.WrapPanel.Orientation" />
    /// dependency property.
    /// </value>
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (WrapPanel), new PropertyMetadata((object) (Orientation) 1, new PropertyChangedCallback(WrapPanel.OnOrientationPropertyChanged)));

    /// <summary>
    /// Gets or sets the height of the layout area for each item that is
    /// contained in a <see cref="T:System.Windows.Controls.WrapPanel" />.
    /// </summary>
    /// <value>
    /// The height applied to the layout area of each item that is contained
    /// within a <see cref="T:System.Windows.Controls.WrapPanel" />.  The
    /// default value is <see cref="F:System.Double.NaN" />.
    /// </value>
    [TypeConverter(typeof (LengthConverter))]
    public double ItemHeight
    {
      get => (double) ((DependencyObject) this).GetValue(WrapPanel.ItemHeightProperty);
      set => ((DependencyObject) this).SetValue(WrapPanel.ItemHeightProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the width of the layout area for each item that is
    /// contained in a <see cref="T:System.Windows.Controls.WrapPanel" />.
    /// </summary>
    /// <value>
    /// The width that applies to the layout area of each item that is
    /// contained in a <see cref="T:System.Windows.Controls.WrapPanel" />.
    /// The default value is <see cref="F:System.Double.NaN" />.
    /// </value>
    [TypeConverter(typeof (LengthConverter))]
    public double ItemWidth
    {
      get => (double) ((DependencyObject) this).GetValue(WrapPanel.ItemWidthProperty);
      set => ((DependencyObject) this).SetValue(WrapPanel.ItemWidthProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the direction in which child elements are arranged.
    /// </summary>
    /// <value>
    /// One of the <see cref="T:System.Windows.Controls.Orientation" />
    /// values.  The default is
    /// <see cref="F:System.Windows.Controls.Orientation.Horizontal" />.
    /// </value>
    public Orientation Orientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(WrapPanel.OrientationProperty);
      set => ((DependencyObject) this).SetValue(WrapPanel.OrientationProperty, (object) value);
    }

    /// <summary>OrientationProperty property changed handler.</summary>
    /// <param name="d">WrapPanel that changed its Orientation.</param>
    /// <param name="e">Event arguments.</param>
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "Almost always set from the CLR property.")]
    private static void OnOrientationPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      WrapPanel wrapPanel = (WrapPanel) d;
      Orientation newValue = (Orientation) e.NewValue;
      if (wrapPanel._ignorePropertyChange)
      {
        wrapPanel._ignorePropertyChange = false;
      }
      else
      {
        if (newValue != 1 && newValue != 0)
        {
          wrapPanel._ignorePropertyChange = true;
          ((DependencyObject) wrapPanel).SetValue(WrapPanel.OrientationProperty, (object) (Orientation) e.OldValue);
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.WrapPanel_OnOrientationPropertyChanged_InvalidValue, new object[1]
          {
            (object) newValue
          }), "value");
        }
        ((UIElement) wrapPanel).InvalidateMeasure();
      }
    }

    /// <summary>
    /// Property changed handler for ItemHeight and ItemWidth.
    /// </summary>
    /// <param name="d">
    /// WrapPanel that changed its ItemHeight or ItemWidth.
    /// </param>
    /// <param name="e">Event arguments.</param>
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "Almost always set from the CLR property.")]
    private static void OnItemHeightOrWidthPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      WrapPanel wrapPanel = (WrapPanel) d;
      double newValue = (double) e.NewValue;
      if (wrapPanel._ignorePropertyChange)
      {
        wrapPanel._ignorePropertyChange = false;
      }
      else
      {
        if (!newValue.IsNaN() && (newValue <= 0.0 || double.IsPositiveInfinity(newValue)))
        {
          wrapPanel._ignorePropertyChange = true;
          ((DependencyObject) wrapPanel).SetValue(e.Property, (object) (double) e.OldValue);
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.WrapPanel_OnItemHeightOrWidthPropertyChanged_InvalidValue, new object[1]
          {
            (object) newValue
          }), "value");
        }
        ((UIElement) wrapPanel).InvalidateMeasure();
      }
    }

    /// <summary>
    /// Measures the child elements of a
    /// <see cref="T:System.Windows.Controls.WrapPanel" /> in anticipation
    /// of arranging them during the
    /// <see cref="M:System.Windows.Controls.WrapPanel.ArrangeOverride(System.Windows.Size)" />
    /// pass.
    /// </summary>
    /// <param name="constraint">
    /// The size available to child elements of the wrap panel.
    /// </param>
    /// <returns>
    /// The size required by the
    /// <see cref="T:System.Windows.Controls.WrapPanel" /> and its
    /// elements.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    protected virtual Size MeasureOverride(Size constraint)
    {
      Orientation orientation1 = this.Orientation;
      OrientedSize orientedSize1 = new OrientedSize(orientation1);
      OrientedSize orientedSize2 = new OrientedSize(orientation1);
      OrientedSize orientedSize3 = new OrientedSize(orientation1, constraint.Width, constraint.Height);
      double itemWidth = this.ItemWidth;
      double itemHeight = this.ItemHeight;
      bool flag1 = !itemWidth.IsNaN();
      bool flag2 = !itemHeight.IsNaN();
      Size size = new Size(flag1 ? itemWidth : constraint.Width, flag2 ? itemHeight : constraint.Height);
      foreach (UIElement child in (PresentationFrameworkCollection<UIElement>) this.Children)
      {
        child.Measure(size);
        OrientedSize orientedSize4;
        ref OrientedSize local = ref orientedSize4;
        Orientation orientation2 = orientation1;
        Size desiredSize;
        double width;
        if (!flag1)
        {
          desiredSize = child.DesiredSize;
          width = desiredSize.Width;
        }
        else
          width = itemWidth;
        double height;
        if (!flag2)
        {
          desiredSize = child.DesiredSize;
          height = desiredSize.Height;
        }
        else
          height = itemHeight;
        local = new OrientedSize(orientation2, width, height);
        if (NumericExtensions.IsGreaterThan(orientedSize1.Direct + orientedSize4.Direct, orientedSize3.Direct))
        {
          orientedSize2.Direct = Math.Max(orientedSize1.Direct, orientedSize2.Direct);
          orientedSize2.Indirect += orientedSize1.Indirect;
          orientedSize1 = orientedSize4;
          if (NumericExtensions.IsGreaterThan(orientedSize4.Direct, orientedSize3.Direct))
          {
            orientedSize2.Direct = Math.Max(orientedSize4.Direct, orientedSize2.Direct);
            orientedSize2.Indirect += orientedSize4.Indirect;
            orientedSize1 = new OrientedSize(orientation1);
          }
        }
        else
        {
          orientedSize1.Direct += orientedSize4.Direct;
          orientedSize1.Indirect = Math.Max(orientedSize1.Indirect, orientedSize4.Indirect);
        }
      }
      orientedSize2.Direct = Math.Max(orientedSize1.Direct, orientedSize2.Direct);
      orientedSize2.Indirect += orientedSize1.Indirect;
      return new Size(orientedSize2.Width, orientedSize2.Height);
    }

    /// <summary>
    /// Arranges and sizes the
    /// <see cref="T:System.Windows.Controls.WrapPanel" /> control and its
    /// child elements.
    /// </summary>
    /// <param name="finalSize">
    /// The area within the parent that the
    /// <see cref="T:System.Windows.Controls.WrapPanel" /> should use
    /// arrange itself and its children.
    /// </param>
    /// <returns>
    /// The actual size used by the
    /// <see cref="T:System.Windows.Controls.WrapPanel" />.
    /// </returns>
    protected virtual Size ArrangeOverride(Size finalSize)
    {
      Orientation orientation1 = this.Orientation;
      OrientedSize orientedSize1 = new OrientedSize(orientation1);
      OrientedSize orientedSize2 = new OrientedSize(orientation1, finalSize.Width, finalSize.Height);
      double itemWidth = this.ItemWidth;
      double itemHeight = this.ItemHeight;
      bool flag1 = !itemWidth.IsNaN();
      bool flag2 = !itemHeight.IsNaN();
      double indirectOffset = 0.0;
      double? directDelta = orientation1 == 1 ? (flag1 ? new double?(itemWidth) : new double?()) : (flag2 ? new double?(itemHeight) : new double?());
      UIElementCollection children = this.Children;
      int count = ((PresentationFrameworkCollection<UIElement>) children).Count;
      int lineStart = 0;
      for (int index = 0; index < count; ++index)
      {
        UIElement uiElement = ((PresentationFrameworkCollection<UIElement>) children)[index];
        OrientedSize orientedSize3;
        ref OrientedSize local = ref orientedSize3;
        Orientation orientation2 = orientation1;
        Size desiredSize;
        double width;
        if (!flag1)
        {
          desiredSize = uiElement.DesiredSize;
          width = desiredSize.Width;
        }
        else
          width = itemWidth;
        double height;
        if (!flag2)
        {
          desiredSize = uiElement.DesiredSize;
          height = desiredSize.Height;
        }
        else
          height = itemHeight;
        local = new OrientedSize(orientation2, width, height);
        if (NumericExtensions.IsGreaterThan(orientedSize1.Direct + orientedSize3.Direct, orientedSize2.Direct))
        {
          this.ArrangeLine(lineStart, index, directDelta, indirectOffset, orientedSize1.Indirect);
          indirectOffset += orientedSize1.Indirect;
          orientedSize1 = orientedSize3;
          if (NumericExtensions.IsGreaterThan(orientedSize3.Direct, orientedSize2.Direct))
          {
            this.ArrangeLine(index, ++index, directDelta, indirectOffset, orientedSize3.Indirect);
            indirectOffset += orientedSize1.Indirect;
            orientedSize1 = new OrientedSize(orientation1);
          }
          lineStart = index;
        }
        else
        {
          orientedSize1.Direct += orientedSize3.Direct;
          orientedSize1.Indirect = Math.Max(orientedSize1.Indirect, orientedSize3.Indirect);
        }
      }
      if (lineStart < count)
        this.ArrangeLine(lineStart, count, directDelta, indirectOffset, orientedSize1.Indirect);
      return finalSize;
    }

    /// <summary>Arrange a sequence of elements in a single line.</summary>
    /// <param name="lineStart">
    /// Index of the first element in the sequence to arrange.
    /// </param>
    /// <param name="lineEnd">
    /// Index of the last element in the sequence to arrange.
    /// </param>
    /// <param name="directDelta">
    /// Optional fixed growth in the primary direction.
    /// </param>
    /// <param name="indirectOffset">
    /// Offset of the line in the indirect direction.
    /// </param>
    /// <param name="indirectGrowth">
    /// Shared indirect growth of the elements on this line.
    /// </param>
    private void ArrangeLine(
      int lineStart,
      int lineEnd,
      double? directDelta,
      double indirectOffset,
      double indirectGrowth)
    {
      double num1 = 0.0;
      Orientation orientation1 = this.Orientation;
      bool flag = orientation1 == 1;
      UIElementCollection children = this.Children;
      for (int index = lineStart; index < lineEnd; ++index)
      {
        UIElement uiElement = ((PresentationFrameworkCollection<UIElement>) children)[index];
        OrientedSize orientedSize;
        ref OrientedSize local = ref orientedSize;
        Orientation orientation2 = orientation1;
        Size desiredSize = uiElement.DesiredSize;
        double width = desiredSize.Width;
        desiredSize = uiElement.DesiredSize;
        double height = desiredSize.Height;
        local = new OrientedSize(orientation2, width, height);
        double num2 = directDelta.HasValue ? directDelta.Value : orientedSize.Direct;
        Rect rect = flag ? new Rect(num1, indirectOffset, num2, indirectGrowth) : new Rect(indirectOffset, num1, indirectGrowth, num2);
        uiElement.Arrange(rect);
        num1 += num2;
      }
    }
  }
}
