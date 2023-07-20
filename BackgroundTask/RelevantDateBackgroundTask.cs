// Decompiled with JetBrains decompiler
// Type: BackgroundTask.RelevantDateBackgroundTask
// Assembly: BackgroundTask, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 10FA53AB-FB11-4171-ABA7-A60EF7EFFF23
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\BackgroundTask.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace BackgroundTask
{
  [MarshalingBehavior]
  [Threading]
  [Version(16842752)]
  [CompilerGenerated]
  [Activatable(16842752)]
  public sealed class RelevantDateBackgroundTask : IBackgroundTask, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern RelevantDateBackgroundTask();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void Run([In] IBackgroundTaskInstance taskInstance);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
