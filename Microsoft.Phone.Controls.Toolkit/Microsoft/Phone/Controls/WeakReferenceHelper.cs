// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.WeakReferenceHelper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A helper class that provides useful utilities to
  /// work with weak references.
  /// </summary>
  public static class WeakReferenceHelper
  {
    /// <summary>
    /// Determine if there is a reference to a known target in an object
    /// that implements IEnumerable.
    /// </summary>
    /// <param name="references">The object that implements IEnumerable.</param>
    /// <param name="target">The known target.</param>
    /// <returns>
    /// True if a reference to the known target exists in the list. False otherwise.
    /// </returns>
    public static bool ContainsTarget(IEnumerable<WeakReference> references, object target)
    {
      if (references == null)
        return false;
      foreach (WeakReference reference in references)
      {
        if (target == reference.Target)
          return true;
      }
      return false;
    }

    /// <summary>
    /// Remove a reference to a known target from an object
    /// that implement IList.
    /// </summary>
    /// <param name="references">The list to be examined.</param>
    /// <param name="target">The known target.</param>
    /// <returns>Try if the target was successsfully removed.</returns>
    public static bool TryRemoveTarget(IList<WeakReference> references, object target)
    {
      if (references == null)
        return false;
      for (int index = 0; index < references.Count; ++index)
      {
        if (references[index].Target == target)
        {
          references.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Removes all the weak references with null targets from an object
    /// that implements IList.
    /// </summary>
    /// <param name="references">The object that implements IList.</param>
    public static void RemoveNullTargetReferences(IList<WeakReference> references)
    {
      if (references == null)
        return;
      for (int index = 0; index < references.Count; ++index)
      {
        if (references[index].Target == null)
        {
          references.RemoveAt(index);
          --index;
        }
      }
    }
  }
}
