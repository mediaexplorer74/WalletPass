// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ToastPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Clarity.Phone.Extensions;
using Coding4Fun.Toolkit.Controls.Binding;
using Coding4Fun.Toolkit.Controls.Common;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  public class ToastPrompt : PopUp<string, PopUpResult>, IDisposable, IImageSourceFull, IImageSource
  {
    private const string ToastImageName = "ToastImage";
    protected Image ToastImage;
    private Timer _timer;
    private TranslateTransform _translate;
    public static readonly DependencyProperty MillisecondsUntilHiddenProperty = DependencyProperty.Register(nameof (MillisecondsUntilHidden), typeof (int), typeof (ToastPrompt), new PropertyMetadata((object) 4000));
    public static readonly DependencyProperty IsTimerEnabledProperty = DependencyProperty.Register(nameof (IsTimerEnabled), typeof (bool), typeof (ToastPrompt), new PropertyMetadata((object) true));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (ToastPrompt), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof (Message), typeof (string), typeof (ToastPrompt), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ToastPrompt), new PropertyMetadata((object) null, new PropertyChangedCallback(ToastPrompt.OnImageSource)));
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof (Stretch), typeof (Stretch), typeof (ToastPrompt), new PropertyMetadata((object) (Stretch) 0));
    public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(nameof (ImageWidth), typeof (double), typeof (ToastPrompt), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(nameof (ImageHeight), typeof (double), typeof (ToastPrompt), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.Register(nameof (TextOrientation), typeof (Orientation), typeof (ToastPrompt), new PropertyMetadata((object) (Orientation) 1));
    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(nameof (TextWrapping), typeof (TextWrapping), typeof (ToastPrompt), new PropertyMetadata((object) (TextWrapping) 1, new PropertyChangedCallback(ToastPrompt.OnTextWrapping)));

    public ToastPrompt()
    {
      this.DefaultStyleKey = (object) typeof (ToastPrompt);
      this.IsAppBarVisible = true;
      this.IsBackKeyOverride = true;
      this.IsCalculateFrameVerticalOffset = true;
      this.IsOverlayApplied = false;
      this.AnimationType = DialogService.AnimationTypes.SlideHorizontal;
      ((UIElement) this).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.ToastPromptManipulationStarted);
      ((UIElement) this).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.ToastPromptManipulationDelta);
      ((UIElement) this).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.ToastPromptManipulationCompleted);
      this.Opened += new EventHandler(this.ToastPromptOpened);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetRenderTransform();
      this.ToastImage = this.GetTemplateChild("ToastImage") as Image;
      if (this.ToastImage != null && this.ImageSource != null)
      {
        this.ToastImage.Source = this.ImageSource;
        this.SetImageVisibility(this.ImageSource);
      }
      this.SetTextOrientation(this.TextWrapping);
    }

    public override void Show()
    {
      if (!this.IsTimerEnabled)
        return;
      base.Show();
      this.SetRenderTransform();
      PreventScrollBinding.SetIsEnabled((DependencyObject) this, true);
    }

    public void Dispose()
    {
      if (this._timer == null)
        return;
      this._timer.Dispose();
      this._timer = (Timer) null;
    }

    private void ToastPromptManipulationStarted(object sender, ManipulationStartedEventArgs e) => this.PauseTimer();

    private void ToastPromptManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      this._translate.X += e.DeltaManipulation.Translation.X;
      if (this._translate.X >= 0.0)
        return;
      this._translate.X = 0.0;
    }

    private void ToastPromptManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (e.TotalManipulation.Translation.X > 200.0 || e.FinalVelocities.LinearVelocity.X > 1000.0)
        this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
        {
          PopUpResult = PopUpResult.UserDismissed
        });
      else if (e.TotalManipulation.Translation.X < 20.0)
      {
        this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
        {
          PopUpResult = PopUpResult.Ok
        });
      }
      else
      {
        this._translate.X = 0.0;
        this.StartTimer();
      }
    }

    private void ToastPromptOpened(object sender, EventArgs e) => this.StartTimer();

    private async void TimerTick(object state) => ApplicationSpace.CurrentDispatcher.BeginInvoke((Action) (() => this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
    {
      PopUpResult = PopUpResult.NoResponse
    })));

    public override void OnCompleted(PopUpEventArgs<string, PopUpResult> result)
    {
      PreventScrollBinding.SetIsEnabled((DependencyObject) this, false);
      this.PauseTimer();
      this.Dispose();
      base.OnCompleted(result);
    }

    private void SetImageVisibility(ImageSource source) => ((UIElement) this.ToastImage).Visibility = source == null ? (Visibility) 1 : (Visibility) 0;

    private void SetTextOrientation(TextWrapping value)
    {
      if (value != 2)
        return;
      this.TextOrientation = (Orientation) 0;
    }

    private void StartTimer()
    {
      if (this._timer != null)
        return;
      this._timer = new Timer(new TimerCallback(this.TimerTick), (object) null, TimeSpan.FromMilliseconds((double) this.MillisecondsUntilHidden), TimeSpan.FromMilliseconds(-1.0));
    }

    private void PauseTimer()
    {
      if (this._timer == null)
        return;
      this._timer.Change(TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
    }

    private void SetRenderTransform()
    {
      this._translate = new TranslateTransform();
      ((UIElement) this).RenderTransform = (Transform) this._translate;
    }

    private static void OnTextWrapping(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is ToastPrompt toastPrompt) || toastPrompt.ToastImage == null)
        return;
      toastPrompt.SetTextOrientation((TextWrapping) e.NewValue);
    }

    private static void OnImageSource(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is ToastPrompt toastPrompt) || toastPrompt.ToastImage == null)
        return;
      toastPrompt.SetImageVisibility(e.NewValue as ImageSource);
    }

    public int MillisecondsUntilHidden
    {
      get => (int) ((DependencyObject) this).GetValue(ToastPrompt.MillisecondsUntilHiddenProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.MillisecondsUntilHiddenProperty, (object) value);
    }

    public bool IsTimerEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(ToastPrompt.IsTimerEnabledProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.IsTimerEnabledProperty, (object) value);
    }

    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(ToastPrompt.TitleProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.TitleProperty, (object) value);
    }

    public string Message
    {
      get => (string) ((DependencyObject) this).GetValue(ToastPrompt.MessageProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.MessageProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(ToastPrompt.ImageSourceProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.ImageSourceProperty, (object) value);
    }

    public Stretch Stretch
    {
      get => (Stretch) ((DependencyObject) this).GetValue(ToastPrompt.StretchProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.StretchProperty, (object) value);
    }

    public double ImageWidth
    {
      get => (double) ((DependencyObject) this).GetValue(ToastPrompt.ImageWidthProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.ImageWidthProperty, (object) value);
    }

    public double ImageHeight
    {
      get => (double) ((DependencyObject) this).GetValue(ToastPrompt.ImageHeightProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.ImageHeightProperty, (object) value);
    }

    public Orientation TextOrientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(ToastPrompt.TextOrientationProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.TextOrientationProperty, (object) value);
    }

    public TextWrapping TextWrapping
    {
      get => (TextWrapping) ((DependencyObject) this).GetValue(ToastPrompt.TextWrappingProperty);
      set => ((DependencyObject) this).SetValue(ToastPrompt.TextWrappingProperty, (object) value);
    }
  }
}
