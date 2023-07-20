// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.DevelopmentHelpers
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System;

namespace Coding4Fun.Toolkit.Controls
{
  public static class DevelopmentHelpers
  {
    [Obsolete("Moved to Coding4Fun.Toolkit.Controls.Common.ApplicationSpace")]
    public static bool IsDesignMode => ApplicationSpace.IsDesignMode;

    [Obsolete("Moved to Coding4Fun.Toolkit.dll now, Namespace is System")]
    public static bool IsTypeOf(this object target, Type type) => TypeExtensions.IsTypeOf(target, type);

    [Obsolete("Moved to Coding4Fun.Toolkit.dll now, Namespace is System")]
    public static bool IsTypeOf(this object target, object referenceObject) => TypeExtensions.IsTypeOf(target, referenceObject);
  }
}
