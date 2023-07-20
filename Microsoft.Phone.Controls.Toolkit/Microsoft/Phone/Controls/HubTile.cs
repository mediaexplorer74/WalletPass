// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.HubTile
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents an animated tile that supports an image and a title.
  /// Furthermore, it can also be associated with a message or a notification.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(Name = "Expanded", GroupName = "ImageState")]
  [TemplatePart(Name = "MessageBlock", Type = typeof (TextBlock))]
  [TemplatePart(Name = "BackTitleBlock", Type = typeof (TextBlock))]
  [TemplatePart(Name = "TitlePanel", Type = typeof (Panel))]
  [TemplateVisualState(Name = "Collapsed", GroupName = "ImageState")]
  [TemplatePart(Name = "NotificationBlock", Type = typeof (TextBlock))]
  [TemplateVisualState(Name = "Semiexpanded", GroupName = "ImageState")]
  [TemplateVisualState(Name = "Flipped", GroupName = "ImageState")]
  public class HubTile : Control
  {
    /// <summary>Common visual states.</summary>
    private const string ImageStates = "ImageState";
    /// <summary>Expanded visual state.</summary>
    private const string Expanded = "Expanded";
    /// <summary>Semiexpanded visual state.</summary>
    private const string Semiexpanded = "Semiexpanded";
    /// <summary>Collapsed visual state.</summary>
    private const string Collapsed = "Collapsed";
    /// <summary>Flipped visual state.</summary>
    private const string Flipped = "Flipped";
    /// <summary>Nofitication Block template part name.</summary>
    private const string NotificationBlock = "NotificationBlock";
    /// <summary>Message Block template part name.</summary>
    private const string MessageBlock = "MessageBlock";
    /// <summary>Back Title Block template part name.</summary>
    private const string BackTitleBlock = "BackTitleBlock";
    /// <summary>Title Panel template part name.</summary>
    private const string TitlePanel = "TitlePanel";
    /// <summary>Notification Block template part.</summary>
    private TextBlock _notificationBlock;
    /// <summary>Message Block template part.</summary>
    private TextBlock _messageBlock;
    /// <summary>Title Panel template part.</summary>
    private Panel _titlePanel;
    /// <summary>Back Title Block template part.</summary>
    private TextBlock _backTitleBlock;
    /// <summary>
    /// Represents the number of steps inside the pipeline of stalled images
    /// </summary>
    internal int _stallingCounter;
    /// <summary>
    /// Flag that determines if the hub tile has a primary text string associated to it.
    /// If it does not, the hub tile will not drop.
    /// </summary>
    internal bool _canDrop;
    /// <summary>
    /// Flag that determines if the hub tile has a secondary text string associated to it.
    /// If it does not, the hub tile will not flip.
    /// </summary>
    internal bool _canFlip;
    /// <summary>Identifies the Source dependency property.</summary>
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof (Source), typeof (ImageSource), typeof (HubTile), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>Identifies the Title dependency property.</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (HubTile), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(HubTile.OnTitleChanged)));
    /// <summary>Identifies the Notification dependency property.</summary>
    public static readonly DependencyProperty NotificationProperty = DependencyProperty.Register(nameof (Notification), typeof (string), typeof (HubTile), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(HubTile.OnBackContentChanged)));
    /// <summary>Identifies the Message dependency property.</summary>
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof (Message), typeof (string), typeof (HubTile), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(HubTile.OnBackContentChanged)));
    /// <summary>
    /// Identifies the DisplayNotification dependency property.
    /// </summary>
    public static readonly DependencyProperty DisplayNotificationProperty = DependencyProperty.Register(nameof (DisplayNotification), typeof (bool), typeof (HubTile), new PropertyMetadata((object) false, new PropertyChangedCallback(HubTile.OnBackContentChanged)));
    /// <summary>Identifies the IsFrozen dependency property.</summary>
    public static readonly DependencyProperty IsFrozenProperty = DependencyProperty.Register(nameof (IsFrozen), typeof (bool), typeof (HubTile), new PropertyMetadata((object) false, new PropertyChangedCallback(HubTile.OnIsFrozenChanged)));
    /// <summary>Identifies the GroupTag dependency property.</summary>
    public static readonly DependencyProperty GroupTagProperty = DependencyProperty.Register(nameof (GroupTag), typeof (string), typeof (HubTile), new PropertyMetadata((object) string.Empty));
    /// <summary>Identifies the State dependency property.</summary>
    private static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof (State), typeof (ImageState), typeof (HubTile), new PropertyMetadata((object) ImageState.Expanded, new PropertyChangedCallback(HubTile.OnImageStateChanged)));
    /// <summary>Identifies the State dependency property.</summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(nameof (Size), typeof (TileSize), typeof (HubTile), new PropertyMetadata((object) TileSize.Default, new PropertyChangedCallback(HubTile.OnSizeChanged)));

    /// <summary>Gets or sets the image source.</summary>
    public ImageSource Source
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(HubTile.SourceProperty);
      set => ((DependencyObject) this).SetValue(HubTile.SourceProperty, (object) value);
    }

    /// <summary>Gets or sets the title.</summary>
    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(HubTile.TitleProperty);
      set => ((DependencyObject) this).SetValue(HubTile.TitleProperty, (object) value);
    }

    /// <summary>
    /// Prevents the hub tile from transitioning into a Semiexpanded or Collapsed visual state if the title is not set.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      HubTile hubTile = (HubTile) obj;
      if (string.IsNullOrEmpty((string) e.NewValue))
      {
        hubTile._canDrop = false;
        hubTile.State = ImageState.Expanded;
      }
      else
        hubTile._canDrop = true;
    }

    /// <summary>Gets or sets the notification alert.</summary>
    public string Notification
    {
      get => (string) ((DependencyObject) this).GetValue(HubTile.NotificationProperty);
      set => ((DependencyObject) this).SetValue(HubTile.NotificationProperty, (object) value);
    }

    /// <summary>
    /// Prevents the hub tile from transitioning into a Flipped visual state if neither the notification alert nor the message are set.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnBackContentChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      HubTile hubTile = (HubTile) obj;
      if (!string.IsNullOrEmpty(hubTile.Notification) && hubTile.DisplayNotification || !string.IsNullOrEmpty(hubTile.Message) && !hubTile.DisplayNotification)
      {
        hubTile._canFlip = true;
      }
      else
      {
        hubTile._canFlip = false;
        hubTile.State = ImageState.Expanded;
      }
    }

    /// <summary>Gets or sets the message.</summary>
    public string Message
    {
      get => (string) ((DependencyObject) this).GetValue(HubTile.MessageProperty);
      set => ((DependencyObject) this).SetValue(HubTile.MessageProperty, (object) value);
    }

    /// <summary>Gets or sets the flag for new notifications.</summary>
    public bool DisplayNotification
    {
      get => (bool) ((DependencyObject) this).GetValue(HubTile.DisplayNotificationProperty);
      set => ((DependencyObject) this).SetValue(HubTile.DisplayNotificationProperty, (object) value);
    }

    /// <summary>Gets or sets the flag for images that do not animate.</summary>
    public bool IsFrozen
    {
      get => (bool) ((DependencyObject) this).GetValue(HubTile.IsFrozenProperty);
      set => ((DependencyObject) this).SetValue(HubTile.IsFrozenProperty, (object) value);
    }

    /// <summary>
    /// Removes the frozen image from the enabled image pool or the stalled image pipeline.
    /// Adds the non-frozen image to the enabled image pool.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsFrozenChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      HubTile tile = (HubTile) obj;
      if ((bool) e.NewValue)
        HubTileService.FreezeHubTile(tile);
      else
        HubTileService.UnfreezeHubTile(tile);
    }

    /// <summary>Gets or sets the group tag.</summary>
    public string GroupTag
    {
      get => (string) ((DependencyObject) this).GetValue(HubTile.GroupTagProperty);
      set => ((DependencyObject) this).SetValue(HubTile.GroupTagProperty, (object) value);
    }

    /// <summary>Gets or sets the visual state.</summary>
    internal ImageState State
    {
      get => (ImageState) ((DependencyObject) this).GetValue(HubTile.StateProperty);
      set => ((DependencyObject) this).SetValue(HubTile.StateProperty, (object) value);
    }

    /// <summary>Triggers the transition between visual states.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnImageStateChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((HubTile) obj).UpdateVisualState();
    }

    /// <summary>Gets or sets the visual state.</summary>
    public TileSize Size
    {
      get => (TileSize) ((DependencyObject) this).GetValue(HubTile.SizeProperty);
      set => ((DependencyObject) this).SetValue(HubTile.SizeProperty, (object) value);
    }

    /// <summary>Triggers the transition between visual states.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      HubTile tile = (HubTile) obj;
      switch (tile.Size)
      {
        case TileSize.Default:
          ((FrameworkElement) tile).Width = 173.0;
          ((FrameworkElement) tile).Height = 173.0;
          break;
        case TileSize.Small:
          ((FrameworkElement) tile).Width = 99.0;
          ((FrameworkElement) tile).Height = 99.0;
          break;
        case TileSize.Medium:
          ((FrameworkElement) tile).Width = 210.0;
          ((FrameworkElement) tile).Height = 210.0;
          break;
        case TileSize.Large:
          ((FrameworkElement) tile).Width = 432.0;
          ((FrameworkElement) tile).Height = 210.0;
          break;
      }
      ((FrameworkElement) tile).SizeChanged += new SizeChangedEventHandler(HubTile.OnHubTileSizeChanged);
      HubTileService.FinalizeReference(tile);
    }

    private static void OnHubTileSizeChanged(object sender, SizeChangedEventArgs e)
    {
      HubTile tile = (HubTile) sender;
      ((FrameworkElement) tile).SizeChanged -= new SizeChangedEventHandler(HubTile.OnHubTileSizeChanged);
      if (tile.State != ImageState.Expanded)
      {
        tile.State = ImageState.Expanded;
        VisualStateManager.GoToState((Control) tile, "Expanded", false);
      }
      else if (tile._titlePanel != null && ((UIElement) tile._titlePanel).RenderTransform is CompositeTransform renderTransform)
        renderTransform.TranslateY = -((FrameworkElement) tile).Height;
      HubTileService.InitializeReference(tile);
    }

    /// <summary>Updates the visual state.</summary>
    private void UpdateVisualState()
    {
      string str;
      if (this.Size != TileSize.Small)
      {
        switch (this.State)
        {
          case ImageState.Expanded:
            str = "Expanded";
            break;
          case ImageState.Semiexpanded:
            str = "Semiexpanded";
            break;
          case ImageState.Collapsed:
            str = "Collapsed";
            break;
          case ImageState.Flipped:
            str = "Flipped";
            break;
          default:
            str = "Expanded";
            break;
        }
      }
      else
        str = "Expanded";
      VisualStateManager.GoToState((Control) this, str, true);
    }

    /// <summary>Gets the template parts and sets binding.</summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this._notificationBlock = this.GetTemplateChild("NotificationBlock") as TextBlock;
      this._messageBlock = this.GetTemplateChild("MessageBlock") as TextBlock;
      this._backTitleBlock = this.GetTemplateChild("BackTitleBlock") as TextBlock;
      this._titlePanel = this.GetTemplateChild("TitlePanel") as Panel;
      if (this._notificationBlock != null)
        ((FrameworkElement) this._notificationBlock).SetBinding(UIElement.VisibilityProperty, new Binding()
        {
          Source = (object) this,
          Path = new PropertyPath("DisplayNotification", new object[0]),
          Converter = (IValueConverter) new VisibilityConverter(),
          ConverterParameter = (object) false
        });
      if (this._messageBlock != null)
        ((FrameworkElement) this._messageBlock).SetBinding(UIElement.VisibilityProperty, new Binding()
        {
          Source = (object) this,
          Path = new PropertyPath("DisplayNotification", new object[0]),
          Converter = (IValueConverter) new VisibilityConverter(),
          ConverterParameter = (object) true
        });
      if (this._backTitleBlock != null)
        ((FrameworkElement) this._backTitleBlock).SetBinding(TextBlock.TextProperty, new Binding()
        {
          Source = (object) this,
          Path = new PropertyPath("Title", new object[0]),
          Converter = (IValueConverter) new MultipleToSingleLineStringConverter()
        });
      this.UpdateVisualState();
    }

    /// <summary>Initializes a new instance of the HubTile class.</summary>
    public HubTile()
    {
      this.DefaultStyleKey = (object) typeof (HubTile);
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.HubTile_Loaded);
      ((FrameworkElement) this).Unloaded += new RoutedEventHandler(this.HubTile_Unloaded);
    }

    /// <summary>
    /// This event handler gets called as soon as a hub tile is added to the visual tree.
    /// A reference of this hub tile is passed on to the service singleton.
    /// </summary>
    /// <param name="sender">The hub tile.</param>
    /// <param name="e">The event information.</param>
    private void HubTile_Loaded(object sender, RoutedEventArgs e) => HubTileService.InitializeReference(this);

    /// <summary>
    /// This event handler gets called as soon as a hub tile is removed from the visual tree.
    /// Any existing reference of this hub tile is eliminated from the service singleton.
    /// </summary>
    /// <param name="sender">The hub tile.</param>
    /// <param name="e">The event information.</param>
    private void HubTile_Unloaded(object sender, RoutedEventArgs e) => HubTileService.FinalizeReference(this);
  }
}
