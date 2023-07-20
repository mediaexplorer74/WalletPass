// WalletPass.LoopingDataSourceBase


//using Microsoft.Phone.Controls.Primitives;
//using Intersoft.Crosslight.WinPhone.Controls;
using System;
using System.Collections;
using Windows.UI.Xaml.Controls;
//using System.Windows.Controls;

namespace WalletPass
{
  public abstract class LoopingDataSourceBase : ILoopingSelectorDataSource
  {
    private object selectedItem;

    public abstract object GetNext(object relativeTo);

    public abstract object GetPrevious(object relativeTo);

    public object SelectedItem
    {
      get => this.selectedItem;
      set
      {
        if (object.Equals(this.selectedItem, value))
          return;
        object selectedItem = this.selectedItem;
        this.selectedItem = value;
        this.OnSelectionChanged(selectedItem, this.selectedItem);
      }
    }

    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

    protected virtual void OnSelectionChanged(object oldSelectedItem, object newSelectedItem)
    {
      EventHandler<SelectionChangedEventArgs> selectionChanged = this.SelectionChanged;

      if (selectionChanged == null)
        return;
      
      //RnD
      //selectionChanged((object) this, new SelectionChangedEventArgs((IList) new object[1]
      //{
      //  oldSelectedItem
      //}, (IList) new object[1]{ newSelectedItem }));
    }
  }
}
