// WalletPass.RichTextBoxBindingClass


using System;
using System.Text.RegularExpressions;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Media;

namespace WalletPass
{
  public static class RichTextBoxBindingClass
  {
    private static Regex RE_URL = new Regex("(?#Protocol)(?:(?:ht|f)tp(?:s?)\\:\\/\\/|~/|/)?(?#Username:Password)(?:\\w+:\\w+@)?(?#Subdomains)(?:(?:[-\\w]+\\.)+(?#TopLevel Domains)(?:com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|travel|[a-z]{2}))(?#Port)(?::[\\d]{1,5})?(?#Directories)(?:(?:(?:/(?:[-\\w~!$+|.,=]|%[a-f\\d]{2})+)+|/)+|\\?|#)?(?#Query)(?:(?:\\?(?:[-\\w~!$+|.,*:]|%[a-f\\d{2}])+=(?:[-\\w~!$+|.,*:=]|%[a-f\\d]{2})*)(?:&(?:[-\\w~!$+|.,*:]|%[a-f\\d{2}])+=(?:[-\\w~!$+|.,*:=]|%[a-f\\d]{2})*)*)*(?#Anchor)(?:#(?:[-\\w~!$+|.,*:=]|%[a-f\\d]{2})*)?");
    private static Regex RE_EMAIL = new Regex("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))$");
    public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof (string), typeof (RichTextBoxBindingClass), new PropertyMetadata((object) null, new PropertyChangedCallback(RichTextBoxBindingClass.OnContentChanged)));

    public static string GetContent(DependencyObject d) => d.GetValue(RichTextBoxBindingClass.ContentProperty) as string;

    public static void SetContent(DependencyObject d, string value) => d.SetValue(RichTextBoxBindingClass.ContentProperty, (object) value);

    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is RichTextBox richTextBox))
        return;
      ((PresentationFrameworkCollection<Block>) richTextBox.Blocks).Clear();
      string newValue = (string) e.NewValue;
      int startIndex1 = 0;
      bool flag1 = false;
      Paragraph paragraph1 = new Paragraph();
      Paragraph paragraph2 = new Paragraph();
      bool flag2 = false;
      foreach (Match match in RichTextBoxBindingClass.RE_URL.Matches(newValue))
      {
        flag1 = true;
        if (match.Index != startIndex1)
        {
          string str = newValue.Substring(startIndex1, match.Index - startIndex1);
          ((PresentationFrameworkCollection<Inline>) paragraph1.Inlines).Add((Inline) new Run()
          {
            Text = str
          });
        }
        string uriString = match.Value;
        Uri result;
        if (!Uri.TryCreate(uriString, UriKind.Absolute, out result) && !uriString.StartsWith("http://"))
          Uri.TryCreate("http://" + uriString, UriKind.Absolute, out result);
        if (result != (Uri) null)
        {
          Hyperlink hyperlink = new Hyperlink();
          hyperlink.NavigateUri = result;
          ((Span) hyperlink).Inlines.Add(uriString);
          hyperlink.TargetName = "_blank";
          hyperlink.MouseOverForeground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 132, (byte) 193, byte.MaxValue));
          ((TextElement) hyperlink).Foreground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 123, (byte) 215));
          ((TextElement) hyperlink).FontSize = 25.0;
          ((PresentationFrameworkCollection<Inline>) paragraph1.Inlines).Add((Inline) hyperlink);
        }
        else
          paragraph1.Inlines.Add(uriString);
        startIndex1 = match.Index + match.Length;
      }
      int startIndex2 = 0;
      foreach (Match match in RichTextBoxBindingClass.RE_EMAIL.Matches(newValue))
      {
        flag1 = true;
        flag2 = true;
        if (match.Index != startIndex2)
        {
          string str = newValue.Substring(startIndex2, match.Index - startIndex2);
          ((PresentationFrameworkCollection<Inline>) paragraph2.Inlines).Add((Inline) new Run()
          {
            Text = str
          });
        }
        string str1 = match.Value;
        Uri result = (Uri) null;
        Uri.TryCreate("mailto:" + str1, UriKind.Absolute, out result);
        if (result != (Uri) null)
        {
          Hyperlink hyperlink = new Hyperlink();
          hyperlink.NavigateUri = result;
          ((Span) hyperlink).Inlines.Add(str1);
          hyperlink.TargetName = "_blank";
          hyperlink.MouseOverForeground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 132, (byte) 193, byte.MaxValue));
          ((TextElement) hyperlink).Foreground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 123, (byte) 215));
          ((TextElement) hyperlink).FontSize = 25.0;
          ((PresentationFrameworkCollection<Inline>) paragraph2.Inlines).Add((Inline) hyperlink);
        }
        else
          paragraph2.Inlines.Add(str1);
        startIndex2 = match.Index + match.Length;
      }
      if (!flag1)
        ((PresentationFrameworkCollection<Inline>) paragraph1.Inlines).Add((Inline) new Run()
        {
          Text = newValue
        });
      if (flag2)
        paragraph1 = paragraph2;
      ((PresentationFrameworkCollection<Block>) richTextBox.Blocks).Add((Block) paragraph1);
    }
  }
}
