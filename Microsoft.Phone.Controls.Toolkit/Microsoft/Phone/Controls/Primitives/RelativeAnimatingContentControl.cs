// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.RelativeAnimatingContentControl
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// A very specialized primitive control that works around a specific visual
  /// state manager issue. The platform does not support relative sized
  /// translation values, and this special control walks through visual state
  /// animation storyboards looking for magic numbers to use as percentages.
  /// This control is not supported, unofficial, and is a hack in many ways.
  /// It is used to enable a Windows Phone native platform-style progress bar
  /// experience in indeterminate mode that remains performant.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public class RelativeAnimatingContentControl : ContentControl
  {
    /// <summary>
    /// A simple Epsilon-style value used for trying to determine the magic
    /// state, if any, of a double.
    /// </summary>
    private const double SimpleDoubleComparisonEpsilon = 9E-06;
    /// <summary>The last known width of the control.</summary>
    private double _knownWidth;
    /// <summary>The last known height of the control.</summary>
    private double _knownHeight;
    /// <summary>
    /// A set of custom animation adapters used to update the animation
    /// storyboards when the size of the control changes.
    /// </summary>
    private List<RelativeAnimatingContentControl.AnimationValueAdapter> _specialAnimations;

    /// <summary>
    /// Initializes a new instance of the RelativeAnimatingContentControl
    /// type.
    /// </summary>
    public RelativeAnimatingContentControl() => ((FrameworkElement) this).SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);

    /// <summary>Handles the size changed event.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event arguments.</param>
    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (e == null || e.NewSize.Height <= 0.0 || e.NewSize.Width <= 0.0)
        return;
      Size newSize = e.NewSize;
      this._knownWidth = newSize.Width;
      newSize = e.NewSize;
      this._knownHeight = newSize.Height;
      ((UIElement) this).Clip = (Geometry) new RectangleGeometry()
      {
        Rect = new Rect(0.0, 0.0, this._knownWidth, this._knownHeight)
      };
      this.UpdateAnyAnimationValues();
    }

    /// <summary>
    /// Walks through the known storyboards in the control's template that
    /// may contain magic double animation values, storing them for future
    /// use and updates.
    /// </summary>
    private void UpdateAnyAnimationValues()
    {
      if (this._knownHeight <= 0.0 || this._knownWidth <= 0.0)
        return;
      if (this._specialAnimations == null)
      {
        this._specialAnimations = new List<RelativeAnimatingContentControl.AnimationValueAdapter>();
        foreach (VisualStateGroup visualStateGroup in (IEnumerable) VisualStateManager.GetVisualStateGroups((FrameworkElement) this))
        {
          if (visualStateGroup != null)
          {
            foreach (VisualState state in (IEnumerable) visualStateGroup.States)
            {
              if (state != null)
              {
                Storyboard storyboard = state.Storyboard;
                if (storyboard != null)
                {
                  foreach (Timeline child in (PresentationFrameworkCollection<Timeline>) storyboard.Children)
                  {
                    DoubleAnimation da1 = child as DoubleAnimation;
                    DoubleAnimationUsingKeyFrames da2 = child as DoubleAnimationUsingKeyFrames;
                    if (da1 != null)
                      this.ProcessDoubleAnimation(da1);
                    else if (da2 != null)
                      this.ProcessDoubleAnimationWithKeys(da2);
                  }
                }
              }
            }
          }
        }
      }
      this.UpdateKnownAnimations();
    }

    /// <summary>
    /// Walks through all special animations, updating based on the current
    /// size of the control.
    /// </summary>
    private void UpdateKnownAnimations()
    {
      foreach (RelativeAnimatingContentControl.AnimationValueAdapter specialAnimation in this._specialAnimations)
        specialAnimation.UpdateWithNewDimension(this._knownWidth, this._knownHeight);
    }

    /// <summary>
    /// Processes a double animation with keyframes, looking for known
    /// special values to store with an adapter.
    /// </summary>
    /// <param name="da">The double animation using key frames instance.</param>
    private void ProcessDoubleAnimationWithKeys(DoubleAnimationUsingKeyFrames da)
    {
      foreach (DoubleKeyFrame keyFrame in (PresentationFrameworkCollection<DoubleKeyFrame>) da.KeyFrames)
      {
        RelativeAnimatingContentControl.DoubleAnimationDimension? dimensionFromMagicNumber = RelativeAnimatingContentControl.GeneralAnimationValueAdapter<DoubleKeyFrame>.GetDimensionFromMagicNumber(keyFrame.Value);
        if (dimensionFromMagicNumber.HasValue)
          this._specialAnimations.Add((RelativeAnimatingContentControl.AnimationValueAdapter) new RelativeAnimatingContentControl.DoubleAnimationFrameAdapter(dimensionFromMagicNumber.Value, keyFrame));
      }
    }

    /// <summary>
    /// Processes a double animation looking for special values.
    /// </summary>
    /// <param name="da">The double animation instance.</param>
    private void ProcessDoubleAnimation(DoubleAnimation da)
    {
      RelativeAnimatingContentControl.DoubleAnimationDimension? dimensionFromMagicNumber;
      if (da.To.HasValue)
      {
        dimensionFromMagicNumber = RelativeAnimatingContentControl.GeneralAnimationValueAdapter<DoubleAnimation>.GetDimensionFromMagicNumber(da.To.Value);
        if (dimensionFromMagicNumber.HasValue)
          this._specialAnimations.Add((RelativeAnimatingContentControl.AnimationValueAdapter) new RelativeAnimatingContentControl.DoubleAnimationToAdapter(dimensionFromMagicNumber.Value, da));
      }
      double? nullable = da.From;
      if (!nullable.HasValue)
        return;
      nullable = da.To;
      dimensionFromMagicNumber = RelativeAnimatingContentControl.GeneralAnimationValueAdapter<DoubleAnimation>.GetDimensionFromMagicNumber(nullable.Value);
      if (dimensionFromMagicNumber.HasValue)
        this._specialAnimations.Add((RelativeAnimatingContentControl.AnimationValueAdapter) new RelativeAnimatingContentControl.DoubleAnimationFromAdapter(dimensionFromMagicNumber.Value, da));
    }

    /// <summary>
    /// A selection of dimensions of interest for updating an animation.
    /// </summary>
    private enum DoubleAnimationDimension
    {
      /// <summary>The width (horizontal) dimension.</summary>
      Width,
      /// <summary>The height (vertical) dimension.</summary>
      Height,
    }

    /// <summary>
    /// A simple class designed to store information about a specific
    /// animation instance and its properties. Able to update the values at
    /// runtime.
    /// </summary>
    private abstract class AnimationValueAdapter
    {
      /// <summary>
      /// Initializes a new instance of the AnimationValueAdapter type.
      /// </summary>
      /// <param name="dimension">The dimension of interest for updates.</param>
      public AnimationValueAdapter(
        RelativeAnimatingContentControl.DoubleAnimationDimension dimension)
      {
        this.Dimension = dimension;
      }

      /// <summary>Gets the dimension of interest for the control.</summary>
      public RelativeAnimatingContentControl.DoubleAnimationDimension Dimension { get; private set; }

      /// <summary>
      /// Updates the original instance based on new dimension information
      /// from the control. Takes both and allows the subclass to make the
      /// decision on which ratio, values, and dimension to use.
      /// </summary>
      /// <param name="width">The width of the control.</param>
      /// <param name="height">The height of the control.</param>
      public abstract void UpdateWithNewDimension(double width, double height);
    }

    private abstract class GeneralAnimationValueAdapter<T> : 
      RelativeAnimatingContentControl.AnimationValueAdapter
    {
      /// <summary>
      /// The ratio based on the original magic value, used for computing
      /// the updated animation property of interest when the size of the
      /// control changes.
      /// </summary>
      private double _ratio;

      /// <summary>Stores the animation instance.</summary>
      protected T Instance { get; set; }

      /// <summary>
      /// Gets the value of the underlying property of interest.
      /// </summary>
      /// <returns>Returns the value of the property.</returns>
      protected abstract double GetValue();

      /// <summary>
      /// Sets the value for the underlying property of interest.
      /// </summary>
      /// <param name="newValue">The new value for the property.</param>
      protected abstract void SetValue(double newValue);

      /// <summary>
      /// Gets the initial value (minus the magic number portion) that the
      /// designer stored within the visual state animation property.
      /// </summary>
      protected double InitialValue { get; private set; }

      /// <summary>
      /// Initializes a new instance of the GeneralAnimationValueAdapter
      /// type.
      /// </summary>
      /// <param name="d">The dimension of interest.</param>
      /// <param name="instance">The animation type instance.</param>
      [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Should not have any undesirable side-effects.")]
      public GeneralAnimationValueAdapter(
        RelativeAnimatingContentControl.DoubleAnimationDimension d,
        T instance)
        : base(d)
      {
        this.Instance = instance;
        this.InitialValue = this.StripMagicNumberOff(this.GetValue());
        this._ratio = this.InitialValue / 100.0;
      }

      /// <summary>
      /// Approximately removes the magic number state from a value.
      /// </summary>
      /// <param name="number">The initial number.</param>
      /// <returns>Returns a double with an adjustment for the magic
      /// portion of the number.</returns>
      public double StripMagicNumberOff(double number) => this.Dimension == RelativeAnimatingContentControl.DoubleAnimationDimension.Width ? number - 0.1 : number - 0.2;

      /// <summary>
      /// Retrieves the dimension, if any, from the number. If the number
      /// is not magic, null is returned instead.
      /// </summary>
      /// <param name="number">The double value.</param>
      /// <returns>Returs a double animation dimension, if the number was
      /// partially magic; otherwise, returns null.</returns>
      public static RelativeAnimatingContentControl.DoubleAnimationDimension? GetDimensionFromMagicNumber(
        double number)
      {
        double num1 = Math.Round(number);
        double num2 = Math.Abs(number - num1);
        if (num2 >= 0.09999100000000001 && num2 <= 0.100009)
          return new RelativeAnimatingContentControl.DoubleAnimationDimension?(RelativeAnimatingContentControl.DoubleAnimationDimension.Width);
        return num2 >= 0.199991 && num2 <= 0.20000900000000002 ? new RelativeAnimatingContentControl.DoubleAnimationDimension?(RelativeAnimatingContentControl.DoubleAnimationDimension.Height) : new RelativeAnimatingContentControl.DoubleAnimationDimension?();
      }

      /// <summary>
      /// Updates the animation instance based on the dimensions of the
      /// control.
      /// </summary>
      /// <param name="width">The width of the control.</param>
      /// <param name="height">The height of the control.</param>
      public override void UpdateWithNewDimension(double width, double height) => this.UpdateValue(this.Dimension == RelativeAnimatingContentControl.DoubleAnimationDimension.Width ? width : height);

      /// <summary>Updates the value of the property.</summary>
      /// <param name="sizeToUse">The size of interest to use with a ratio
      /// computation.</param>
      private void UpdateValue(double sizeToUse) => this.SetValue(sizeToUse * this._ratio);
    }

    /// <summary>Adapter for DoubleAnimation's To property.</summary>
    private class DoubleAnimationToAdapter : 
      RelativeAnimatingContentControl.GeneralAnimationValueAdapter<DoubleAnimation>
    {
      /// <summary>
      /// Gets the value of the underlying property of interest.
      /// </summary>
      /// <returns>Returns the value of the property.</returns>
      protected override double GetValue() => this.Instance.To.Value;

      /// <summary>
      /// Sets the value for the underlying property of interest.
      /// </summary>
      /// <param name="newValue">The new value for the property.</param>
      protected override void SetValue(double newValue) => this.Instance.To = new double?(newValue);

      /// <summary>
      /// Initializes a new instance of the DoubleAnimationToAdapter type.
      /// </summary>
      /// <param name="dimension">The dimension of interest.</param>
      /// <param name="instance">The instance of the animation type.</param>
      public DoubleAnimationToAdapter(
        RelativeAnimatingContentControl.DoubleAnimationDimension dimension,
        DoubleAnimation instance)
        : base(dimension, instance)
      {
      }
    }

    /// <summary>Adapter for DoubleAnimation's From property.</summary>
    private class DoubleAnimationFromAdapter : 
      RelativeAnimatingContentControl.GeneralAnimationValueAdapter<DoubleAnimation>
    {
      /// <summary>
      /// Gets the value of the underlying property of interest.
      /// </summary>
      /// <returns>Returns the value of the property.</returns>
      protected override double GetValue() => this.Instance.From.Value;

      /// <summary>
      /// Sets the value for the underlying property of interest.
      /// </summary>
      /// <param name="newValue">The new value for the property.</param>
      protected override void SetValue(double newValue) => this.Instance.From = new double?(newValue);

      /// <summary>
      /// Initializes a new instance of the DoubleAnimationFromAdapter
      /// type.
      /// </summary>
      /// <param name="dimension">The dimension of interest.</param>
      /// <param name="instance">The instance of the animation type.</param>
      public DoubleAnimationFromAdapter(
        RelativeAnimatingContentControl.DoubleAnimationDimension dimension,
        DoubleAnimation instance)
        : base(dimension, instance)
      {
      }
    }

    /// <summary>Adapter for double key frames.</summary>
    private class DoubleAnimationFrameAdapter : 
      RelativeAnimatingContentControl.GeneralAnimationValueAdapter<DoubleKeyFrame>
    {
      /// <summary>
      /// Gets the value of the underlying property of interest.
      /// </summary>
      /// <returns>Returns the value of the property.</returns>
      protected override double GetValue() => this.Instance.Value;

      /// <summary>
      /// Sets the value for the underlying property of interest.
      /// </summary>
      /// <param name="newValue">The new value for the property.</param>
      protected override void SetValue(double newValue) => this.Instance.Value = newValue;

      /// <summary>
      /// Initializes a new instance of the DoubleAnimationFrameAdapter
      /// type.
      /// </summary>
      /// <param name="dimension">The dimension of interest.</param>
      /// <param name="frame">The instance of the animation type.</param>
      public DoubleAnimationFrameAdapter(
        RelativeAnimatingContentControl.DoubleAnimationDimension dimension,
        DoubleKeyFrame frame)
        : base(dimension, frame)
      {
      }
    }
  }
}
