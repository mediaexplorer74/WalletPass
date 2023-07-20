// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.HubTileService
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;

namespace Microsoft.Phone.Controls
{
  /// <summary>Provides organized animations for the hub tiles.</summary>
  /// <QualityBand>Preview</QualityBand>
  public static class HubTileService
  {
    /// <summary>Number of steps in the pipeline</summary>
    private const int WaitingPipelineSteps = 3;
    /// <summary>
    /// Number of hub tile that can be animated at exactly the same time.
    /// </summary>
    private const int NumberOfSimultaneousAnimations = 1;
    /// <summary>Track resurrection for weak references.</summary>
    private const bool TrackResurrection = false;
    /// <summary>Timer to trigger animations in timely.</summary>
    private static DispatcherTimer Timer = new DispatcherTimer();
    /// <summary>
    /// Random number generator to take certain random decisions.
    /// e.g. which hub tile is to be animated next.
    /// </summary>
    private static Random ProbabilisticBehaviorSelector = new Random();
    /// <summary>
    /// Pool that contains references to the hub tiles that are not frozen.
    /// i.e. hub tiles that can be animated at the moment.
    /// </summary>
    private static List<WeakReference> EnabledImagesPool = new List<WeakReference>();
    /// <summary>
    /// Pool that contains references to the hub tiles which are frozen.
    /// i.e. hub tiles that cannot be animated at the moment.
    /// </summary>
    private static List<WeakReference> FrozenImagesPool = new List<WeakReference>();
    /// <summary>
    /// Pipeline that contains references to the hub tiles that where animated previously.
    /// These are stalled briefly before they can be animated again.
    /// </summary>
    private static List<WeakReference> StalledImagesPipeline = new List<WeakReference>();

    /// <summary>Static constructor to add the tick event handler.</summary>
    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Attaching event handlers cannot be done inline.")]
    static HubTileService() => HubTileService.Timer.Tick += new EventHandler(HubTileService.OnTimerTick);

    /// <summary>Restart the timer to trigger animations.</summary>
    private static void RestartTimer()
    {
      if (HubTileService.Timer.IsEnabled)
        return;
      HubTileService.Timer.Interval = TimeSpan.FromMilliseconds(2500.0);
      HubTileService.Timer.Start();
    }

    /// <summary>Add a reference to a newly instantiated hub tile.</summary>
    /// <param name="tile">The newly instantiated hub tile.</param>
    internal static void InitializeReference(HubTile tile)
    {
      WeakReference tile1 = new WeakReference((object) tile, false);
      if (tile.IsFrozen)
        HubTileService.AddReferenceToFrozenPool(tile1);
      else
        HubTileService.AddReferenceToEnabledPool(tile1);
      HubTileService.RestartTimer();
    }

    /// <summary>
    /// Remove all references of a hub tile before finalizing it.
    /// </summary>
    /// <param name="tile">The hub tile that is to be finalized.</param>
    internal static void FinalizeReference(HubTile tile)
    {
      WeakReference tile1 = new WeakReference((object) tile, false);
      HubTileService.RemoveReferenceFromEnabledPool(tile1);
      HubTileService.RemoveReferenceFromFrozenPool(tile1);
      HubTileService.RemoveReferenceFromStalledPipeline(tile1);
    }

    /// <summary>
    /// Add a reference of a hub tile to the enabled images pool.
    /// </summary>
    /// <param name="tile">The hub tile to be added.</param>
    private static void AddReferenceToEnabledPool(WeakReference tile)
    {
      if (HubTileService.ContainsTarget(HubTileService.EnabledImagesPool, tile.Target))
        return;
      HubTileService.EnabledImagesPool.Add(tile);
    }

    /// <summary>
    /// Add a reference of a hub tile to the frozen images pool.
    /// </summary>
    /// <param name="tile">The hub tile to be added.</param>
    private static void AddReferenceToFrozenPool(WeakReference tile)
    {
      if (HubTileService.ContainsTarget(HubTileService.FrozenImagesPool, tile.Target))
        return;
      HubTileService.FrozenImagesPool.Add(tile);
    }

