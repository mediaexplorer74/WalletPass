// WindowsPhone.MVVM.Tombstone.TombstoneAttribute

using System;

namespace WindowsPhone.MVVM.Tombstone
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
  public class TombstoneAttribute : Attribute
  {
  }
}
