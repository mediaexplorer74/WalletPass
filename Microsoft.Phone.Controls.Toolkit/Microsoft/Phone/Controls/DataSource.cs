// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DataSource
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  public abstract class DataSource : ILoopingSelectorDataSource
  {
    private DateTimeWrapper _selectedItem;

    public object GetNext(object relativeTo)
    {
      DateTime? relativeTo1 = this.GetRelativeTo(((DateTimeWrapper) relativeTo).DateTime, 1);
      return relativeTo1.HasValue ? (object) new DateTimeWrapper(relativeTo1.Value) : (object) (DateTimeWrapper) null;
    }

    public object GetPrevious(object relativeTo)
    {
      DateTime? relativeTo1 = this.GetRelativeTo(((DateTimeWrapper) relativeTo).DateTime, -1);
      return relativeTo1.HasValue ? (object) new DateTimeWrapper(relativeTo1.Value) : (object) (DateTimeWrapper) null;
    }

    protected abstract DateTime? GetRelativeTo(DateTime relativeDate, int delta);

    public object SelectedItem
    {
      get => (object) this._selectedItem;
      set
      {
        if (value == this._selectedItem)
          return;
        DateTimeWrapper dateTimeWrapper = (DateTimeWrapper) value;
        if (dateTimeWrapper == null || this._selectedItem == null || dateTimeWrapper.DateTime != this._selectedItem.DateTime)
        {
          object selectedItem = (object) this._selectedItem;
          this._selectedItem = dateTimeWrapper;
          EventHandler<SelectionChangedEventArgs> selectionChanged = this.SelectionChanged;
          if (null != selectionChanged)
            selectionChanged((object) this, new SelectionChangedEventArgs((IList) new object[1]
            {
              selectedItem
            }, (IList) new object[1]
            {
              (object) this._selectedItem
            }));
        }
      }
    }

    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
  }
}
