// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.CollectionChangeListener`1
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// CollectionChangedListener class.
  /// Generic template that can used as a middle man of events from a collection to another collection.
  /// It has a CollectionChanged event handler that will forward the calls to concrete implementations
  /// which can in turn apply them into a target collection.
  /// </summary>
  /// <typeparam name="T">Type of element in the collection that implements the INotifyCollectionChanged</typeparam>
  internal abstract class CollectionChangeListener<T>
  {
    /// <summary>
    /// CollectionChanged handler.
    /// Will forward the events to concrete implementations.
    /// </summary>
    /// <param name="sender">the sender value</param>
    /// <param name="e">the e value</param>
    protected void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e), "NotifyCollectionChangedEventArgs");
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          for (int index = e.NewItems.Count - 1; index >= 0; --index)
            this.InsertItemInternal(e.NewStartingIndex, (T) e.NewItems[index]);
          break;
        case NotifyCollectionChangedAction.Remove:
          IEnumerator enumerator = e.OldItems.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
              this.RemoveItemInternal((T) enumerator.Current);
            break;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        case NotifyCollectionChangedAction.Replace:
          foreach (T oldItem in (IEnumerable) e.OldItems)
            this.RemoveItemInternal(oldItem);
          for (int index = e.NewItems.Count - 1; index >= 0; --index)
            this.InsertItemInternal(e.NewStartingIndex, (T) e.NewItems[index]);
          break;
        case NotifyCollectionChangedAction.Move:
          Debug.Assert(e.OldItems.Count == 1, "Expecting only one item in the old items collection");
          Debug.Assert(e.NewItems.Count == 1, "Expecting only one item in the new items collection");
          this.MoveInternal((T) e.OldItems[0], e.NewStartingIndex);
          break;
        case NotifyCollectionChangedAction.Reset:
          this.ResetInternal();
          this.AddRangeInternal((IEnumerable<T>) sender);
          break;
        default:
          Debug.Assert(false, "Did we miss a new event?");
          break;
      }
    }

    /// <summary>Adds Range to the target collection</summary>
    /// <param name="items">the collection items</param>
    protected void AddRangeInternal(IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      foreach (T obj in items)
        this.AddInternal(obj);
    }

    /// <summary>
    /// Concrete implementations will have a chance to insert the object at the specified index into a target collection
    /// </summary>
    /// <param name="index">index where the object was inserted</param>
    /// <param name="obj">object to add</param>
    protected abstract void InsertItemInternal(int index, T obj);

    /// <summary>
    /// Concrete implementations will have a chance to remove the object from a target collection
    /// </summary>
    /// <param name="obj">object to remove</param>
    protected abstract void RemoveItemInternal(T obj);

    /// <summary>
    /// Concrete implementations will have a chance to reset the contents target collection
    /// </summary>
    protected abstract void ResetInternal();

    /// <summary>
    /// Concrete implementations will have a chance to add the item into a target collection
    /// </summary>
    /// <param name="obj">object to add</param>
    protected abstract void AddInternal(T obj);

    /// <summary>
    /// Concrete implementations will have a chance to move the end object from the old index to the new index
    /// </summary>
    /// <param name="obj">object to move</param>
    /// <param name="newIndex">new index</param>
    protected abstract void MoveInternal(T obj, int newIndex);
  }
}
