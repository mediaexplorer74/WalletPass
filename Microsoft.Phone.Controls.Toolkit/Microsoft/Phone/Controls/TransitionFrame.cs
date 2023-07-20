// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TransitionFrame
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Enables navigation transitions for
  /// <see cref="T:Microsoft.Phone.Controls.PhoneApplicationPage" />s.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplatePart(Name = "FirstContentPresenter", Type = typeof (ContentPresenter))]
  [TemplatePart(Name = "SecondContentPresenter", Type = typeof (ContentPresenter))]
  public class TransitionFrame : PhoneApplicationFrame
  {
    /// <summary>
    /// The new
    /// <see cref="T:System.Windows.Controls.ContentPresenter" />
    /// template part name.
    /// </summary>
    private const string FirstTemplatePartName = "FirstContentPresenter";
    /// <summary>
    /// The old
    /// <see cref="T:System.Windows.Controls.ContentPresenter" />
    /// template part name.
    /// </summary>
    private const string SecondTemplatePartName = "SecondContentPresenter";
    /// <summary>
    /// A single shared instance for setting BitmapCache on a visual.
    /// </summary>
    internal static readonly CacheMode BitmapCacheMode = (CacheMode) new BitmapCache();
    /// <summary>
    /// The first <see cref="T:System.Windows.Controls.ContentPresenter" />.
    /// </summary>
    private ContentPresenter _firstContentPresenter;
    /// <summary>
    /// The second <see cref="T:System.Windows.Controls.ContentPresenter" />.
    /// </summary>
    private ContentPresenter _secondContentPresenter;
    /// <summary>
    /// The new <see cref="T:System.Windows.Controls.ContentPresenter" />.
    /// </summary>
    private ContentPresenter _newContentPresenter;
    /// <summary>
    /// The old <see cref="T:System.Windows.Controls.ContentPresenter" />.
    /// </summary>
    private ContentPresenter _oldContentPresenter;
    /// <summary>Indicates whether a navigation is forward.</summary>
    private bool _isForwardNavigation;
    /// <summary>
    /// Determines whether to set the new content to the first or second
    /// <see cref="T:System.Windows.Controls.ContentPresenter" />.
    /// </summary>
    private bool _useFirstAsNew;
    /// <summary>
    /// A value indicating whether the old transition has completed and the
    /// new transition can begin.
    /// </summary>
    private bool _readyToTransitionToNewContent;
    /// <summary>
    /// A value indicating whether the new content has been loaded and the
    /// new transition can begin.
    /// </summary>
    private bool _contentReady;
    /// <summary>
    /// A value indicating whether the exit transition is currently being performed.
    /// </summary>
    private bool _performingExitTransition;
    /// <summary>
    /// A value indicating whether the navigation is cancelled.
    /// </summary>
    private bool _navigationStopped;
    /// <summary>
    /// The transition to use to move in new content once the old transition
    /// is complete and ready for movement.
    /// </summary>
    private ITransition _storedNewTransition;
    /// <summary>
    /// The stored NavigationIn transition instance to use once the old
    /// transition is complete and ready for movement.
    /// </summary>
    private NavigationInTransition _storedNavigationInTransition;
    /// <summary>The transition to use to complete the old transition.</summary>
    private ITransition _storedOldTransition;
    /// <summary>The stored NavigationOut transition instance.</summary>
    private NavigationOutTransition _storedNavigationOutTransition;

    /// <summary>
    /// Initialzies a new instance of the TransitionFrame class.
    /// </summary>
    public TransitionFrame()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (TransitionFrame);
      ((Frame) this).Navigating += new NavigatingCancelEventHandler(this.OnNavigating);
      ((Frame) this).NavigationStopped += new NavigationStoppedEventHandler(this.OnNavigationStopped);
    }

    /// <summary>
    /// Flips the logical content presenters to prepare for the next visual
    /// transition.
    /// </summary>
    private void FlipPresenters()
    {
      this._newContentPresenter = this._useFirstAsNew ? this._firstContentPresenter : this._secondContentPresenter;
      this._oldContentPresenter = this._useFirstAsNew ? this._secondContentPresenter : this._firstContentPresenter;
      this._useFirstAsNew = !this._useFirstAsNew;
    }

    /// <summary>
    /// Handles the Navigating event of the frame, the immediate way to
    /// begin a transition out before the new page has loaded or had its
    /// layout pass.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event arguments.</param>
    private void OnNavigating(object sender, NavigatingCancelEventArgs e)
    {
      if (!e.IsNavigationInitiator)
        return;
      this._isForwardNavigation = e.NavigationMode != 1;
      if (!(((ContentControl) this).Content is UIElement content))
        return;
      this.EnsureLastTransitionIsComplete();
      this.FlipPresenters();
      TransitionElement transitionElement = (TransitionElement) null;
      ITransition transition = (ITransition) null;
      NavigationOutTransition navigationOutTransition = TransitionService.GetNavigationOutTransition(content);
      if (navigationOutTransition != null)
        transitionElement = this._isForwardNavigation ? navigationOutTransition.Forward : navigationOutTransition.Backward;
      if (transitionElement != null)
        transition = transitionElement.GetTransition(content);
      if (transition != null)
      {
        TransitionFrame.EnsureStoppedTransition(transition);
        this._storedNavigationOutTransition = navigationOutTransition;
        this._storedOldTransition = transition;
        transition.Completed += new EventHandler(this.OnExitTransitionCompleted);
        this._performingExitTransition = true;
        TransitionFrame.PerformTransition((NavigationTransition) navigationOutTransition, this._oldContentPresenter, transition);
        TransitionFrame.PrepareContentPresenterForCompositor(this._oldContentPresenter);
      }
      else
        this._readyToTransitionToNewContent = true;
    }

    /// <summary>
    /// Handles the NavigationStopped event of the frame. Set a value indicating
    /// that the navigation is cancelled.
    /// </summary>
    private void OnNavigationStopped(object sender, NavigationEventArgs e) => this._navigationStopped = true;

    /// <summary>
    /// Stops the last navigation transition if it's active and a new navigation occurs.
    /// </summary>
    private void EnsureLastTransitionIsComplete()
    {
      this._readyToTransitionToNewContent = false;
      this._contentReady = false;
      if (!this._performingExitTransition)
        return;
      Debug.Assert(this._storedOldTransition != null && this._storedNavigationOutTransition != null);
      this._oldContentPresenter.Content = (object) null;
      this._storedOldTransition.Stop();
      this._storedNavigationOutTransition = (NavigationOutTransition) null;
      this._storedOldTransition = (ITransition) null;
      if (this._storedNewTransition != null)
      {
        this._storedNewTransition.Stop();
        this._storedNewTransition = (ITransition) null;
        this._storedNavigationInTransition = (NavigationInTransition) null;
      }
      this._performingExitTransition = false;
    }

    /// <summary>
    /// Handles the completion of the exit transition, automatically
    /// continuing to bring in the new element's transition as well if it is
    /// ready.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event arguments.</param>
    private void OnExitTransitionCompleted(object sender, EventArgs e)
    {
      this._readyToTransitionToNewContent = true;
      this._performingExitTransition = false;
      if (this._navigationStopped)
      {
        TransitionFrame.CompleteTransition((NavigationTransition) this._storedNavigationOutTransition, this._oldContentPresenter, this._storedOldTransition);
        this._navigationStopped = false;
      }
      else
        TransitionFrame.CompleteTransition((NavigationTransition) this._storedNavigationOutTransition, (ContentPresenter) null, this._storedOldTransition);
      this._storedNavigationOutTransition = (NavigationOutTransition) null;
      this._storedOldTransition = (ITransition) null;
      if (!this._contentReady)
        return;
      ITransition storedNewTransition = this._storedNewTransition;
      NavigationInTransition navigationInTransition = this._storedNavigationInTransition;
      this._storedNewTransition = (ITransition) null;
      this._storedNavigationInTransition = (NavigationInTransition) null;
      this.TransitionNewContent(storedNewTransition, navigationInTransition);
    }

    /// <summary>
    /// When overridden in a derived class, is invoked whenever application
    /// code or internal processes (such as a rebuilding layout pass) call
    /// <see cref="M:System.Windows.Controls.Control.ApplyTemplate" />.
    /// In simplest terms, this means the method is called just before a UI
    /// element displays in an application.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._firstContentPresenter = ((Control) this).GetTemplateChild("FirstContentPresenter") as ContentPresenter;
      this._secondContentPresenter = ((Control) this).GetTemplateChild("SecondContentPresenter") as ContentPresenter;
      this._newContentPresenter = this._secondContentPresenter;
      this._oldContentPresenter = this._firstContentPresenter;
      this._useFirstAsNew = true;
      this._readyToTransitionToNewContent = true;
      if (((ContentControl) this).Content == null)
        return;
      ((ContentControl) this).OnContentChanged((object) null, ((ContentControl) this).Content);
    }

    /// <summary>
    /// Called when the value of the
    /// <see cref="P:System.Windows.Controls.ContentControl.Content" />
    /// property changes.
    /// </summary>
    /// <param name="oldContent">The old <see cref="T:System.Object" />.</param>
    /// <param name="newContent">The new <see cref="T:System.Object" />.</param>
    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      ((ContentControl) this).OnContentChanged(oldContent, newContent);
      this._contentReady = true;
      UIElement uiElement = oldContent as UIElement;
      UIElement element = newContent as UIElement;
      if (this._firstContentPresenter == null || this._secondContentPresenter == null || element == null)
        return;
      NavigationInTransition navigationInTransition = (NavigationInTransition) null;
      ITransition newTransition = (ITransition) null;
      if (element != null)
      {
        navigationInTransition = TransitionService.GetNavigationInTransition(element);
        TransitionElement transitionElement = (TransitionElement) null;
        if (navigationInTransition != null)
          transitionElement = this._isForwardNavigation ? navigationInTransition.Forward : navigationInTransition.Backward;
        if (transitionElement != null)
        {
          element.UpdateLayout();
          newTransition = transitionElement.GetTransition(element);
          TransitionFrame.PrepareContentPresenterForCompositor(this._newContentPresenter);
        }
      }
      ((UIElement) this._newContentPresenter).Opacity = 0.0;
      ((UIElement) this._newContentPresenter).Visibility = (Visibility) 0;
      this._newContentPresenter.Content = (object) element;
      ((UIElement) this._oldContentPresenter).Opacity = 1.0;
      ((UIElement) this._oldContentPresenter).Visibility = (Visibility) 0;
      this._oldContentPresenter.Content = (object) uiElement;
      if (this._readyToTransitionToNewContent)
      {
        this.TransitionNewContent(newTransition, navigationInTransition);
      }
      else
      {
        this._storedNewTransition = newTransition;
        this._storedNavigationInTransition = navigationInTransition;
      }
    }

    /// <summary>
    /// Transitions the new <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <param name="newTransition">The <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for the new <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="navigationInTransition">The <see cref="T:Microsoft.Phone.Controls.NavigationInTransition" />
    /// for the new <see cref="T:System.Windows.UIElement" />.</param>
    private void TransitionNewContent(
      ITransition newTransition,
      NavigationInTransition navigationInTransition)
    {
      if (this._oldContentPresenter != null)
      {
        ((UIElement) this._oldContentPresenter).Visibility = (Visibility) 1;
        this._oldContentPresenter.Content = (object) null;
      }
      if (null == newTransition)
      {
        TransitionFrame.RestoreContentPresenterInteractivity(this._newContentPresenter);
      }
      else
      {
        TransitionFrame.EnsureStoppedTransition(newTransition);
        newTransition.Completed += (EventHandler) ((param0, param1) => TransitionFrame.CompleteTransition((NavigationTransition) navigationInTransition, this._newContentPresenter, newTransition));
        this._readyToTransitionToNewContent = false;
        this._storedNavigationInTransition = (NavigationInTransition) null;
        this._storedNewTransition = (ITransition) null;
        TransitionFrame.PerformTransition((NavigationTransition) navigationInTransition, this._newContentPresenter, newTransition);
      }
    }

    /// <summary>
    /// This checks to make sure that, if the transition not be in the clock
    /// state of Stopped, that is will be stopped.
    /// </summary>
    /// <param name="transition">The transition instance.</param>
    private static void EnsureStoppedTransition(ITransition transition)
    {
      if (transition == null || transition.GetCurrentState() == 2)
        return;
      transition.Stop();
    }

    /// <summary>
    /// Performs a transition when given the appropriate components,
    /// includes calling the appropriate start event and ensuring opacity
    /// on the content presenter.
    /// </summary>
    /// <param name="navigationTransition">The navigation transition.</param>
    /// <param name="presenter">The content presenter.</param>
    /// <param name="transition">The transition instance.</param>
    private static void PerformTransition(
      NavigationTransition navigationTransition,
      ContentPresenter presenter,
      ITransition transition)
    {
      navigationTransition?.OnBeginTransition();
      if (presenter != null && ((UIElement) presenter).Opacity != 1.0)
        ((UIElement) presenter).Opacity = 1.0;
      transition?.Begin();
    }

    /// <summary>
    /// Completes a transition operation by stopping it, restoring
    /// interactivity, and then firing the OnEndTransition event.
    /// </summary>
    /// <param name="navigationTransition">The navigation transition.</param>
    /// <param name="presenter">The content presenter.</param>
    /// <param name="transition">The transition instance.</param>
    private static void CompleteTransition(
      NavigationTransition navigationTransition,
      ContentPresenter presenter,
      ITransition transition)
    {
      transition?.Stop();
      TransitionFrame.RestoreContentPresenterInteractivity(presenter);
      navigationTransition?.OnEndTransition();
    }

    /// <summary>
    /// Updates the content presenter for off-thread compositing for the
    /// transition animation. Also disables interactivity on it to prevent
    /// accidental touches.
    /// </summary>
    /// <param name="presenter">The content presenter instance.</param>
    /// <param name="applyBitmapCache">A value indicating whether to apply
    /// a bitmap cache.</param>
    private static void PrepareContentPresenterForCompositor(
      ContentPresenter presenter,
      bool applyBitmapCache = true)
    {
      if (presenter == null)
        return;
      if (applyBitmapCache)
        ((UIElement) presenter).CacheMode = TransitionFrame.BitmapCacheMode;
      ((UIElement) presenter).IsHitTestVisible = false;
    }

    /// <summary>
    /// Restores the interactivity for the presenter post-animation, also
    /// removes the BitmapCache value.
    /// </summary>
    /// <param name="presenter">The content presenter instance.</param>
    private static void RestoreContentPresenterInteractivity(ContentPresenter presenter)
    {
      if (presenter == null)
        return;
      ((UIElement) presenter).CacheMode = (CacheMode) null;
      if (((UIElement) presenter).Opacity != 1.0)
        ((UIElement) presenter).Opacity = 1.0;
      ((UIElement) presenter).IsHitTestVisible = true;
    }
  }
}
