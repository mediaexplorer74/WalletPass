// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.VisualStates
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>Names and helpers for visual states in the controls.</summary>
  internal static class VisualStates
  {
    /// <summary>Common state group.</summary>
    public const string GroupCommon = "CommonStates";
    /// <summary>Normal state of the Common state group.</summary>
    public const string StateNormal = "Normal";
    /// <summary>Normal state of the Common state group.</summary>
    public const string StateReadOnly = "ReadOnly";
    /// <summary>MouseOver state of the Common state group.</summary>
    public const string StateMouseOver = "MouseOver";
    /// <summary>Pressed state of the Common state group.</summary>
    public const string StatePressed = "Pressed";
    /// <summary>Disabled state of the Common state group.</summary>
    public const string StateDisabled = "Disabled";
    /// <summary>Focus state group.</summary>
    public const string GroupFocus = "FocusStates";
    /// <summary>Unfocused state of the Focus state group.</summary>
    public const string StateUnfocused = "Unfocused";
    /// <summary>Focused state of the Focus state group.</summary>
    public const string StateFocused = "Focused";
    /// <summary>Selection state group.</summary>
    public const string GroupSelection = "SelectionStates";
    /// <summary>Selected state of the Selection state group.</summary>
    public const string StateSelected = "Selected";
    /// <summary>Unselected state of the Selection state group.</summary>
    public const string StateUnselected = "Unselected";
    /// <summary>Selected inactive state of the Selection state group.</summary>
    public const string StateSelectedInactive = "SelectedInactive";
    /// <summary>Expansion state group.</summary>
    public const string GroupExpansion = "ExpansionStates";
    /// <summary>Expanded state of the Expansion state group.</summary>
    public const string StateExpanded = "Expanded";
    /// <summary>Collapsed state of the Expansion state group.</summary>
    public const string StateCollapsed = "Collapsed";
    /// <summary>Popup state group.</summary>
    public const string GroupPopup = "PopupStates";
    /// <summary>Opened state of the Popup state group.</summary>
    public const string StatePopupOpened = "PopupOpened";
    /// <summary>Closed state of the Popup state group.</summary>
    public const string StatePopupClosed = "PopupClosed";
    /// <summary>ValidationStates state group.</summary>
    public const string GroupValidation = "ValidationStates";
    /// <summary>The valid state for the ValidationStates group.</summary>
    public const string StateValid = "Valid";
    /// <summary>
    /// Invalid, focused state for the ValidationStates group.
    /// </summary>
    public const string StateInvalidFocused = "InvalidFocused";
    /// <summary>
    /// Invalid, unfocused state for the ValidationStates group.
    /// </summary>
    public const string StateInvalidUnfocused = "InvalidUnfocused";
    /// <summary>ExpandDirection state group.</summary>
    public const string GroupExpandDirection = "ExpandDirectionStates";
    /// <summary>
    /// Down expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandDown = "ExpandDown";
    /// <summary>
    /// Up expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandUp = "ExpandUp";
    /// <summary>
    /// Left expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandLeft = "ExpandLeft";
    /// <summary>
    /// Right expand direction state of ExpandDirection state group.
    /// </summary>
    public const string StateExpandRight = "ExpandRight";
    /// <summary>HasItems state group.</summary>
    public const string GroupHasItems = "HasItemsStates";
    /// <summary>HasItems state of the HasItems state group.</summary>
    public const string StateHasItems = "HasItems";
    /// <summary>NoItems state of the HasItems state group.</summary>
    public const string StateNoItems = "NoItems";
    /// <summary>Increment state group.</summary>
    public const string GroupIncrease = "IncreaseStates";
    /// <summary>State enabled for increment group.</summary>
    public const string StateIncreaseEnabled = "IncreaseEnabled";
    /// <summary>State disabled for increment group.</summary>
    public const string StateIncreaseDisabled = "IncreaseDisabled";
    /// <summary>Decrement state group.</summary>
    public const string GroupDecrease = "DecreaseStates";
    /// <summary>State enabled for decrement group.</summary>
    public const string StateDecreaseEnabled = "DecreaseEnabled";
    /// <summary>State disabled for decrement group.</summary>
    public const string StateDecreaseDisabled = "DecreaseDisabled";
    /// <summary>InteractionMode state group.</summary>
    public const string GroupInteractionMode = "InteractionModeStates";
    /// <summary>Edit of the DisplayMode state group.</summary>
    public const string StateEdit = "Edit";
    /// <summary>Display of the DisplayMode state group.</summary>
    public const string StateDisplay = "Display";
    /// <summary>DisplayMode state group.</summary>
    public const string GroupLocked = "LockedStates";
    /// <summary>Edit of the DisplayMode state group.</summary>
    public const string StateLocked = "Locked";
    /// <summary>Display of the DisplayMode state group.</summary>
    public const string StateUnlocked = "Unlocked";
    /// <summary>Active state.</summary>
    public const string StateActive = "Active";
    /// <summary>Inactive state.</summary>
    public const string StateInactive = "Inactive";
    /// <summary>Active state group.</summary>
    public const string GroupActive = "ActiveStates";
    /// <summary>Non-watermarked state.</summary>
    public const string StateUnwatermarked = "Unwatermarked";
    /// <summary>Watermarked state.</summary>
    public const string StateWatermarked = "Watermarked";
    /// <summary>Watermark state group.</summary>
    public const string GroupWatermark = "WatermarkStates";
    /// <summary>Unfocused state for Calendar Buttons.</summary>
    public const string StateCalendarButtonUnfocused = "CalendarButtonUnfocused";
    /// <summary>Focused state for Calendar Buttons.</summary>
    public const string StateCalendarButtonFocused = "CalendarButtonFocused";
    /// <summary>CalendarButtons Focus state group.</summary>
    public const string GroupCalendarButtonFocus = "CalendarButtonFocusStates";
    /// <summary>Busy state for BusyIndicator.</summary>
    public const string StateBusy = "Busy";
    /// <summary>Idle state for BusyIndicator.</summary>
    public const string StateIdle = "Idle";
    /// <summary>Busyness group name.</summary>
    public const string GroupBusyStatus = "BusyStatusStates";
    /// <summary>Visible state name for BusyIndicator.</summary>
    public const string StateVisible = "Visible";
    /// <summary>Hidden state name for BusyIndicator.</summary>
    public const string StateHidden = "Hidden";
    /// <summary>BusyDisplay group.</summary>
    public const string GroupVisibility = "VisibilityStates";

    /// <summary>
    /// Use VisualStateManager to change the visual state of the control.
    /// </summary>
    /// <param name="control">
    /// Control whose visual state is being changed.
    /// </param>
    /// <param name="useTransitions">
    /// A value indicating whether to use transitions when updating the
    /// visual state, or to snap directly to the new visual state.
    /// </param>
    /// <param name="stateNames">
    /// Ordered list of state names and fallback states to transition into.
    /// Only the first state to be found will be used.
    /// </param>
    public static void GoToState(Control control, bool useTransitions, params string[] stateNames)
    {
      Debug.Assert(control != null, "control should not be null!");
      Debug.Assert(stateNames != null, "stateNames should not be null!");
      Debug.Assert(stateNames.Length > 0, "stateNames should not be empty!");
      foreach (string stateName in stateNames)
      {
        if (VisualStateManager.GoToState(control, stateName, useTransitions))
          break;
      }
    }

    /// <summary>Gets the implementation root of the Control.</summary>
    /// <param name="dependencyObject">The DependencyObject.</param>
    /// <remarks>
    /// Implements the corresponding internal property on Control.
    /// </remarks>
    /// <returns>Returns the implementation root or null.</returns>
    public static FrameworkElement GetImplementationRoot(DependencyObject dependencyObject)
    {
      Debug.Assert(dependencyObject != null, "DependencyObject should not be null.");
      return 1 == VisualTreeHelper.GetChildrenCount(dependencyObject) ? VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement : (FrameworkElement) null;
    }

    /// <summary>
    /// This method tries to get the named VisualStateGroup for the
    /// dependency object. The provided object's ImplementationRoot will be
    /// looked up in this call.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="groupName">The visual state group's name.</param>
    /// <returns>Returns null or the VisualStateGroup object.</returns>
    public static VisualStateGroup TryGetVisualStateGroup(
      DependencyObject dependencyObject,
      string groupName)
    {
      FrameworkElement implementationRoot = VisualStates.GetImplementationRoot(dependencyObject);
      return implementationRoot == null ? (VisualStateGroup) null : VisualStateManager.GetVisualStateGroups(implementationRoot).OfType<VisualStateGroup>().Where<VisualStateGroup>((Func<VisualStateGroup, bool>) (group => string.CompareOrdinal(groupName, group.Name) == 0)).FirstOrDefault<VisualStateGroup>();
    }
  }
}
