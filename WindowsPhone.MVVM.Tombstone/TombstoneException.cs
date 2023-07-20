// WindowsPhone.MVVM.Tombstone.TombstoneException

using System;

namespace WindowsPhone.MVVM.Tombstone
{
  internal class TombstoneException : Exception
  {
    public TombstoneException(string message)
      : base(message)
    {
    }
  }
}
