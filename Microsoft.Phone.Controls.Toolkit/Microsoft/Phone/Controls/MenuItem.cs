// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.MenuItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a selectable item inside a Menu or ContextMenu.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (MenuItem))]
  [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
  [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  public class MenuItem : HeaderedItemsControl
  {
    /// <summary>
    /// Stores a value indicating whether this element has logical focus.
    /// </summary>
    private bool _isFocused;
    /// <summary>Identifies the Command dependency property.</summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof (Command), typeof (ICommand), typeof (MenuItem), new PropertyMetadata((object) null, new PropertyChangedCallback(MenuItem.OnCommandChanged)));
    /// <summary>Identifies the CommandParameter dependency property.</summary>
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof (CommandParameter), typeof (object), typeof (MenuItem), new PropertyMetadata((object) null, new PropertyChangedCallback(MenuItem.OnCommandParameterChanged)));

    /// <summary>Occurs when a MenuItem is clicked.</summary>
    public event RoutedEventHandler Click;

    /// <summary>Gets or sets a reference to the MenuBase parent.</summary>
    internal MenuBase ParentMenuBase { get; set; }

    /// <summary>
    /// Gets or sets the command associated with the menu item.
    /// </summary>
    public ICommand Command
    {
      get => (ICommand) ((DependencyObject) this).GetValue(MenuItem.CommandProperty);
      set => ((DependencyObject) this).SetValue(MenuItem.CommandProperty, (object) value);
    }

    /// <summary>Handles changes to the Command DependencyProperty.</summary>
    /// <param name="o">DependencyObject that changed.</param>
    /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
    private static void OnCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) => ((MenuItem) o).OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);

    /// <summary>Handles changes to the Command property.</summary>
    /// <param name="oldValue">Old value.</param>
    /// <param name="newValue">New value.</param>
    private void OnCommandChanged(ICommand oldValue, ICommand newValue)
    {
      if (null != oldValue)
        oldValue.CanExecuteChanged -= new EventHandler(this.OnCanExecuteChanged);
      if (null != newValue)
        newValue.CanExecuteChanged += new EventHandler(this.OnCanExecuteChanged);
      this.UpdateIsEnabled(true);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the Command property of a MenuItem.
    /// </summary>
    public object CommandParameter
    {
      get => ((DependencyObject) this).GetValue(MenuItem.CommandParameterProperty);
      set => ((DependencyObject) this).SetValue(MenuItem.CommandParameterProperty, value);
    }

    /// <summary>
    /// Handles changes to the CommandParameter DependencyProperty.
    /// </summary>
    /// <param name="o">DependencyObject that changed.</param>
    /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
    private static void OnCommandParameterChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((MenuItem) o).OnCommandParameterChanged();
    }

    /// <summary>Handles changes to the CommandParameter property.</summary>
    private void OnCommandParameterChanged() => this.UpdateIsEnabled(true);

    /// <summary>Initializes a new instance of the MenuItem class.</summary>
    [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "False parameter to UpdateIsEnabled ensures ChangeVisualState won't be called.")]
    public MenuItem()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (MenuItem);
      ((Control) this).IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
      ((DependencyObject) this).SetValue(TiltEffect.IsTiltEnabledProperty, (object) true);
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.OnLoaded);
      this.UpdateIsEnabled(false);
    }

    /// <summary>Called when the template's tree is generated.</summary>
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.ChangeVisualState(false);
    }

    /// <summary>
    /// Invoked whenever an unhandled GotFocus event reaches this element in its route.
    /// </summary>
    /// <param name="e">A RoutedEventArgs that contains event data.</param>
    protected virtual void OnGotFocus(RoutedEventArgs e)
    {
      ((Control) this).OnGotFocus(e);
      this._isFocused = true;
      this.ChangeVisualState(true);
    }

    /// <summary>
    /// Raises the LostFocus routed event by using the event data that is provided.
    /// </summary>
    /// <param name="e">A RoutedEventArgs that contains event data.</param>
    protected virtual void OnLostFocus(RoutedEventArgs e)
    {
      ((Control) this).OnLostFocus(e);
      this._isFocused = false;
      this.ChangeVisualState(true);
    }

    /// <summary>Called whenever the mouse enters a MenuItem.</summary>
    /// <param name="e">The event data for the MouseEnter event.</param>
    protected virtual void OnMouseEnter(MouseEventArgs e)
    {
      ((Control) this).OnMouseEnter(e);
      ((Control) this).Focus();
      this.ChangeVisualState(true);
    }

    /// <summary>Called whenever the mouse leaves a MenuItem.</summary>
    /// <param name="e">The event data for the MouseLeave event.</param>
    protected virtual void OnMouseLeave(MouseEventArgs e)
    {
      ((Control) this).OnMouseLeave(e);
      if (null != this.ParentMenuBase)
        ((Control) this.ParentMenuBase).Focus();
      this.ChangeVisualState(true);
    }

    /// <summary>Called when the left mouse button is released.</summary>
    /// <param name="e">The event data for the MouseLeftButtonUp event.</param>
    protected virtual void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (!e.Handled)
      {
        this.OnClick();
        e.Handled = true;
      }
      ((Control) this).OnMouseLeftButtonUp(e);
    }

    /// <summary>Responds to the KeyDown event.</summary>
    /// <param name="e">The event data for the KeyDown event.</param>
    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (!e.Handled && 3 == e.Key)
      {
        this.OnClick();
        e.Handled = true;
      }
      ((Control) this).OnKeyDown(e);
    }

    /// <summary>Called when the Items property changes.</summary>
    /// <param name="e">The event data for the ItemsChanged event.</param>
    protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e) => throw new NotImplementedException();

    /// <summary>
    /// Called when a MenuItem is clicked and raises a Click event.
    /// </summary>
    protected virtual void OnClick()
    {
      ContextMenu parentMenuBase = this.ParentMenuBase as ContextMenu;
      if (null != parentMenuBase)
        parentMenuBase.ChildMenuItemClicked();
      RoutedEventHandler click = this.Click;
      if (null != click)
        click((object) this, new RoutedEventArgs());
      if (this.Command == null || !this.Command.CanExecute(this.CommandParameter))
        return;
      this.Command.Execute(this.CommandParameter);
    }

    /// <summary>
    /// Handles the CanExecuteChanged event of the Command property.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnCanExecuteChanged(object sender, EventArgs e) => this.UpdateIsEnabled(true);

    /// <summary>Updates the IsEnabled property.</summary>
    /// <remarks>
    /// WPF overrides the local value of IsEnabled according to ICommand, so Silverlight does, too.
    /// </remarks>
    /// <param name="changeVisualState">True if ChangeVisualState should be called.</param>
    private void UpdateIsEnabled(bool changeVisualState)
    {
      ((Control) this).IsEnabled = this.Command == null || this.Command.CanExecute(this.CommandParameter);
      if (!changeVisualState)
        return;
      this.ChangeVisualState(true);
    }

    /// <summary>Called when the IsEnabled property changes.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) => this.ChangeVisualState(true);

    /// <summary>Called when the Loaded event is raised.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnLoaded(object sender, RoutedEventArgs e) => this.ChangeVisualState(false);

    /// <summary>
    /// Changes to the correct visual state(s) for the control.
    /// </summary>
    /// <param name="useTransitions">True to use transitions; otherwise false.</param>
    protected virtual void ChangeVisualState(bool useTransitions)
    {
      if (!((Control) this).IsEnabled)
        VisualStateManager.GoToState((Control) this, "Disabled", useTransitions);
      else
        VisualStateManager.GoToState((Control) this, "Normal", useTransitions);
      if (this._isFocused && ((Control) this).IsEnabled)
        VisualStateManager.GoToState((Control) this, "Focused", useTransitions);
      else
        VisualStateManager.GoToState((Control) this, "Unfocused", useTransitions);
    }
  }
}
