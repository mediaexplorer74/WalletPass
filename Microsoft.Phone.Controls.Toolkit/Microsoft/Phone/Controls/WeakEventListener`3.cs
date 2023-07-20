// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.WeakEventListener`3
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Implements a weak event listener that allows the owner to be garbage
  /// collected if its only remaining link is an event handler.
  /// </summary>
  /// <typeparam name="TInstance">Type of instance listening for the event.</typeparam>
  /// <typeparam name="TSource">Type of source for the event.</typeparam>
  /// <typeparam name="TEventArgs">Type of event arguments for the event.</typeparam>
  [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Used as link target in several projects.")]
  internal class WeakEventListener<TInstance, TSource, TEventArgs> where TInstance : class
  {
    /// <summary>
    /// WeakReference to the instance listening for the event.
    /// </summary>
    private WeakReference _weakInstance;

    /// <summary>Gets or sets the method to call when the event fires.</summary>
    public Action<TInstance, TSource, TEventArgs> OnEventAction { get; set; }

    /// <summary>
    /// Gets or sets the method to call when detaching from the event.
    /// </summary>
    public Action<WeakEventListener<TInstance, TSource, TEventArgs>> OnDetachAction { get; set; }

    /// <summary>
    /// Initializes a new instances of the WeakEventListener class.
    /// </summary>
    /// <param name="instance">Instance subscribing to the event.</param>
    public WeakEventListener(TInstance instance) => this._weakInstance = null != (object) instance ? new WeakReference((object) instance) : throw new ArgumentNullException(nameof (instance));

    /// <summary>
    /// Handler for the subscribed event calls OnEventAction to handle it.
    /// </summary>
    /// <param name="source">Event source.</param>
    /// <param name="eventArgs">Event arguments.</param>
    public void OnEvent(TSource source, TEventArgs eventArgs)
    {
      TInstance target = (TInstance) this._weakInstance.Target;
      if (null != (object) target)
      {
        if (null == this.OnEventAction)
          return;
        this.OnEventAction(target, source, eventArgs);
      }
      else
        this.Detach();
    }

    /// <summary>Detaches from the subscribed event.</summary>
    public void Detach()
    {
      if (null == this.OnDetachAction)
        return;
      this.OnDetachAction(this);
      this.OnDetachAction = (Action<WeakEventListener<TInstance, TSource, TEventArgs>>) null;
    }
  }
}
