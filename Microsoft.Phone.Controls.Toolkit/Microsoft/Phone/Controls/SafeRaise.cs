// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.SafeRaise
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Phone.Controls
{
  /// <summary>A helper class for raising events safely.</summary>
  internal static class SafeRaise
  {
    /// <summary>
    /// Raises an event in a thread-safe manner, also does the null check.
    /// </summary>
    /// <param name="eventToRaise">The event to raise.</param>
    /// <param name="sender">The event sender.</param>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Keeping existing implementation.")]
    public static void Raise(EventHandler eventToRaise, object sender)
    {
      if (eventToRaise == null)
        return;
      eventToRaise(sender, EventArgs.Empty);
    }

    /// <summary>
    /// Raises an event in a thread-safe manner, also does the null check.
    /// </summary>
    /// <param name="eventToRaise">The event to raise.</param>
    /// <param name="sender">The event sender.</param>
    public static void Raise(EventHandler<EventArgs> eventToRaise, object sender) => SafeRaise.Raise<EventArgs>(eventToRaise, sender, EventArgs.Empty);

    /// <summary>
    /// Raises an event in a thread-safe manner, also does the null check.
    /// </summary>
    /// <typeparam name="T">The event args type.</typeparam>
    /// <param name="eventToRaise">The event to raise.</param>
    /// <param name="sender">The event sender.</param>
    /// <param name="args">The event args.</param>
    public static void Raise<T>(EventHandler<T> eventToRaise, object sender, T args) where T : EventArgs
    {
      if (eventToRaise == null)
        return;
      eventToRaise(sender, args);
    }

    /// <summary>
    /// Raise an event in a thread-safe manner, with the required null check. Lazily creates event args.
    /// </summary>
    /// <typeparam name="T">The event args type.</typeparam>
    /// <param name="eventToRaise">The event to raise.</param>
    /// <param name="sender">The event sender.</param>
    /// <param name="getEventArgs">The delegate to return the event args if needed.</param>
    public static void Raise<T>(
      EventHandler<T> eventToRaise,
      object sender,
      SafeRaise.GetEventArgs<T> getEventArgs)
      where T : EventArgs
    {
      if (eventToRaise == null)
        return;
      eventToRaise(sender, getEventArgs());
    }

    /// <summary>
    /// This is a method that returns event args, used for lazy creation.
    /// </summary>
    /// <typeparam name="T">The event type.</typeparam>
    /// <returns></returns>
    public delegate T GetEventArgs<T>() where T : EventArgs;
  }
}
