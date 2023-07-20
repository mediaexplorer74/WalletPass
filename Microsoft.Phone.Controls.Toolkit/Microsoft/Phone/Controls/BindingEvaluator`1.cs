// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.BindingEvaluator`1
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A framework element that permits a binding to be evaluated in a new data
  /// context leaf node.
  /// </summary>
  /// <typeparam name="T">The type of dynamic binding to return.</typeparam>
  internal class BindingEvaluator<T> : FrameworkElement
  {
    /// <summary>
    /// Gets or sets the string value binding used by the control.
    /// </summary>
    private Binding _binding;
    /// <summary>Identifies the Value dependency property.</summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (T), typeof (BindingEvaluator<T>), new PropertyMetadata((object) default (T)));

    /// <summary>Gets or sets the data item string value.</summary>
    public T Value
    {
      get => (T) ((DependencyObject) this).GetValue(BindingEvaluator<T>.ValueProperty);
      set => ((DependencyObject) this).SetValue(BindingEvaluator<T>.ValueProperty, (object) value);
    }

    /// <summary>Gets or sets the value binding.</summary>
    public Binding ValueBinding
    {
      get => this._binding;
      set
      {
        this._binding = value;
        this.SetBinding(BindingEvaluator<T>.ValueProperty, this._binding);
      }
    }

    /// <summary>
    /// Initializes a new instance of the BindingEvaluator class.
    /// </summary>
    public BindingEvaluator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the BindingEvaluator class,
    /// setting the initial binding to the provided parameter.
    /// </summary>
    /// <param name="binding">The initial string value binding.</param>
    public BindingEvaluator(Binding binding) => this.SetBinding(BindingEvaluator<T>.ValueProperty, binding);

    /// <summary>
    /// Clears the data context so that the control does not keep a
    /// reference to the last-looked up item.
    /// </summary>
    public void ClearDataContext() => this.DataContext = (object) null;

    /// <summary>
    /// Updates the data context of the framework element and returns the
    /// updated binding value.
    /// </summary>
    /// <param name="o">The object to use as the data context.</param>
    /// <param name="clearDataContext">If set to true, this parameter will
    /// clear the data context immediately after retrieving the value.</param>
    /// <returns>Returns the evaluated T value of the bound dependency
    /// property.</returns>
    public T GetDynamicValue(object o, bool clearDataContext)
    {
      this.DataContext = o;
      T dynamicValue = this.Value;
      if (clearDataContext)
        this.DataContext = (object) null;
      return dynamicValue;
    }

    /// <summary>
    /// Updates the data context of the framework element and returns the
    /// updated binding value.
    /// </summary>
    /// <param name="o">The object to use as the data context.</param>
    /// <returns>Returns the evaluated T value of the bound dependency
    /// property.</returns>
    public T GetDynamicValue(object o)
    {
      this.DataContext = o;
      return this.Value;
    }
  }
}
