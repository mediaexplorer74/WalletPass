// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.SelectorSelectionAdapter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents the selection adapter contained in the drop-down portion of
  /// an <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  public class SelectorSelectionAdapter : ISelectionAdapter
  {
    /// <summary>The Selector instance.</summary>
    private Selector _selector;

    /// <summary>
    /// Gets or sets a value indicating whether the selection change event
    /// should not be fired.
    /// </summary>
    private bool IgnoringSelectionChanged { get; set; }

    /// <summary>
    /// Gets or sets the underlying
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control.
    /// </summary>
    /// <value>The underlying
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control.</value>
    public Selector SelectorControl
    {
      get => this._selector;
      set
      {
        if (this._selector != null)
          this._selector.SelectionChanged -= new SelectionChangedEventHandler(this.OnSelectionChanged);
        this._selector = value;
        if (this._selector == null)
          return;
        this._selector.SelectionChanged += new SelectionChangedEventHandler(this.OnSelectionChanged);
      }
    }

    /// <summary>
    /// Occurs when the
    /// <see cref="P:System.Windows.Controls.SelectorSelectionAdapter.SelectedItem" />
    /// property value changes.
    /// </summary>
    public event SelectionChangedEventHandler SelectionChanged;

    /// <summary>
    /// Occurs when an item is selected and is committed to the underlying
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control.
    /// </summary>
    public event RoutedEventHandler Commit;

    /// <summary>
    /// Occurs when a selection is canceled before it is committed.
    /// </summary>
    public event RoutedEventHandler Cancel;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:System.Windows.Controls.SelectorSelectionAdapter" />
    /// class.
    /// </summary>
    public SelectorSelectionAdapter()
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:System.Windows.Controls.SelectorSelectionAdapter" />
    /// class with the specified
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control.
    /// </summary>
    /// <param name="selector">The
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" /> control
    /// to wrap as a
    /// <see cref="T:System.Windows.Controls.SelectorSelectionAdapter" />.</param>
    public SelectorSelectionAdapter(Selector selector) => this.SelectorControl = selector;

    /// <summary>
    /// Gets or sets the selected item of the selection adapter.
    /// </summary>
    /// <value>The selected item of the underlying selection adapter.</value>
    public object SelectedItem
    {
      get => this.SelectorControl == null ? (object) null : this.SelectorControl.SelectedItem;
      set
      {
        this.IgnoringSelectionChanged = true;
        if (this.SelectorControl != null)
          this.SelectorControl.SelectedItem = value;
        if (value == null)
          this.ResetScrollViewer();
        this.IgnoringSelectionChanged = false;
      }
    }

    /// <summary>
    /// Gets or sets a collection that is used to generate the content of
    /// the selection adapter.
    /// </summary>
    /// <value>The collection used to generate content for the selection
    /// adapter.</value>
    public IEnumerable ItemsSource
    {
      get => this.SelectorControl == null ? (IEnumerable) null : ((ItemsControl) this.SelectorControl).ItemsSource;
      set
      {
        if (this.SelectorControl == null)
          return;
        ((ItemsControl) this.SelectorControl).ItemsSource = value;
      }
    }

    /// <summary>
    /// If the control contains a ScrollViewer, this will reset the viewer
    /// to be scrolled to the top.
    /// </summary>
    private void ResetScrollViewer()
    {
      if (this.SelectorControl == null)
        return;
      ((FrameworkElement) this.SelectorControl).GetLogicalChildrenBreadthFirst().OfType<ScrollViewer>().FirstOrDefault<ScrollViewer>()?.ScrollToVerticalOffset(0.0);
    }

    /// <summary>
    /// Handles the SelectionChanged event on the Selector control.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The selection changed event data.</param>
    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (this.IgnoringSelectionChanged)
        return;
      SelectionChangedEventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged != null)
        selectionChanged(sender, e);
      this.OnCommit();
    }

    /// <summary>
    /// Increments the
    /// <see cref="P:System.Windows.Controls.Primitives.Selector.SelectedIndex" />
    /// property of the underlying
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control.
    /// </summary>
    protected void SelectedIndexIncrement()
    {
      if (this.SelectorControl == null)
        return;
      this.SelectorControl.SelectedIndex = this.SelectorControl.SelectedIndex + 1 >= ((PresentationFrameworkCollection<object>) ((ItemsControl) this.SelectorControl).Items).Count ? -1 : this.SelectorControl.SelectedIndex + 1;
    }

    /// <summary>
    /// Decrements the
    /// <see cref="P:System.Windows.Controls.Primitives.Selector.SelectedIndex" />
    /// property of the underlying
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control.
    /// </summary>
    protected void SelectedIndexDecrement()
    {
      if (this.SelectorControl == null)
        return;
      int selectedIndex = this.SelectorControl.SelectedIndex;
      if (selectedIndex >= 0)
        --this.SelectorControl.SelectedIndex;
      else if (selectedIndex == -1)
        this.SelectorControl.SelectedIndex = ((PresentationFrameworkCollection<object>) ((ItemsControl) this.SelectorControl).Items).Count - 1;
    }

    /// <summary>
    /// Provides handling for the
    /// <see cref="E:System.Windows.UIElement.KeyDown" /> event that occurs
    /// when a key is pressed while the drop-down portion of the
    /// <see cref="T:System.Windows.Controls.AutoCompleteBox" /> has focus.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Input.KeyEventArgs" />
    /// that contains data about the
    /// <see cref="E:System.Windows.UIElement.KeyDown" /> event.</param>
    public void HandleKeyDown(KeyEventArgs e)
    {
      Key key = e != null ? e.Key : throw new ArgumentNullException(nameof (e));
      if (key != 3)
      {
        if (key != 8)
        {
          switch (key - 15)
          {
            case 0:
              this.SelectedIndexDecrement();
              e.Handled = true;
              break;
            case 2:
              if ((1 & Keyboard.Modifiers) != 0)
                break;
              this.SelectedIndexIncrement();
              e.Handled = true;
              break;
          }
        }
        else
        {
          this.OnCancel();
          e.Handled = true;
        }
      }
      else
      {
        this.OnCommit();
        e.Handled = true;
      }
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:System.Windows.Controls.SelectorSelectionAdapter.Commit" />
    /// event.
    /// </summary>
    protected virtual void OnCommit() => this.OnCommit((object) this, new RoutedEventArgs());

    /// <summary>Fires the Commit event.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnCommit(object sender, RoutedEventArgs e)
    {
      RoutedEventHandler commit = this.Commit;
      if (commit != null)
        commit(sender, e);
      this.AfterAdapterAction();
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:System.Windows.Controls.SelectorSelectionAdapter.Cancel" />
    /// event.
    /// </summary>
    protected virtual void OnCancel() => this.OnCancel((object) this, new RoutedEventArgs());

    /// <summary>Fires the Cancel event.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnCancel(object sender, RoutedEventArgs e)
    {
      RoutedEventHandler cancel = this.Cancel;
      if (cancel != null)
        cancel(sender, e);
      this.AfterAdapterAction();
    }

    /// <summary>Change the selection after the actions are complete.</summary>
    private void AfterAdapterAction()
    {
      this.IgnoringSelectionChanged = true;
      if (this.SelectorControl != null)
      {
        this.SelectorControl.SelectedItem = (object) null;
        this.SelectorControl.SelectedIndex = -1;
      }
      this.IgnoringSelectionChanged = false;
    }

    /// <summary>
    /// Returns an automation peer for the underlying
    /// <see cref="T:System.Windows.Controls.Primitives.Selector" />
    /// control, for use by the Silverlight automation infrastructure.
    /// </summary>
    /// <returns>An automation peer for use by the Silverlight automation
    /// infrastructure.</returns>
    public AutomationPeer CreateAutomationPeer() => this._selector != null ? FrameworkElementAutomationPeer.CreatePeerForElement((UIElement) this._selector) : (AutomationPeer) null;
  }
}
