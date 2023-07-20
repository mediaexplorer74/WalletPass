// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.GestureService
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// The GestureService class is the helper for getting and setting GestureListeners
  /// on elements.
  /// </summary>
  public static class GestureService
  {
    /// <summary>
    /// The definition of the GestureListener attached DependencyProperty.
    /// </summary>
    public static readonly DependencyProperty GestureListenerProperty = DependencyProperty.RegisterAttached("GestureListener", typeof (GestureListener), typeof (GestureService), new PropertyMetadata((PropertyChangedCallback) null));

    /// <summary>
    /// Gets a GestureListener for the new element. Will create a new one if necessary.
    /// </summary>
    /// <param name="obj">The object to get the GestureListener from.</param>
    /// <returns>Either the previously existing GestureListener, or a new one.</returns>
    public static GestureListener GetGestureListener(DependencyObject obj) => obj != null ? GestureService.GetGestureListenerInternal(obj, true) : throw new ArgumentNullException(nameof (obj));

    /// <summary>
    /// Gets the GestureListener on an element. If one is not set, can create a new one
    /// so that this will never return null, depending on the state of the createIfMissing
    /// flag.
    /// </summary>
    /// <param name="obj">The object to get the GestureListener from.</param>
    /// <param name="createIfMissing">When this is true, if the attached property was not set on the element, it will create one and set it on the element.</param>
    /// <returns></returns>
    internal static GestureListener GetGestureListenerInternal(
      DependencyObject obj,
      bool createIfMissing)
    {
      GestureListener listenerInternal = (GestureListener) obj.GetValue(GestureService.GestureListenerProperty);
      if (listenerInternal == null && createIfMissing)
      {
        listenerInternal = new GestureListener();
        GestureService.SetGestureListenerInternal(obj, listenerInternal);
      }
      return listenerInternal;
    }

    /// <summary>
    /// Sets the GestureListener on an element. Needed for XAML, but should not be used in code. Use
    /// GetGestureListener instead, which will create a new instance if one is not already set, to
    /// add your handlers to an element.
    /// </summary>
    /// <param name="obj">The object to set the GestureListener on.</param>
    /// <param name="value">The GestureListener.</param>
    [Obsolete("Do not add handlers using this method. Instead, use GetGestureListener, which will create a new instance if one is not already set, to add your handlers to an element.", true)]
    public static void SetGestureListener(DependencyObject obj, GestureListener value)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      GestureService.SetGestureListenerInternal(obj, value);
    }

    /// <summary>
    /// This is used to set the value of the attached DependencyProperty internally.
    /// </summary>
    /// <param name="obj">The object to set the GestureListener on.</param>
    /// <param name="value">The GestureListener.</param>
    private static void SetGestureListenerInternal(DependencyObject obj, GestureListener value) => obj.SetValue(GestureService.GestureListenerProperty, (object) value);
  }
}
