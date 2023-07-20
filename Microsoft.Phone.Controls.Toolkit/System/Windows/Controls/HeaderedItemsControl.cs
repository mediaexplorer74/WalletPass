// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.HeaderedItemsControl
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;

namespace System.Windows.Controls
{
  /// <summary>
  /// Represents a control that contains a collection of items and a header.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (ContentPresenter))]
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Headered", Justification = "Consistency with WPF")]
  public class HeaderedItemsControl : ItemsControl
  {
    /// <summary>
    /// Identifies the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.Header" />
    /// dependency property.
    /// </summary>
    /// <value>
    /// The identifier for the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.Header" />
    /// dependency property.
    /// </value>
    /// <remarks>
    /// Note: WPF defines this property via a call to AddOwner of
    /// HeaderedContentControl's HeaderProperty.
    /// </remarks>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (object), typeof (HeaderedItemsControl), new PropertyMetadata(new PropertyChangedCallback(HeaderedItemsControl.OnHeaderPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.HeaderTemplate" />
    /// dependency property.
    /// </summary>
    /// <value>
    /// The identifier for the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.HeaderTemplate" />
    /// dependency property.
    /// </value>
    /// <remarks>
    /// Note: WPF defines this property via a call to AddOwner of
    /// HeaderedContentControl's HeaderTemplateProperty.
    /// </remarks>
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(nameof (HeaderTemplate), typeof (DataTemplate), typeof (HeaderedItemsControl), new PropertyMetadata(new PropertyChangedCallback(HeaderedItemsControl.OnHeaderTemplatePropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.ItemContainerStyle" />
    /// dependency property.
    /// </summary>
    /// <value>
    /// The identifier for the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.ItemContainerStyle" />
    /// dependency property.
    /// </value>
    public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(nameof (ItemContainerStyle), typeof (Style), typeof (HeaderedItemsControl), new PropertyMetadata((object) null, new PropertyChangedCallback(HeaderedItemsControl.OnItemContainerStylePropertyChanged)));

    /// <summary>
    /// Gets or sets a value indicating whether the Header property has been
    /// set to the item of an ItemsControl.
    /// </summary>
    internal bool HeaderIsItem { get; set; }

    /// <summary>Gets or sets the item that labels the control.</summary>
    /// <value>
    /// The item that labels the control. The default value is null.
    /// </value>
    public object Header
    {
      get => ((DependencyObject) this).GetValue(HeaderedItemsControl.HeaderProperty);
      set => ((DependencyObject) this).SetValue(HeaderedItemsControl.HeaderProperty, value);
    }

    /// <summary>HeaderProperty property changed handler.</summary>
    /// <param name="d">HeaderedItemsControl that changed its Header.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnHeaderPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      (d as HeaderedItemsControl).OnHeaderChanged(e.OldValue, e.NewValue);
    }

    /// <summary>
    /// Gets or sets a data template that is used to display the contents of
    /// the control's header.
    /// </summary>
    /// <value>
    /// Gets or sets a data template that is used to display the contents of
    /// the control's header. The default is null.
    /// </value>
    public DataTemplate HeaderTemplate
    {
      get => ((DependencyObject) this).GetValue(HeaderedItemsControl.HeaderTemplateProperty) as DataTemplate;
      set => ((DependencyObject) this).SetValue(HeaderedItemsControl.HeaderTemplateProperty, (object) value);
    }

    /// <summary>HeaderTemplateProperty property changed handler.</summary>
    /// <param name="d">
    /// HeaderedItemsControl that changed its HeaderTemplate.
    /// </param>
    /// <param name="e">Event arguments.</param>
    private static void OnHeaderTemplatePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      (d as HeaderedItemsControl).OnHeaderTemplateChanged(e.OldValue as DataTemplate, e.NewValue as DataTemplate);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Style" /> that is
    /// applied to the container element generated for each item.
    /// </summary>
    /// <value>
    /// The <see cref="T:System.Windows.Style" /> that is applied to the
    /// container element generated for each item. The default is null.
    /// </value>
    public Style ItemContainerStyle
    {
      get => ((DependencyObject) this).GetValue(HeaderedItemsControl.ItemContainerStyleProperty) as Style;
      set => ((DependencyObject) this).SetValue(HeaderedItemsControl.ItemContainerStyleProperty, (object) value);
    }

    /// <summary>ItemContainerStyleProperty property changed handler.</summary>
    /// <param name="d">
    /// HeaderedItemsControl that changed its ItemContainerStyle.
    /// </param>
    /// <param name="e">Event arguments.</param>
    private static void OnItemContainerStylePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      (d as HeaderedItemsControl).ItemsControlHelper.UpdateItemContainerStyle(e.NewValue as Style);
    }

    /// <summary>
    /// Gets the ItemsControlHelper that is associated with this control.
    /// </summary>
    internal ItemsControlHelper ItemsControlHelper { get; private set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:System.Windows.Controls.HeaderedItemsControl" /> class.
    /// </summary>
    public HeaderedItemsControl()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (HeaderedItemsControl);
      this.ItemsControlHelper = new ItemsControlHelper((ItemsControl) this);
    }

    /// <summary>
    /// Called when the value of the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.Header" />
    /// property changes.
    /// </summary>
    /// <param name="oldHeader">
    /// The old value of the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.Header" />
    /// property.
    /// </param>
    /// <param name="newHeader">
    /// The new value of the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.Header" />
    /// property.
    /// </param>
    protected virtual void OnHeaderChanged(object oldHeader, object newHeader)
    {
    }

    /// <summary>
    /// Called when the value of the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.HeaderTemplate" />
    /// property changes.
    /// </summary>
    /// <param name="oldHeaderTemplate">
    /// The old value of the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.HeaderTemplate" />
    /// property.
    /// </param>
    /// <param name="newHeaderTemplate">
    /// The new value of the
    /// <see cref="P:System.Windows.Controls.HeaderedItemsControl.HeaderTemplate" />
    /// property.
    /// </param>
    protected virtual void OnHeaderTemplateChanged(
      DataTemplate oldHeaderTemplate,
      DataTemplate newHeaderTemplate)
    {
    }

    /// <summary>
    /// Builds the visual tree for the
    /// <see cref="T:System.Windows.Controls.HeaderedItemsControl" /> when a
    /// new template is applied.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      this.ItemsControlHelper.OnApplyTemplate();
      ((FrameworkElement) this).OnApplyTemplate();
    }

