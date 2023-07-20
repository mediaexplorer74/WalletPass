// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapItemsSourceChangeManager
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Collections.Specialized;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Class MapItemsSourceChangeManager.
  /// Concrete implementation of <see cref="T:Microsoft.Phone.Maps.Toolkit.MapItemsSourceChangeManager" /> that will listen
  /// to change events in the source collection events to the the target Items collection
  /// </summary>
  internal class MapItemsSourceChangeManager : CollectionChangeListener<object>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.MapItemsSourceChangeManager" /> class
    /// </summary>
    /// <param name="sourceCollection">Source collection</param>
    public MapItemsSourceChangeManager(INotifyCollectionChanged sourceCollection)
    {
      this.SourceCollection = sourceCollection;
      this.SourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(((CollectionChangeListener<object>) this).CollectionChanged);
    }

    /// <summary>
    /// Gets or sets the target Items Collection of a <see cref="T:Microsoft.Phone.Maps.Toolkit.MapItemsControl" />
    /// </summary>
    public MapChildCollection Items { get; set; }

    /// <summary>Gets or sets the source collection</summary>
    private INotifyCollectionChanged SourceCollection { get; set; }

    /// <summary>
    /// Disconnects this manager from the source. No more events will be processed
    /// </summary>
    public void Disconnect()
    {
      this.SourceCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(((CollectionChangeListener<object>) this).CollectionChanged);
      this.SourceCollection = (INotifyCollectionChanged) null;
    }

    /// <summary>
    /// Inserts the object at the index in the target collection
    /// </summary>
    /// <param name="index">The index at which the object will be inserted</param>
    /// <param name="obj">Object to be inserted</param>
    protected override void InsertItemInternal(int index, object obj) => this.Items.InsertInternal(index, obj);

    /// <summary>Removes the object from the target collection</summary>
    /// <param name="obj">Object to be removed</param>
    protected override void RemoveItemInternal(object obj) => this.Items.RemoveInternal(this.Items.IndexOf(obj));

    /// <summary>Clears the target collection</summary>
    protected override void ResetInternal() => this.Items.ClearInternal();

    /// <summary>Adds the object to the target collection</summary>
    /// <param name="obj">Object to be added</param>
    protected override void AddInternal(object obj) => this.Items.AddInternal(obj);

    /// <summary>
    /// Moves the item to the new index within the target collection
    /// </summary>
    /// <param name="obj">Object to be moved</param>
    /// <param name="newIndex">New index</param>
    protected override void MoveInternal(object obj, int newIndex) => this.Items.MoveInternal(this.Items.IndexOf(obj), newIndex);
  }
}
