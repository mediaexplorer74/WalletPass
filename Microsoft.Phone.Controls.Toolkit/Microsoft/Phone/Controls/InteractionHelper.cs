// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.InteractionHelper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// The InteractionHelper provides controls with support for all of the
  /// common interactions like mouse movement, mouse clicks, key presses,
  /// etc., and also incorporates proper event semantics when the control is
  /// disabled.
  /// </summary>
  internal sealed class InteractionHelper
  {
    /// <summary>
    /// The threshold used to determine whether two clicks are temporally
    /// local and considered a double click (or triple, quadruple, etc.).
    /// 500 milliseconds is the default double click value on Windows.
    /// This value would ideally be pulled form the system settings.
    /// </summary>
    private const double SequentialClickThresholdInMilliseconds = 500.0;
    /// <summary>
    /// The threshold used to determine whether two clicks are spatially
    /// local and considered a double click (or triple, quadruple, etc.)
    /// in pixels squared.  We use pixels squared so that we can compare to
    /// the distance delta without taking a square root.
    /// </summary>
    private const double SequentialClickThresholdInPixelsSquared = 9.0;
    /// <summary>
    /// Reference used to call UpdateVisualState on the base class.
    /// </summary>
    private IUpdateVisualState _updateVisualState;

    /// <summary>Gets the control the InteractionHelper is targeting.</summary>
    public Control Control { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the control has focus.
    /// </summary>
    public bool IsFocused { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the mouse is over the control.
    /// </summary>
    public bool IsMouseOver { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the read-only property is set.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Linked file.")]
    public bool IsReadOnly { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the mouse button is pressed down
    /// over the control.
    /// </summary>
    public bool IsPressed { get; private set; }

    /// <summary>
    /// Initializes a new instance of the InteractionHelper class.
    /// </summary>
    /// <param name="control">Control receiving interaction.</param>
    public InteractionHelper(Control control)
    {
      Debug.Assert(control != null, "control should not be null!");
      this.Control = control;
      this._updateVisualState = control as IUpdateVisualState;
      ((FrameworkElement) control).Loaded += new RoutedEventHandler(this.OnLoaded);
      control.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
    }

    /// <summary>Update the visual state of the control.</summary>
    /// <param name="useTransitions">
    /// A value indicating whether to automatically generate transitions to
    /// the new state, or instantly transition to the new state.
    /// </param>
    /// <remarks>
    /// UpdateVisualState works differently than the rest of the injected
    /// functionality.  Most of the other events are overridden by the
    /// calling class which calls Allow, does what it wants, and then calls
    /// Base.  UpdateVisualState is the opposite because a number of the
    /// methods in InteractionHelper need to trigger it in the calling
    /// class.  We do this using the IUpdateVisualState internal interface.
    /// </remarks>
    private void UpdateVisualState(bool useTransitions)
    {
      if (this._updateVisualState == null)
        return;
      this._updateVisualState.UpdateVisualState(useTransitions);
    }

    /// <summary>Update the visual state of the control.</summary>
    /// <param name="useTransitions">
    /// A value indicating whether to automatically generate transitions to
    /// the new state, or instantly transition to the new state.
    /// </param>
    public void UpdateVisualStateBase(bool useTransitions)
    {
      if (!this.Control.IsEnabled)
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "Disabled", "Normal");
      else if (this.IsReadOnly)
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "ReadOnly", "Normal");
      else if (this.IsPressed)
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "Pressed", "MouseOver", "Normal");
      else if (this.IsMouseOver)
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "MouseOver", "Normal");
      else
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "Normal");
      if (this.IsFocused)
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "Focused", "Unfocused");
      else
        VisualStates.GoToState(this.Control, (useTransitions ? 1 : 0) != 0, "Unfocused");
    }

    /// <summary>Handle the control's Loaded event.</summary>
    /// <param name="sender">The control.</param>
    /// <param name="e">Event arguments.</param>
    private void OnLoaded(object sender, RoutedEventArgs e) => this.UpdateVisualState(false);

    /// <summary>Handle changes to the control's IsEnabled property.</summary>
    /// <param name="sender">The control.</param>
    /// <param name="e">Event arguments.</param>
    private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (!(bool) e.NewValue)
      {
        this.IsPressed = false;
        this.IsMouseOver = false;
        this.IsFocused = false;
      }
      this.UpdateVisualState(true);
    }

    /// <summary>Handles changes to the control's IsReadOnly property.</summary>
    /// <param name="value">The value of the property.</param>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Linked file.")]
    public void OnIsReadOnlyChanged(bool value)
    {
      this.IsReadOnly = value;
      if (!value)
      {
        this.IsPressed = false;
        this.IsMouseOver = false;
        this.IsFocused = false;
      }
      this.UpdateVisualState(true);
    }

    /// <summary>
    /// Update the visual state of the control when its template is changed.
    /// </summary>
    public void OnApplyTemplateBase() => this.UpdateVisualState(false);
  }
}