    /// <summary>
    /// Prepares the specified element to display the specified item.
    /// </summary>
    /// <param name="element">
    /// The container element used to display the specified item.
    /// </param>
    /// <param name="item">The content to display.</param>
    protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      ItemsControlHelper.PrepareContainerForItemOverride(element, this.ItemContainerStyle);
      HeaderedItemsControl.PreparePrepareHeaderedItemsControlContainerForItemOverride(element, item, (ItemsControl) this, this.ItemContainerStyle);
      base.PrepareContainerForItemOverride(element, item);
    }

    /// <summary>
    /// Prepares the specified container to display the specified item.
    /// </summary>
    /// <param name="element">
    /// Container element used to display the specified item.
    /// </param>
    /// <param name="item">Specified item to display.</param>
    /// <param name="parent">The parent ItemsControl.</param>
    /// <param name="parentItemContainerStyle">
    /// The ItemContainerStyle for the parent ItemsControl.
    /// </param>
    internal static void PreparePrepareHeaderedItemsControlContainerForItemOverride(
      DependencyObject element,
      object item,
      ItemsControl parent,
      Style parentItemContainerStyle)
    {
      if (!(element is HeaderedItemsControl control))
        return;
      HeaderedItemsControl.PrepareHeaderedItemsControlContainer(control, item, parent, parentItemContainerStyle);
    }