    /// <summary>
    /// Add a reference of a hub tile to the stalled images pipeline.
    /// </summary>
    /// <param name="tile">The hub tile to be added.</param>
    private static void AddReferenceToStalledPipeline(WeakReference tile)
    {
      if (HubTileService.ContainsTarget(HubTileService.StalledImagesPipeline, tile.Target))
        return;
      HubTileService.StalledImagesPipeline.Add(tile);
    }

    /// <summary>
    /// Remove the reference of a hub tile from the enabled images pool.
    /// </summary>
    /// <param name="tile">The hub tile to be removed.</param>
    private static void RemoveReferenceFromEnabledPool(WeakReference tile) => HubTileService.RemoveTarget(HubTileService.EnabledImagesPool, tile.Target);

    /// <summary>
    /// Remove the reference of a hub tile from the frozen images pool.
    /// </summary>
    /// <param name="tile">The hub tile to be removed.</param>
    private static void RemoveReferenceFromFrozenPool(WeakReference tile) => HubTileService.RemoveTarget(HubTileService.FrozenImagesPool, tile.Target);

    /// <summary>
    /// Remove the reference of a hub tile from the stalled images pipeline.
    /// </summary>
    /// <param name="tile">The hub tile to be removed.</param>
    private static void RemoveReferenceFromStalledPipeline(WeakReference tile) => HubTileService.RemoveTarget(HubTileService.StalledImagesPipeline, tile.Target);

