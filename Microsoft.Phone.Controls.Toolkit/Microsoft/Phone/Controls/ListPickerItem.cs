// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ListPickerItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Class that implements a container for the ListPicker control.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(GroupName = "SelectionStates", Name = "Unselected")]
  [TemplateVisualState(GroupName = "SelectionStates", Name = "Selected")]
  public class ListPickerItem : ContentControl
  {
    private const string SelectionStatesGroupName = "SelectionStates";
    private const string SelectionStatesUnselectedStateName = "Unselected";
    private const string SelectionStatesSelectedStateName = "Selected";
    private bool _isSelected;

    /// <summary>
    /// Initializes a new instance of the ListPickerItem class.
    /// </summary>
    public ListPickerItem() => ((Control) this).DefaultStyleKey = (object) typeof (ListPickerItem);

    /// <summary>
    /// Builds the visual tree for the control when a new template is applied.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      VisualStateManager.GoToState((Control) this, this.IsSelected ? "Selected" : "Unselected", false);
    }

    internal bool IsSelected
    {
      get => this._isSelected;
      set
      {
        this._isSelected = value;
        VisualStateManager.GoToState((Control) this, this._isSelected ? "Selected" : "Unselected", true);
      }
    }
  }
}