    /// <summary>
    /// Prepare a PrepareHeaderedItemsControlContainer container for an
    /// item.
    /// </summary>
    /// <param name="control">Container to prepare.</param>
    /// <param name="item">Item to be placed in the container.</param>
    /// <param name="parentItemsControl">The parent ItemsControl.</param>
    /// <param name="parentItemContainerStyle">
    /// The ItemContainerStyle for the parent ItemsControl.
    /// </param>
    private static void PrepareHeaderedItemsControlContainer(
      HeaderedItemsControl control,
      object item,
      ItemsControl parentItemsControl,
      Style parentItemContainerStyle)
    {
      if (control == item)
        return;
      DataTemplate itemTemplate = parentItemsControl.ItemTemplate;
      if (itemTemplate != null)
        ((DependencyObject) control).SetValue(ItemsControl.ItemTemplateProperty, (object) itemTemplate);
      if (parentItemContainerStyle != null && HeaderedItemsControl.HasDefaultValue((Control) control, HeaderedItemsControl.ItemContainerStyleProperty))
        ((DependencyObject) control).SetValue(HeaderedItemsControl.ItemContainerStyleProperty, (object) parentItemContainerStyle);
      if (control.HeaderIsItem || HeaderedItemsControl.HasDefaultValue((Control) control, HeaderedItemsControl.HeaderProperty))
      {
        control.Header = item;
        control.HeaderIsItem = true;
      }
      if (itemTemplate != null)
        ((DependencyObject) control).SetValue(HeaderedItemsControl.HeaderTemplateProperty, (object) itemTemplate);
      if (parentItemContainerStyle != null && ((FrameworkElement) control).Style == null)
        ((DependencyObject) control).SetValue(FrameworkElement.StyleProperty, (object) parentItemContainerStyle);
      if (itemTemplate is HierarchicalDataTemplate hierarchicalDataTemplate)
      {
        if (hierarchicalDataTemplate.ItemsSource != null && HeaderedItemsControl.HasDefaultValue((Control) control, ItemsControl.ItemsSourceProperty))
          ((FrameworkElement) control).SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
          {
            Converter = hierarchicalDataTemplate.ItemsSource.Converter,
            ConverterCulture = hierarchicalDataTemplate.ItemsSource.ConverterCulture,
            ConverterParameter = hierarchicalDataTemplate.ItemsSource.ConverterParameter,
            Mode = hierarchicalDataTemplate.ItemsSource.Mode,
            NotifyOnValidationError = hierarchicalDataTemplate.ItemsSource.NotifyOnValidationError,
            Path = hierarchicalDataTemplate.ItemsSource.Path,
            Source = control.Header,
            ValidatesOnExceptions = hierarchicalDataTemplate.ItemsSource.ValidatesOnExceptions
          });
        if (hierarchicalDataTemplate.IsItemTemplateSet && control.ItemTemplate == itemTemplate)
        {
          ((DependencyObject) control).ClearValue(ItemsControl.ItemTemplateProperty);
          if (hierarchicalDataTemplate.ItemTemplate != null)
            control.ItemTemplate = hierarchicalDataTemplate.ItemTemplate;
        }
        if (hierarchicalDataTemplate.IsItemContainerStyleSet && control.ItemContainerStyle == parentItemContainerStyle)
        {
          ((DependencyObject) control).ClearValue(HeaderedItemsControl.ItemContainerStyleProperty);
          if (hierarchicalDataTemplate.ItemContainerStyle != null)
            control.ItemContainerStyle = hierarchicalDataTemplate.ItemContainerStyle;
        }
      }
    }

    /// <summary>
    /// Check whether a control has the default value for a property.
    /// </summary>
    /// <param name="control">The control to check.</param>
    /// <param name="property">The property to check.</param>
    /// <returns>
    /// True if the property has the default value; false otherwise.
    /// </returns>
    private static bool HasDefaultValue(Control control, DependencyProperty property)
    {
      Debug.Assert(control != null, "control should not be null!");
      Debug.Assert(property != null, "property should not be null!");
      return ((DependencyObject) control).ReadLocalValue(property) == DependencyProperty.UnsetValue;
    }
  }
}
