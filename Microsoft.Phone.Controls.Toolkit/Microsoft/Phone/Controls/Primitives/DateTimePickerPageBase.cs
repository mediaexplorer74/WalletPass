// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.DateTimePickerPageBase
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.LocalizedResources;
using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Represents a base class for pages that work with DateTimePickerBase to allow users to choose a date/time.
  /// </summary>
  public abstract class DateTimePickerPageBase : PhoneApplicationPage, IDateTimePickerPage
  {
    private const string VisibilityGroupName = "VisibilityStates";
    private const string OpenVisibilityStateName = "Open";
    private const string ClosedVisibilityStateName = "Closed";
    private const string StateKey_Value = "DateTimePickerPageBase_State_Value";
    private LoopingSelector _primarySelectorPart;
    private LoopingSelector _secondarySelectorPart;
    private LoopingSelector _tertiarySelectorPart;
    private Storyboard _closedStoryboard;
    private DateTime? _value;

    /// <summary>
    /// Initializes the DateTimePickerPageBase class; must be called from the subclass's constructor.
    /// </summary>
    /// <param name="primarySelector">Primary selector.</param>
    /// <param name="secondarySelector">Secondary selector.</param>
    /// <param name="tertiarySelector">Tertiary selector.</param>
    protected void InitializeDateTimePickerPage(
      LoopingSelector primarySelector,
      LoopingSelector secondarySelector,
      LoopingSelector tertiarySelector)
    {
      if (null == primarySelector)
        throw new ArgumentNullException(nameof (primarySelector));
      if (null == secondarySelector)
        throw new ArgumentNullException(nameof (secondarySelector));
      if (null == tertiarySelector)
        throw new ArgumentNullException(nameof (tertiarySelector));
      this._primarySelectorPart = primarySelector;
      this._secondarySelectorPart = secondarySelector;
      this._tertiarySelectorPart = tertiarySelector;
      this._primarySelectorPart.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(this.OnDataSourceSelectionChanged);
      this._secondarySelectorPart.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(this.OnDataSourceSelectionChanged);
      this._tertiarySelectorPart.DataSource.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(this.OnDataSourceSelectionChanged);
      this._primarySelectorPart.IsExpandedChanged += new DependencyPropertyChangedEventHandler(this.OnSelectorIsExpandedChanged);
      this._secondarySelectorPart.IsExpandedChanged += new DependencyPropertyChangedEventHandler(this.OnSelectorIsExpandedChanged);
      this._tertiarySelectorPart.IsExpandedChanged += new DependencyPropertyChangedEventHandler(this.OnSelectorIsExpandedChanged);
      ((UIElement) this._primarySelectorPart).Visibility = (Visibility) 1;
      ((UIElement) this._secondarySelectorPart).Visibility = (Visibility) 1;
      ((UIElement) this._tertiarySelectorPart).Visibility = (Visibility) 1;
      int num = 0;
      foreach (LoopingSelector loopingSelector in this.GetSelectorsOrderedByCulturePattern())
      {
        Grid.SetColumn((FrameworkElement) loopingSelector, num);
        ((UIElement) loopingSelector).Visibility = (Visibility) 0;
        ++num;
      }
      FrameworkElement child = VisualTreeHelper.GetChild((DependencyObject) this, 0) as FrameworkElement;
      if (null != child)
      {
        foreach (VisualStateGroup visualStateGroup in (IEnumerable) VisualStateManager.GetVisualStateGroups(child))
        {
          if ("VisibilityStates" == visualStateGroup.Name)
          {
            foreach (VisualState state in (IEnumerable) visualStateGroup.States)
            {
              if ("Closed" == state.Name && null != state.Storyboard)
              {
                this._closedStoryboard = state.Storyboard;
                ((Timeline) this._closedStoryboard).Completed += new EventHandler(this.OnClosedStoryboardCompleted);
              }
            }
          }
        }
      }
      if (null != this.ApplicationBar)
      {
        foreach (object button in (IEnumerable) this.ApplicationBar.Buttons)
        {
          IApplicationBarIconButton iapplicationBarIconButton = button as IApplicationBarIconButton;
          if (null != iapplicationBarIconButton)
          {
            if ("DONE" == ((IApplicationBarMenuItem) iapplicationBarIconButton).Text)
            {
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Text = ControlResources.DateTimePickerDoneText;
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Click += new EventHandler(this.OnDoneButtonClick);
            }
            else if ("CANCEL" == ((IApplicationBarMenuItem) iapplicationBarIconButton).Text)
            {
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Text = ControlResources.DateTimePickerCancelText;
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Click += new EventHandler(this.OnCancelButtonClick);
            }
          }
        }
      }
      VisualStateManager.GoToState((Control) this, "Open", true);
    }

    private void OnDataSourceSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      DataSource dataSource = (DataSource) sender;
      this._primarySelectorPart.DataSource.SelectedItem = dataSource.SelectedItem;
      this._secondarySelectorPart.DataSource.SelectedItem = dataSource.SelectedItem;
      this._tertiarySelectorPart.DataSource.SelectedItem = dataSource.SelectedItem;
    }

    private void OnSelectorIsExpandedChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (!(bool) e.NewValue)
        return;
      this._primarySelectorPart.IsExpanded = sender == this._primarySelectorPart;
      this._secondarySelectorPart.IsExpanded = sender == this._secondarySelectorPart;
      this._tertiarySelectorPart.IsExpanded = sender == this._tertiarySelectorPart;
    }

    private void OnDoneButtonClick(object sender, EventArgs e)
    {
      Debug.Assert(this._primarySelectorPart.DataSource.SelectedItem == this._secondarySelectorPart.DataSource.SelectedItem && this._secondarySelectorPart.DataSource.SelectedItem == this._tertiarySelectorPart.DataSource.SelectedItem);
      this._value = new DateTime?(((DateTimeWrapper) this._primarySelectorPart.DataSource.SelectedItem).DateTime);
      this.ClosePickerPage();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
      this._value = new DateTime?();
      this.ClosePickerPage();
    }

    /// <summary>Called when the Back key is pressed.</summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      e.Cancel = true;
      this.ClosePickerPage();
    }

    private void ClosePickerPage()
    {
      if (null != this._closedStoryboard)
        VisualStateManager.GoToState((Control) this, "Closed", true);
      else
        this.OnClosedStoryboardCompleted((object) null, (EventArgs) null);
    }

    private void OnClosedStoryboardCompleted(object sender, EventArgs e) => ((Page) this).NavigationService.GoBack();

    /// <summary>
    /// Gets a sequence of LoopingSelector parts ordered according to culture string for date/time formatting.
    /// </summary>
    /// <returns>LoopingSelectors ordered by culture-specific priority.</returns>
    protected abstract IEnumerable<LoopingSelector> GetSelectorsOrderedByCulturePattern();

    /// <summary>
    /// Gets a sequence of LoopingSelector parts ordered according to culture string for date/time formatting.
    /// </summary>
    /// <param name="pattern">Culture-specific date/time format string.</param>
    /// <param name="patternCharacters">Date/time format string characters for the primary/secondary/tertiary LoopingSelectors.</param>
    /// <param name="selectors">Instances for the primary/secondary/tertiary LoopingSelectors.</param>
    /// <returns>LoopingSelectors ordered by culture-specific priority.</returns>
    protected static IEnumerable<LoopingSelector> GetSelectorsOrderedByCulturePattern(
      string pattern,
      char[] patternCharacters,
      LoopingSelector[] selectors)
    {
      if (null == pattern)
        throw new ArgumentNullException(nameof (pattern));
      if (null == patternCharacters)
        throw new ArgumentNullException(nameof (patternCharacters));
      if (null == selectors)
        throw new ArgumentNullException(nameof (selectors));
      if (patternCharacters.Length != selectors.Length)
        throw new ArgumentException("Arrays must contain the same number of elements.");
      List<Tuple<int, LoopingSelector>> source = new List<Tuple<int, LoopingSelector>>(patternCharacters.Length);
      for (int index = 0; index < patternCharacters.Length; ++index)
        source.Add(new Tuple<int, LoopingSelector>(pattern.IndexOf(patternCharacters[index]), selectors[index]));
      return source.Where<Tuple<int, LoopingSelector>>((Func<Tuple<int, LoopingSelector>, bool>) (p => -1 != p.Item1)).OrderBy<Tuple<int, LoopingSelector>, int>((Func<Tuple<int, LoopingSelector>, int>) (p => p.Item1)).Select<Tuple<int, LoopingSelector>, LoopingSelector>((Func<Tuple<int, LoopingSelector>, LoopingSelector>) (p => p.Item2)).Where<LoopingSelector>((Func<LoopingSelector, bool>) (s => null != s));
    }

    /// <summary>
    /// Gets or sets the DateTime to show in the picker page and to set when the user makes a selection.
    /// </summary>
    public DateTime? Value
    {
      get => this._value;
      set
      {
        this._value = value;
        DateTimeWrapper dateTimeWrapper = new DateTimeWrapper(this._value.GetValueOrDefault(DateTime.Now));
        this._primarySelectorPart.DataSource.SelectedItem = (object) dateTimeWrapper;
        this._secondarySelectorPart.DataSource.SelectedItem = (object) dateTimeWrapper;
        this._tertiarySelectorPart.DataSource.SelectedItem = (object) dateTimeWrapper;
      }
    }

    /// <summary>
    /// Called when a page is no longer the active page in a frame.
    /// </summary>
    /// <param name="e">An object that contains the event data.</param>
    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Page) this).OnNavigatedFrom(e);
      if (!("app://external/" == e.Uri.ToString()))
        return;
      this.State["DateTimePickerPageBase_State_Value"] = (object) this.Value;
    }

    /// <summary>
    /// Called when a page becomes the active page in a frame.
    /// </summary>
    /// <param name="e">An object that contains the event data.</param>
    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Page) this).OnNavigatedTo(e);
      if (!this.State.ContainsKey("DateTimePickerPageBase_State_Value"))
        return;
      this.Value = this.State["DateTimePickerPageBase_State_Value"] as DateTime?;
      if (((Page) this).NavigationService.CanGoBack)
        ((Page) this).NavigationService.GoBack();
    }

    /// <summary>Sets the selectors and title flow direction.</summary>
    /// <param name="flowDirection">Flow direction to set.</param>
    internal abstract void SetFlowDirection(FlowDirection flowDirection);
  }
}
