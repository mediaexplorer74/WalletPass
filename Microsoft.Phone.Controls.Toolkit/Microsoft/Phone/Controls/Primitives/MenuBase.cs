// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.MenuBase
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Represents a control that defines choices for users to select.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (MenuItem))]
  public abstract class MenuBase : ItemsControl
  {
    /// <summary>
    /// Identifies the ItemContainerStyle dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(nameof (ItemContainerStyle), typeof (Style), typeof (MenuBase), (PropertyMetadata) null);

    /// <summary>
    /// Gets or sets the Style that is applied to the container element generated for each item.
    /// </summary>
    public Style ItemContainerStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(MenuBase.ItemContainerStyleProperty);
      set => ((DependencyObject) this).SetValue(MenuBase.ItemContainerStyleProperty, (object) value);
    }

    /// <summary>
    /// Determines whether the specified item is, or is eligible to be, its own item container.
    /// </summary>
    /// <param name="item">The item to check whether it is an item container.</param>
    /// <returns>True if the item is a MenuItem or a Separator; otherwise, false.</returns>
    protected virtual bool IsItemItsOwnContainerOverride(object item) => item is MenuItem || item is Separator;

    /// <summary>
    /// Creates or identifies the element used to display the specified item.
    /// </summary>
    /// <returns>A MenuItem.</returns>
    protected virtual DependencyObject GetContainerForItemOverride() => (DependencyObject) new MenuItem();

    /// <summary>
    /// Prepares the specified element to display the specified item.
    /// </summary>
    /// <param name="element">Element used to display the specified item.</param>
    /// <param name="item">Specified item.</param>
    protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      base.PrepareContainerForItemOverride(element, item);
      MenuItem menuItem = element as MenuItem;
      if (null == menuItem)
        return;
      menuItem.ParentMenuBase = this;
      if (menuItem != item)
      {
        DataTemplate itemTemplate = this.ItemTemplate;
        Style itemContainerStyle = this.ItemContainerStyle;
        if (itemTemplate != null)
          ((DependencyObject) menuItem).SetValue(ItemsControl.ItemTemplateProperty, (object) itemTemplate);
        if (itemContainerStyle != null && MenuBase.HasDefaultValue((Control) menuItem, HeaderedItemsControl.ItemContainerStyleProperty))
          ((DependencyObject) menuItem).SetValue(HeaderedItemsControl.ItemContainerStyleProperty, (object) itemContainerStyle);
        if (MenuBase.HasDefaultValue((Control) menuItem, HeaderedItemsControl.HeaderProperty))
          menuItem.Header = item;
        if (itemTemplate != null)
          ((DependencyObject) menuItem).SetValue(HeaderedItemsControl.HeaderTemplateProperty, (object) itemTemplate);
        if (itemContainerStyle != null)
          ((DependencyObject) menuItem).SetValue(FrameworkElement.StyleProperty, (object) itemContainerStyle);
      }
    }

    /// <summary>
    /// Checks whether a control has the default value for a property.
    /// </summary>
    /// <param name="control">The control to check.</param>
    /// <param name="property">The property to check.</param>
    /// <returns>True if the property has the default value; false otherwise.</returns>
    private static bool HasDefaultValue(Control control, DependencyProperty property) => ((DependencyObject) control).ReadLocalValue(property) == DependencyProperty.UnsetValue;
  }
}
