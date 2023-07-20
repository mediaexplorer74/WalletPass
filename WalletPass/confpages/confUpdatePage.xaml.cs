// Decompiled with JetBrains decompiler
// Type: WalletPass.confUpdatePage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WalletPass.Resources;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WalletPass
{
  public sealed partial class confUpdatePage : Page
  {
    internal Grid LayoutRoot;
    internal Button btnSearchUpdate;
    internal Button btnSearchPrueba;
    private bool _contentLoaded;

    public confUpdatePage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        else
          ((UIElement) this).Opacity = 1.0;
      }
      else
      {
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
      }
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      this.showTransitionOutBackward();
      base.OnBackKeyPress(e);
    }

    private async void btnSearchUpdate_Tap(object sender, GestureEventArgs e)
    {
      bool hasUpdates = false;
      HttpResponseMessage x = new HttpResponseMessage();
      for (int i = 0; i < ((Collection<ClasePass>) App._passcollection).Count; ++i)
      {
        try
        {
          HttpClient htp = new HttpClient();
          htp.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset?((DateTimeOffset) ((Collection<ClasePass>) App._passcollection)[i].dateModified);
          htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApplePass", ((Collection<ClasePass>) App._passcollection)[i].authenticationToken);
          if (((string) ((Collection<ClasePass>) App._passcollection)[i].webServiceURL).Substring(((string) ((Collection<ClasePass>) App._passcollection)[i].webServiceURL).Length - 1, 1) != "/")
          {
            ClasePass clasePass = ((Collection<ClasePass>) App._passcollection)[i];
            clasePass.webServiceURL = string.Concat(clasePass.webServiceURL, "/");
          }
          x = new HttpResponseMessage();
          x = await htp.GetAsync(string.Format("{0}v1/passes/{1}/{2}", (object) ((Collection<ClasePass>) App._passcollection)[i].webServiceURL, (object) ((Collection<ClasePass>) App._passcollection)[i].passTypeIdentifier, (object) ((Collection<ClasePass>) App._passcollection)[i].serialNumber), HttpCompletionOption.ResponseHeadersRead);
          hasUpdates |= x.IsSuccessStatusCode;
          if (x.IsSuccessStatusCode)
            App._updatePassCollection.addDeleteDoubles(((Collection<ClasePass>) App._passcollection)[i]);
        }
        catch (Exception ex)
        {
        }
      }
      if (!hasUpdates)
        return;
      XmlDocument templateContent = ToastNotificationManager.GetTemplateContent((ToastTemplateType) 5);
      XmlNodeList elementsByTagName = templateContent.GetElementsByTagName("text");
      ((IReadOnlyList<IXmlNode>) elementsByTagName)[0].AppendChild((IXmlNode) templateContent.CreateTextNode(AppResources.toastUpdateHeader));
      ((IReadOnlyList<IXmlNode>) elementsByTagName)[1].AppendChild((IXmlNode) templateContent.CreateTextNode(AppResources.toastUpdateText));
      string str = "#/MainPage.xaml";
      ((XmlElement) templateContent.SelectSingleNode("/toast")).SetAttribute("launch", str);
      ToastNotification toastNotification = new ToastNotification(templateContent);
      ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
      IO.SaveUpdateData(App._updatePassCollection);
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
      transition.Completed += (EventHandler) ((param0, param1) => ((UIElement) this).Opacity = 1.0);
      transition.Begin();
    }

    private void showTransitionTurnstile()
    {
      TurnstileTransition turnstileTransition = new TurnstileTransition();
      turnstileTransition.Mode = TurnstileTransitionMode.ForwardOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      turnstileTransition.GetTransition((UIElement) element).Begin();
    }

    private async void btnSearchPrueba_Click(object sender, RoutedEventArgs e)
    {
    }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confUpdatePage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.btnSearchUpdate = (Button) ((FrameworkElement) this).FindName("btnSearchUpdate");
      this.btnSearchPrueba = (Button) ((FrameworkElement) this).FindName("btnSearchPrueba");
    }
        */
  }
}
