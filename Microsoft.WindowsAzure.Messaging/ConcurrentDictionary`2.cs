// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.ConcurrentDictionary`2
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.WindowsAzure.Messaging
{
  internal class ConcurrentDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable,
    IDisposable
  {
    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
    private ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

    public TValue AddOrUpdate(
      TKey key,
      TValue addValue,
      Func<TKey, TValue, TValue> updateValueFactory)
    {
      return this.WriteOperation<TValue>((Func<TValue>) (() =>
      {
        if (this.dictionary.ContainsKey(key))
          this.dictionary[key] = updateValueFactory(key, this.dictionary[key]);
        else
          this.dictionary.Add(key, addValue);
        return this.dictionary[key];
      }));
    }

    public TValue GetOrAdd(TKey key, TValue value) => this.WriteOperation<TValue>((Func<TValue>) (() =>
    {
      if (!this.dictionary.ContainsKey(key))
        this.dictionary.Add(key, value);
      return this.dictionary[key];
    }));

    public bool TryGetValue(TKey key, out TValue value)
    {
      TValue existValue = default (TValue);
      bool flag = this.ReadOperation<bool>((Func<bool>) (() => this.dictionary.TryGetValue(key, out existValue)));
      value = existValue;
      return flag;
    }

    public void Add(TKey key, TValue value) => this.WriteOperation((Action) (() => this.dictionary.Add(key, value)));

    public bool ContainsKey(TKey key) => this.ReadOperation<bool>((Func<bool>) (() => this.dictionary.ContainsKey(key)));

    public ICollection<TKey> Keys => (ICollection<TKey>) this.ReadOperation<List<TKey>>((Func<List<TKey>>) (() => new List<TKey>((IEnumerable<TKey>) this.dictionary.Keys)));

    public bool Remove(TKey key) => this.WriteOperation<bool>((Func<bool>) (() => this.dictionary.Remove(key)));

    public ICollection<TValue> Values => (ICollection<TValue>) this.ReadOperation<List<TValue>>((Func<List<TValue>>) (() => new List<TValue>((IEnumerable<TValue>) this.dictionary.Values)));

    public TValue this[TKey key]
    {
      get => this.ReadOperation<TValue>((Func<TValue>) (() => this.dictionary[key]));
      set => this.WriteOperation<TValue>((Func<TValue>) (() => this.dictionary[key] = value));
    }

    public void Add(KeyValuePair<TKey, TValue> item) => this.Add(item.Key, item.Value);

    public void Clear() => this.WriteOperation((Action) (() => this.dictionary.Clear()));

    public bool Contains(KeyValuePair<TKey, TValue> item) => this.ReadOperation<bool>((Func<bool>) (() => this.dictionary.Contains<KeyValuePair<TKey, TValue>>(item)));

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this.WriteOperation((Action) (() =>
    {
      int num = arrayIndex;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
        array[num++] = keyValuePair;
    }));

    public bool IsReadOnly => false;

    public int Count => this.ReadOperation<int>((Func<int>) (() => this.dictionary.Count));

    public bool Remove(KeyValuePair<TKey, TValue> item) => this.WriteOperation<bool>((Func<bool>) (() => this.dictionary.Remove(item.Key)));

    private T ReadOperation<T>(Func<T> func)
    {
      this.locker.EnterWriteLock();
      try
      {
        return func();
      }
      finally
      {
        this.locker.ExitWriteLock();
      }
    }

    private void WriteOperation(Action operation)
    {
      this.locker.EnterWriteLock();
      try
      {
        operation();
      }
      finally
      {
        this.locker.ExitWriteLock();
      }
    }

    private T WriteOperation<T>(Func<T> operation)
    {
      this.locker.EnterWriteLock();
      try
      {
        return operation();
      }
      finally
      {
        this.locker.ExitWriteLock();
      }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => (IEnumerator<KeyValuePair<TKey, TValue>>) this.dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.dictionary.GetEnumerator();

    public void Dispose()
    {
      if (this.locker == null)
        return;
      this.locker.Dispose();
      this.locker = (ReaderWriterLockSlim) null;
    }
  }
}
