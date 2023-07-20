// Decompiled with JetBrains decompiler
// Type: WalletPass.ListLoopingDataSource`1
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;

namespace WalletPass
{
  public class ListLoopingDataSource<T> : LoopingDataSourceBase
  {
    private LinkedList<T> linkedList;
    private List<LinkedListNode<T>> sortedList;
    private ListLoopingDataSource<T>.NodeComparer nodeComparer;
    private IComparer<T> comparer;

    public IEnumerable<T> Items
    {
      get => (IEnumerable<T>) this.linkedList;
      set => this.SetItemCollection(value);
    }

    private void SetItemCollection(IEnumerable<T> collection)
    {
      this.linkedList = new LinkedList<T>(collection);
      this.sortedList = new List<LinkedListNode<T>>(this.linkedList.Count);
      for (LinkedListNode<T> linkedListNode = this.linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
        this.sortedList.Add(linkedListNode);
      IComparer<T> comparer = this.comparer;
      if (comparer == null)
      {
        if (!typeof (IComparable<T>).IsAssignableFrom(typeof (T)))
          throw new InvalidOperationException("There is no default comparer for this type of item. You must set one.");
        comparer = (IComparer<T>) System.Collections.Generic.Comparer<T>.Default;
      }
      this.nodeComparer = new ListLoopingDataSource<T>.NodeComparer(comparer);
      this.sortedList.Sort((IComparer<LinkedListNode<T>>) this.nodeComparer);
    }

    public IComparer<T> Comparer
    {
      get => this.comparer;
      set => this.comparer = value;
    }

    public override object GetNext(object relativeTo)
    {
      int index = this.sortedList.BinarySearch(new LinkedListNode<T>((T) relativeTo), (IComparer<LinkedListNode<T>>) this.nodeComparer);
      return index < 0 ? (object) default (T) : (object) (this.sortedList[index].Next ?? this.linkedList.First).Value;
    }

    public override object GetPrevious(object relativeTo)
    {
      int index = this.sortedList.BinarySearch(new LinkedListNode<T>((T) relativeTo), (IComparer<LinkedListNode<T>>) this.nodeComparer);
      return index < 0 ? (object) default (T) : (object) (this.sortedList[index].Previous ?? this.linkedList.Last).Value;
    }

    private class NodeComparer : IComparer<LinkedListNode<T>>
    {
      private IComparer<T> comparer;

      public NodeComparer(IComparer<T> comparer) => this.comparer = comparer;

      public int Compare(LinkedListNode<T> x, LinkedListNode<T> y) => this.comparer.Compare(x.Value, y.Value);
    }
  }
}
