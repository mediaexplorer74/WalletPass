// Decompiled with JetBrains decompiler
// Type: System.Windows.HierarchicalDataTemplate
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows.Data;

namespace System.Windows
{
  /// <summary>
  /// Represents a <see cref="T:System.Windows.DataTemplate" /> that supports
  /// <see cref="T:System.Windows.Controls.HeaderedItemsControl" /> objects,
  /// such as <see cref="T:System.Windows.Controls.TreeViewItem" />.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  public class HierarchicalDataTemplate : DataTemplate
  {
    /// <summary>
    /// The DataTemplate to apply to the ItemTemplate property on a
    /// generated HeaderedItemsControl (such as a MenuItem or a
    /// TreeViewItem), to indicate how to display items from the next level
    /// in the data hierarchy.
    /// </summary>
    private DataTemplate _itemTemplate;
    /// <summary>
    /// The Style to apply to the ItemContainerStyle property on a generated
    /// HeaderedItemsControl (such as a MenuItem or a TreeViewItem), to
    /// indicate how to style items from the next level in the data
    /// hierarchy.
    /// </summary>
    private Style _itemContainerStyle;

    /// <summary>
    /// Gets or sets the collection that is used to generate content for the
    /// next sublevel in the data hierarchy.
    /// </summary>
    /// <value>
    /// The collection that is used to generate content for the next
    /// sublevel in the data hierarchy.  The default value is null.
    /// </value>
    public Binding ItemsSource { get; set; }

    /// <summary>
    /// Gets a value indicating whether the ItemTemplate property was set on
    /// the template.
    /// </summary>
    internal bool IsItemTemplateSet { get; private set; }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DataTemplate" /> to
    /// apply to the
    /// <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate" />
    /// property on a generated
    /// <see cref="T:System.Windows.Controls.HeaderedItemsControl" />, such
    /// as a <see cref="T:System.Windows.Controls.TreeViewItem" />, to
    /// indicate how to display items from the next sublevel in the data
    /// hierarchy.
    /// </summary>
    /// <value>
    /// The <see cref="T:System.Windows.DataTemplate" /> to apply to the
    /// <see cref="P:System.Windows.Controls.ItemsControl.ItemTemplate" />
    /// property on a generated
    /// <see cref="T:System.Windows.Controls.HeaderedItemsControl" />, such
    /// as a <see cref="T:System.Windows.Controls.TreeViewItem" />, to
    /// indicate how to display items from the next sublevel in the data
    /// hierarchy.
    /// </value>
    public DataTemplate ItemTemplate
    {
      get => this._itemTemplate;
      set
      {
        this.IsItemTemplateSet = true;
        this._itemTemplate = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the ItemContainerStyle property was
    /// set on the template.
    /// </summary>
    internal bool IsItemContainerStyleSet { get; private set; }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Style" /> that is
    /// applied to the item container for each child item.
    /// </summary>
    /// <value>
    /// The style that is applied to the item container for each child item.
    /// </value>
    public Style ItemContainerStyle
    {
      get => this._itemContainerStyle;
      set
      {
        this.IsItemContainerStyleSet = true;
        this._itemContainerStyle = value;
      }
    }
  }
}
