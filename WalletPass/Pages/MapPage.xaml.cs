// Decompiled with JetBrains decompiler
// Type: WalletPass.MapPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Shell;
using Nokia.Phone.HereLaunchers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WalletPass
{
  public class MapPage : PhoneApplicationPage
  {
    private List<ClaseLocations> currentLocations = new List<ClaseLocations>();
    private ReverseGeocodeQuery _reverseGeocodeQuery = new ReverseGeocodeQuery();
    private ClaseLocations _selectedLocation = new ClaseLocations();
    private bool _showInfoMap;
    private bool isTombstoned = true;
    private ApplicationBarIconButton _btnHere;
    internal Grid LayoutRoot;
    internal Map map;
    internal Grid geoInfo;
    internal TextBlock txtBlock1;
    internal TextBlock txtBlock2;
    internal TextBlock txtBlock3;
    private bool _contentLoaded;

    public MapPage()
    {
      this.InitializeComponent();
      this._showInfoMap = false;
      this._btnHere = new ApplicationBarIconButton();
      this._btnHere.IconUri = new Uri("/Assets/AppBar/appbar.here.png", UriKind.Relative);
      this._btnHere.Text = "here Maps";
      this._btnHere.IsEnabled = false;
      this._btnHere.Click += new EventHandler(this.btnHere_Click);
      ((UIElement) this).Opacity = 0.0;
      this.ApplicationBar.Buttons.Add((object) this._btnHere);
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush3 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorMain, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      this.ApplicationBar.BackgroundColor = solidColorBrush3.Color;
      this.ApplicationBar.ForegroundColor = solidColorBrush2.Color;
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        else
          ((UIElement) this).Opacity = 1.0;
      }
      else
      {
        StateManager.LoadStateAll((PhoneApplicationPage) this);
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
        App._reconstructPages = true;
        this.ApplicationBar.IsVisible = true;
      }
      if (App._groupItemIndex != -1)
      {
        foreach (ClaseLocations location in App._tempPassGroup[App._groupItemIndex].Locations)
          this.currentLocations.Add(location);
      }
      else
      {
        foreach (ClaseLocations location in App._tempPassClass.Locations)
          this.currentLocations.Add(location);
      }
      if (this.currentLocations.Count == 1)
      {
        this._selectedLocation = this.currentLocations[0];
        this._btnHere.IsEnabled = true;
      }
      this.DrawMapMarkers();
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      if (!this.isTombstoned)
        return;
      StateManager.SaveStateAll((PhoneApplicationPage) this);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      if (this._showInfoMap)
      {
        e.Cancel = true;
        this._showInfoMap = false;
        if (this.currentLocations.Count > 1)
        {
          this._btnHere.IsEnabled = false;
          this._selectedLocation = (ClaseLocations) null;
        }
        ((UIElement) this.geoInfo).Visibility = (Visibility) 1;
        for (double num = 15.0; num >= 10.0; num += -0.001)
          this.map.ZoomLevel = num;
      }
      else
      {
        this.showTransitionOutBackward();
        this.isTombstoned = false;
      }
    }

    private void DrawMapMarkers()
    {
      this.map.Layers.Clear();
      MapLayer mapLayer = new MapLayer();
      foreach (ClaseLocations currentLocation in this.currentLocations)
      {
        if (currentLocation != null)
        {
          this.DrawAccuracyRadius(mapLayer, new GeoCoordinate(currentLocation.locLatitude, currentLocation.locLongitude, currentLocation.locAltitude));
          this.DrawMapMarker(currentLocation, Colors.Red, mapLayer);
        }
      }
      this.map.Layers.Add(mapLayer);
      this.map.Center = new GeoCoordinate(this.currentLocations[0].locLatitude, this.currentLocations[0].locLongitude);
      this.map.ZoomLevel = 10.0;
    }

    private void DrawMapMarker(ClaseLocations coordinate, Color color, MapLayer mapLayer)
    {
      Ellipse ellipse = new Ellipse();
      ((Shape) ellipse).Fill = (Brush) new SolidColorBrush(Colors.White);
      ((Shape) ellipse).Stroke = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 107, (byte) 168));
      ((Shape) ellipse).StrokeThickness = 17.0;
      ((UIElement) ellipse).Opacity = 0.7;
      ((FrameworkElement) ellipse).Height = 45.0;
      ((FrameworkElement) ellipse).Width = 45.0;
      ((FrameworkElement) ellipse).Tag = (object) new ClaseLocations(coordinate.locLatitude, coordinate.locLongitude, coordinate.locText);
      ((UIElement) ellipse).Tap += new EventHandler<GestureEventArgs>(this.cnv_CanvasTap);
      ((Collection<MapOverlay>) mapLayer).Add(new MapOverlay()
      {
        Content = (object) ellipse,
        GeoCoordinate = new GeoCoordinate(coordinate.locLatitude, coordinate.locLongitude),
        PositionOrigin = new Point(0.0, 1.0)
      });
    }

    private void cnv_CanvasTap(object sender, GestureEventArgs e)
    {
      Ellipse ellipse = (Ellipse) sender;
      ClaseLocations tag = (ClaseLocations) ((FrameworkElement) ellipse).Tag;
      this.map.Center = new GeoCoordinate(tag.locLatitude, tag.locLongitude, tag.locAltitude);
      for (double num = 10.0; num <= 15.0; num += 0.001)
        this.map.ZoomLevel = num;
      this._selectedLocation = (ClaseLocations) ((FrameworkElement) ellipse).Tag;
      this._btnHere.IsEnabled = true;
      this._reverseGeocodeQuery = new ReverseGeocodeQuery();
      this._reverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(tag.locLatitude, tag.locLongitude);
      ((Query<IList<MapLocation>>) this._reverseGeocodeQuery).QueryCompleted += new EventHandler<QueryCompletedEventArgs<IList<MapLocation>>>(this._reverseGeocodeQuery_Completed);
      ((Query<IList<MapLocation>>) this._reverseGeocodeQuery).QueryAsync();
    }

    private void _reverseGeocodeQuery_Completed(
      object sender,
      QueryCompletedEventArgs<IList<MapLocation>> e)
    {
      if (((AsyncCompletedEventArgs) e).Error != null || e.Result.Count <= 0)
        return;
      MapAddress address = e.Result[0].Information.Address;
      ((UIElement) this.geoInfo).Visibility = (Visibility) 0;
      this._showInfoMap = true;
      this.txtBlock1.Text = this._selectedLocation.locText;
      this.txtBlock2.Text = address.Street + " " + address.HouseNumber;
      this.txtBlock3.Text = address.PostalCode + ", " + address.State;
    }

    private void DrawAccuracyRadius(MapLayer maplayer, GeoCoordinate coordinate)
    {
      double num = 90.0 / (Math.Cos(coordinate.Latitude * Math.PI / 180.0) * 2.0 * Math.PI * 6378137.0 / (256.0 * Math.Pow(2.0, this.map.ZoomLevel)));
      Ellipse ellipse = new Ellipse();
      ((FrameworkElement) ellipse).Width = num * 2.0;
      ((FrameworkElement) ellipse).Height = num * 2.0;
      ((Shape) ellipse).Fill = (Brush) new SolidColorBrush(Color.FromArgb((byte) 75, (byte) 200, (byte) 0, (byte) 0));
      ((Collection<MapOverlay>) maplayer).Add(new MapOverlay()
      {
        Content = (object) ellipse,
        GeoCoordinate = coordinate,
        PositionOrigin = new Point(0.5, 0.5)
      });
    }

    private void btnHere_Click(object sender, EventArgs e) => new ExploremapsShowPlaceTask()
    {
      Location = new GeoCoordinate(this._selectedLocation.locLatitude, this._selectedLocation.locLongitude),
      Zoom = 15.0,
      Title = this._selectedLocation.locText
    }.Show();

    private void map_Loaded(object sender, RoutedEventArgs e)
    {
      MapsSettings.ApplicationContext.ApplicationId = "61e56c4f-08e0-4427-af8d-7253603353cb";
      MapsSettings.ApplicationContext.AuthenticationToken = "ljFlW3fy45ZWIIzE9FOPEg";
    }

    private void showTransitionOutBackward()
    {
      OpacityTransition opacityTransition = new OpacityTransition();
      opacityTransition.Mode = OpacityTransitionMode.TransitionOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      opacityTransition.GetTransition((UIElement) element).Begin();
    }

    private void showTransitionInForward()
    {
      OpacityTransition opacityTransition = new OpacityTransition();
      opacityTransition.Mode = OpacityTransitionMode.TransitionIn;
      PhoneApplicationPage content = (PhoneApplicationPage) ((ContentControl) Application.Current.RootVisual).Content;
      ITransition transition = opacityTransition.GetTransition((UIElement) content);
      transition.Completed += (EventHandler) ((param0, param1) =>
      {
        ((UIElement) this).Opacity = 1.0;
        this.ApplicationBar.IsVisible = true;
      });
      transition.Begin();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/MapPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.map = (Map) ((FrameworkElement) this).FindName("map");
      this.geoInfo = (Grid) ((FrameworkElement) this).FindName("geoInfo");
      this.txtBlock1 = (TextBlock) ((FrameworkElement) this).FindName("txtBlock1");
      this.txtBlock2 = (TextBlock) ((FrameworkElement) this).FindName("txtBlock2");
      this.txtBlock3 = (TextBlock) ((FrameworkElement) this).FindName("txtBlock3");
    }
  }
}