    /// <summary>
    /// Determine if there is a reference to a known target in a list.
    /// </summary>
    /// <param name="list">The list to be examined.</param>
    /// <param name="target">The known target.</param>
    /// <returns>True if a reference to the known target exists in the list. False otherwise.</returns>
    private static bool ContainsTarget(List<WeakReference> list, object target)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].Target == target)
          return true;
      }
      return false;
    }

    /// <summary>Remove a reference to a known target in a list.</summary>
    /// <param name="list">The list to be examined.</param>
    /// <param name="target">The known target.</param>
    private static void RemoveTarget(List<WeakReference> list, object target)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].Target == target)
        {
          list.RemoveAt(index);
          break;
        }
      }
    }

    /// <summary>
    /// Executes the code to process a visual transition:
    /// 1. Stop the timer.
    /// 2. Advances the stalled tiles to the next step in the pipeline.
    /// If there is at least one tile that can be currently animated ...
    /// 3. Animate as many tiles as indicated.
    /// 4. Select a tile andomly from the pool of enabled tiles.
    /// 5. Based on this tile's current visual state, move it onto
    /// the next one.
    /// 6. Set the stalling counter for the recently animated image.
    /// 7. Take it out from the pool and into the pipeline to prevent it
    /// from being animated continuously.
    /// 8. Restart the timer with a randomly generated time interval
    /// between 100 and 3000 ms.
    /// Notice that if there are no hub tiles that can be animated,
    /// the timer is not restarted.
    /// </summary>
    /// <param name="sender">The static timer.</param>
    /// <param name="e">The event information.</param>
    private static void OnTimerTick(object sender, EventArgs e)
    {
      HubTileService.Timer.Stop();
      for (int index = 0; index < HubTileService.StalledImagesPipeline.Count; ++index)
      {
        if ((HubTileService.StalledImagesPipeline[index].Target as HubTile)._stallingCounter-- == 0)
        {
          HubTileService.AddReferenceToEnabledPool(HubTileService.StalledImagesPipeline[index]);
          HubTileService.RemoveReferenceFromStalledPipeline(HubTileService.StalledImagesPipeline[index]);
          --index;
        }
      }
      if (HubTileService.EnabledImagesPool.Count > 0)
      {
        for (int index1 = 0; index1 < 1; ++index1)
        {
          int index2 = HubTileService.ProbabilisticBehaviorSelector.Next(HubTileService.EnabledImagesPool.Count);
          switch ((HubTileService.EnabledImagesPool[index2].Target as HubTile).State)
          {
            case ImageState.Expanded:
              if (((HubTileService.EnabledImagesPool[index2].Target as HubTile)._canDrop || (HubTileService.EnabledImagesPool[index2].Target as HubTile)._canFlip) && (HubTileService.EnabledImagesPool[index2].Target as HubTile).Size != TileSize.Small)
              {
                if (!(HubTileService.EnabledImagesPool[index2].Target as HubTile)._canDrop && (HubTileService.EnabledImagesPool[index2].Target as HubTile)._canFlip)
                {
                  (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Flipped;
                  break;
                }
                if (!(HubTileService.EnabledImagesPool[index2].Target as HubTile)._canFlip && (HubTileService.EnabledImagesPool[index2].Target as HubTile)._canDrop)
                {
                  (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Semiexpanded;
                  break;
                }
                if (HubTileService.ProbabilisticBehaviorSelector.Next(2) == 0)
                {
                  (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Semiexpanded;
                  break;
                }
                (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Flipped;
                break;
              }
              break;
            case ImageState.Semiexpanded:
              (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Collapsed;
              break;
            case ImageState.Collapsed:
              (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Expanded;
              break;
            case ImageState.Flipped:
              (HubTileService.EnabledImagesPool[index2].Target as HubTile).State = ImageState.Expanded;
              break;
          }
          (HubTileService.EnabledImagesPool[index2].Target as HubTile)._stallingCounter = 3;
          HubTileService.AddReferenceToStalledPipeline(HubTileService.EnabledImagesPool[index2]);
          HubTileService.RemoveReferenceFromEnabledPool(HubTileService.EnabledImagesPool[index2]);
        }
      }
      else if (HubTileService.StalledImagesPipeline.Count == 0)
        return;
      HubTileService.Timer.Interval = TimeSpan.FromMilliseconds((double) (HubTileService.ProbabilisticBehaviorSelector.Next(1, 31) * 100));
      HubTileService.Timer.Start();
    }

    /// <summary>Freeze a hub tile.</summary>
    /// <param name="tile">The hub tile to be frozen.</param>
    public static void FreezeHubTile(HubTile tile)
    {
      WeakReference tile1 = new WeakReference((object) tile, false);
      HubTileService.AddReferenceToFrozenPool(tile1);
      HubTileService.RemoveReferenceFromEnabledPool(tile1);
      HubTileService.RemoveReferenceFromStalledPipeline(tile1);
    }

    /// <summary>
    /// Unfreezes a hub tile and restarts the timer if needed.
    /// </summary>
    /// <param name="tile">The hub tile to be unfrozen.</param>
    public static void UnfreezeHubTile(HubTile tile)
    {
      WeakReference tile1 = new WeakReference((object) tile, false);
      HubTileService.AddReferenceToEnabledPool(tile1);
      HubTileService.RemoveReferenceFromFrozenPool(tile1);
      HubTileService.RemoveReferenceFromStalledPipeline(tile1);
      HubTileService.RestartTimer();
    }

    /// <summary>
    /// Freezes all the hub tiles with the specified group tag that are not already frozen.
    /// </summary>
    /// <param name="group">The group tag representing the hub tiles that should be frozen.</param>
    public static void FreezeGroup(string group)
    {
      for (int index = 0; index < HubTileService.EnabledImagesPool.Count; ++index)
      {
        if ((HubTileService.EnabledImagesPool[index].Target as HubTile).GroupTag == group)
        {
          (HubTileService.EnabledImagesPool[index].Target as HubTile).IsFrozen = true;
          --index;
        }
      }
      for (int index = 0; index < HubTileService.StalledImagesPipeline.Count; ++index)
      {
        if ((HubTileService.StalledImagesPipeline[index].Target as HubTile).GroupTag == group)
        {
          (HubTileService.StalledImagesPipeline[index].Target as HubTile).IsFrozen = true;
          --index;
        }
      }
    }

    /// <summary>
    /// Unfreezes all the hub tiles with the specified group tag
    /// that are currently frozen and restarts the timer if needed.
    /// </summary>
    /// <param name="group">The group tag representing the hub tiles that should be unfrozen.</param>
    public static void UnfreezeGroup(string group)
    {
      for (int index = 0; index < HubTileService.FrozenImagesPool.Count; ++index)
      {
        if ((HubTileService.FrozenImagesPool[index].Target as HubTile).GroupTag == group)
        {
          (HubTileService.FrozenImagesPool[index].Target as HubTile).IsFrozen = false;
          --index;
        }
      }
      HubTileService.RestartTimer();
    }
  }
}
