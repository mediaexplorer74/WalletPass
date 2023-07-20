// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TransitionService
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides attached properties for navigation
  /// <see cref="T:Microsoft.Phone.Controls.ITransition" />s.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public static class TransitionService
  {
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the in <see cref="T:Microsoft.Phone.Controls.ITransition" />s.
    /// </summary>
    public static readonly DependencyProperty NavigationInTransitionProperty = DependencyProperty.RegisterAttached("NavigationInTransition", typeof (NavigationInTransition), typeof (TransitionService), (PropertyMetadata) null);
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the in <see cref="T:Microsoft.Phone.Controls.ITransition" />s.
    /// </summary>
    public static readonly DependencyProperty NavigationOutTransitionProperty = DependencyProperty.RegisterAttached("NavigationOutTransition", typeof (NavigationOutTransition), typeof (TransitionService), (PropertyMetadata) null);

    /// <summary>
    /// Gets the
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />s
    /// of
    /// <see cref="M:Microsoft.Phone.Controls.TransitionService.NavigationInTransitionProperty" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The </returns>
    public static NavigationInTransition GetNavigationInTransition(UIElement element) => element != null ? (NavigationInTransition) ((DependencyObject) element).GetValue(TransitionService.NavigationInTransitionProperty) : throw new ArgumentNullException(nameof (element));

    /// <summary>
    /// Gets the
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />s
    /// of
    /// <see cref="M:Microsoft.Phone.Controls.TransitionService.NavigationOutTransitionProperty" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The </returns>
    public static NavigationOutTransition GetNavigationOutTransition(UIElement element) => element != null ? (NavigationOutTransition) ((DependencyObject) element).GetValue(TransitionService.NavigationOutTransitionProperty) : throw new ArgumentNullException(nameof (element));

    /// <summary>
    /// Sets a
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />
    /// to
    /// <see cref="M:Microsoft.Phone.Controls.TransitionService.NavigationInTransitionProperty" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="value">The <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />.</param>
    /// <returns>The </returns>
    public static void SetNavigationInTransition(UIElement element, NavigationInTransition value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      ((DependencyObject) element).SetValue(TransitionService.NavigationInTransitionProperty, (object) value);
    }

    /// <summary>
    /// Sets a
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />s
    /// to
    /// <see cref="M:Microsoft.Phone.Controls.TransitionService.NavigationOutTransitionProperty" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="value">The <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />.</param>
    /// <returns>The </returns>
    public static void SetNavigationOutTransition(UIElement element, NavigationOutTransition value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      ((DependencyObject) element).SetValue(TransitionService.NavigationOutTransitionProperty, (object) value);
    }
  }
}
