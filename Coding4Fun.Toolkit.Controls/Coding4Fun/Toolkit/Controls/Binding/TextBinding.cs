// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Binding.TextBinding
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls.Binding
{
  public class TextBinding
  {
    public static readonly DependencyProperty UpdateSourceOnChangeProperty = DependencyProperty.RegisterAttached("UpdateSourceOnChange", typeof (bool), typeof (TextBinding), new PropertyMetadata((object) false, new PropertyChangedCallback(TextBinding.OnUpdateSourceOnChangePropertyChanged)));

    public static bool GetUpdateSourceOnChange(DependencyObject obj) => (bool) obj.GetValue(TextBinding.UpdateSourceOnChangeProperty);

    public static void SetUpdateSourceOnChange(DependencyObject obj, bool value) => obj.SetValue(TextBinding.UpdateSourceOnChangeProperty, (object) value);

    private static void OnUpdateSourceOnChangePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue == e.OldValue)
        return;
      TextBinding.HandleUpdateSourceOnChangeEventAppend((object) obj, (bool) e.NewValue);
    }

    private static void HandleUpdateSourceOnChangeEventAppend(object sender, bool value)
    {
      switch (sender)
      {
        case TextBox _:
          TextBinding.HandleUpdateSourceOnChangeEventAppendTextBox(sender, value);
          break;
        case PasswordBox _:
          TextBinding.HandleUpdateSourceOnChangeEventAppendPassword(sender, value);
          break;
      }
    }

    private static void HandleUpdateSourceOnChangeEventAppendTextBox(object sender, bool value)
    {
      if (!(sender is TextBox textBox))
        return;
      if (value)
        textBox.TextChanged += new TextChangedEventHandler(TextBinding.UpdateSourceOnChangePropertyChanged);
      else
        textBox.TextChanged -= new TextChangedEventHandler(TextBinding.UpdateSourceOnChangePropertyChanged);
    }

    private static void HandleUpdateSourceOnChangeEventAppendPassword(object sender, bool value)
    {
      if (!(sender is PasswordBox passwordBox))
        return;
      if (value)
        passwordBox.PasswordChanged += new RoutedEventHandler(TextBinding.UpdateSourceOnChangePropertyChanged);
      else
        passwordBox.PasswordChanged -= new RoutedEventHandler(TextBinding.UpdateSourceOnChangePropertyChanged);
    }

    private static void UpdateSourceOnChangePropertyChanged(object sender, RoutedEventArgs e)
    {
      DependencyProperty dependancyPropertyForText = TextBinding.GetDependancyPropertyForText(sender);
      if (dependancyPropertyForText == null)
        return;
      ((FrameworkElement) sender).GetBindingExpression(dependancyPropertyForText)?.UpdateSource();
    }

    private static DependencyProperty GetDependancyPropertyForText(object sender)
    {
      DependencyProperty dependancyPropertyForText = (DependencyProperty) null;
      switch (sender)
      {
        case TextBox _:
          dependancyPropertyForText = TextBox.TextProperty;
          break;
        case PasswordBox _:
          dependancyPropertyForText = PasswordBox.PasswordProperty;
          break;
      }
      return dependancyPropertyForText;
    }
  }
}
