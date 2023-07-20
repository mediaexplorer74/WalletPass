// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PhoneHelper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.Phone.Controls
{
  /// <summary>Helper for the phone.</summary>
  /// <remarks>
  /// All orientations are condensed into portrait and landscape, where landscape includes <see cref="F:Microsoft.Phone.Controls.PageOrientation.None" />.
  /// </remarks>
  internal static class PhoneHelper
  {
    /// <summary>The height of the SIP in landscape orientation.</summary>
    public const double SipLandscapeHeight = 259.0;
    /// <summary>The height of the SIP in portrait orientation.</summary>
    public const double SipPortraitHeight = 339.0;
    /// <summary>
    /// The height of the SIP text completion in either orientation.
    /// </summary>
    public const double SipTextCompletionHeight = 62.0;

    /// <summary>
    /// Gets the current <see cref="T:PhoneApplicationFrame" />.
    /// </summary>
    /// <param name="phoneApplicationFrame">The current <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns><code>true</code> if the current <see cref="T:PhoneApplicationFrame" /> was found; <code>false</code> otherwise.</returns>
    public static bool TryGetPhoneApplicationFrame(out PhoneApplicationFrame phoneApplicationFrame)
    {
      phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
      return phoneApplicationFrame != null;
    }

    /// <summary>
    /// Determines whether a <see cref="T:PhoneApplicationFrame" /> is oriented as portrait.
    /// </summary>
    /// <param name="phoneApplicationFrame">The <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns><code>true</code> if the <see cref="T:PhoneApplicationFrame" /> is oriented as portrait; <code>false</code> otherwise.</returns>
    public static bool IsPortrait(this PhoneApplicationFrame phoneApplicationFrame) => ((PageOrientation) 13 & phoneApplicationFrame.Orientation) == phoneApplicationFrame.Orientation;

    /// <summary>
    /// Gets the correct width of a <see cref="T:PhoneApplicationFrame" /> in either orientation.
    /// </summary>
    /// <param name="phoneApplicationFrame">The <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns>The width.</returns>
    public static double GetUsefulWidth(this PhoneApplicationFrame phoneApplicationFrame) => phoneApplicationFrame.IsPortrait() ? ((FrameworkElement) phoneApplicationFrame).ActualWidth : ((FrameworkElement) phoneApplicationFrame).ActualHeight;

    /// <summary>
    /// Gets the correct height of a <see cref="T:PhoneApplicationFrame" /> in either orientation.
    /// </summary>
    /// <param name="phoneApplicationFrame">The <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns>The height.</returns>
    public static double GetUsefulHeight(this PhoneApplicationFrame phoneApplicationFrame) => phoneApplicationFrame.IsPortrait() ? ((FrameworkElement) phoneApplicationFrame).ActualHeight : ((FrameworkElement) phoneApplicationFrame).ActualWidth;

    /// <summary>
    /// Gets the correct <see cref="T:Size" /> of a <see cref="T:PhoneApplicationFrame" />.
    /// </summary>
    /// <param name="phoneApplicationFrame">The <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns>The <see cref="T:Size" />.</returns>
    public static Size GetUsefulSize(this PhoneApplicationFrame phoneApplicationFrame) => new Size(phoneApplicationFrame.GetUsefulWidth(), phoneApplicationFrame.GetUsefulHeight());

    /// <summary>
    /// Gets the focused <see cref="T:TextBox" />, if there is one.
    /// </summary>
    /// <param name="textBox">The <see cref="T:TextBox" />.</param>
    /// <returns><code>true</code> if there is a focused <see cref="T:TextBox" />; <code>false</code> otherwise.</returns>
    private static bool TryGetFocusedTextBox(out TextBox textBox)
    {
      textBox = FocusManager.GetFocusedElement() as TextBox;
      return textBox != null;
    }

    /// <summary>Determines whether the SIP is shown.</summary>
    /// <returns><code>true</code> if the SIP is shown; <code>false</code> otherwise.</returns>
    public static bool IsSipShown() => PhoneHelper.TryGetFocusedTextBox(out TextBox _);

    /// <summary>
    /// Determines whether the <see cref="T:TextBox" /> would show the SIP text completion.
    /// </summary>
    /// <param name="textBox">The <see cref="T:TextBox" />.</param>
    /// <returns><code>true</code> if the <see cref="T:TextBox" /> woudl show the SIP text completion; <code>false</code> otherwise.</returns>
    public static bool IsSipTextCompletionShown(this TextBox textBox)
    {
      if (textBox.InputScope == null)
        return false;
      foreach (InputScopeName name in (IEnumerable) textBox.InputScope.Names)
      {
        switch (name.NameValue - 49)
        {
          case 0:
          case 1:
            return true;
          default:
            continue;
        }
      }
      return false;
    }

    /// <summary>
    /// Gets the <see cref="T:Size" /> covered by the SIP when it is shown.
    /// </summary>
    /// <param name="phoneApplicationFrame">The <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns>The <see cref="T:Size" />.</returns>
    public static Size GetSipCoveredSize(this PhoneApplicationFrame phoneApplicationFrame)
    {
      if (!PhoneHelper.IsSipShown())
        return new Size(0.0, 0.0);
      double usefulWidth = phoneApplicationFrame.GetUsefulWidth();
      double num = phoneApplicationFrame.IsPortrait() ? 339.0 : 259.0;
      TextBox textBox;
      if (PhoneHelper.TryGetFocusedTextBox(out textBox) && textBox.IsSipTextCompletionShown())
        num += 62.0;
      return new Size(usefulWidth, num);
    }

    /// <summary>
    /// Gets the <see cref="T:Size" /> uncovered by the SIP when it is shown.
    /// </summary>
    /// <param name="phoneApplicationFrame">The <see cref="T:PhoneApplicationFrame" />.</param>
    /// <returns>The <see cref="T:Size" />.</returns>
    public static Size GetSipUncoveredSize(this PhoneApplicationFrame phoneApplicationFrame) => new Size(phoneApplicationFrame.GetUsefulWidth(), phoneApplicationFrame.GetUsefulHeight() - phoneApplicationFrame.GetSipCoveredSize().Height);
  }
}
