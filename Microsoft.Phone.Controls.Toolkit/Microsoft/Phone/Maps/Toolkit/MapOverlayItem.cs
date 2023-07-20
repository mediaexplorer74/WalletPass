// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapOverlayItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Maps.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// MapOverlayItem class
  /// This class helps with the task to create the target item to be presented when a template is provided.
  /// This target item will have bindings to the MapOverlay.
  /// When the item has been resolved (from template + content), this class will take care of creating the bindings.
  /// When there is no template and the content is the UI, it will follow the same pattern
  /// to wait until item is visible before binding the dependency properties.
  /// </summary>
  internal class MapOverlayItem : ContentPresenter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.MapOverlayItem" /> class
    /// </summary>
    /// <param name="content">Content to be used</param>
    /// <param name="contentTemplate">Content template</param>
    /// <param name="mapOverlay">MapOverlay that will be used to bind the dependency properties when content becomes visible</param>
    public MapOverlayItem(object content, DataTemplate contentTemplate, MapOverlay mapOverlay)
    {
      this.ContentTemplate = contentTemplate;
      this.Content = content;
      this.MapOverlay = mapOverlay;
    }

    /// <summary>
    /// Gets or sets the MapOverlay that will be used to bind the properties
    /// </summary>
    private MapOverlay MapOverlay { get; set; }

    /// <summary>
    /// OnApplyTemplate override.
    /// Will take care of binding the dependency properties.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      MapChild.BindMapOverlayProperties(this.MapOverlay);
    }
  }
}
