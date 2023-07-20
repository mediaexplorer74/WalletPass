// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Services.QueryExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Phone.Maps.Services
{
  /// <summary>Represents a class that extends map queries.</summary>
  public static class QueryExtensions
  {
    /// <summary>Get MapLocations as an asynchronous operation.</summary>
    /// <param name="geocodeQuery">The <see cref="T:Microsoft.Phone.Maps.Services.GeocodeQuery" /> the operation is performed on.</param>
    /// <returns>
    /// Returns <see cref="T:System.Threading.Tasks.Task`1" />.
    /// The task object representing the asynchronous operation.
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Want to use concrete type for extension method")]
    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
    public static Task<IList<MapLocation>> GetMapLocationsAsync(this GeocodeQuery geocodeQuery) => ((Query<IList<MapLocation>>) geocodeQuery).QueryAsync<IList<MapLocation>>();

    /// <summary>Get MapLocations as an asynchronous operation.</summary>
    /// <param name="reverseGeocodeQuery">The <see cref="T:Microsoft.Phone.Maps.Services.ReverseGeocodeQuery" /> the operation is performed on.</param>
    /// <returns>
    /// Returns <see cref="T:System.Threading.Tasks.Task`1" />.
    /// The task object representing the asynchronous operation.
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Want to use concrete type for extension method")]
    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
    public static Task<IList<MapLocation>> GetMapLocationsAsync(
      this ReverseGeocodeQuery reverseGeocodeQuery)
    {
      return ((Query<IList<MapLocation>>) reverseGeocodeQuery).QueryAsync<IList<MapLocation>>();
    }

    /// <summary>Get Route as an asynchronous operation.</summary>
    /// <param name="routeQuery">The <see cref="T:Microsoft.Phone.Maps.Services.RouteQuery" /> the operation is performed on.</param>
    /// <returns>
    /// Returns <see cref="T:System.Threading.Tasks.Task`1" />.
    /// The task object representing the asynchronous operation.
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Want to use concrete type for extension method")]
    public static Task<Route> GetRouteAsync(this RouteQuery routeQuery) => ((Query<Route>) routeQuery).QueryAsync<Route>();

    private static Task<TResult> QueryAsync<TResult>(this Query<TResult> query)
    {
      EventHandler<QueryCompletedEventArgs<TResult>> queryCompletedHandler = (EventHandler<QueryCompletedEventArgs<TResult>>) null;
      TaskCompletionSource<TResult> taskCompletionSource = QueryExtensions.CreateSource<TResult>((object) query);
      queryCompletedHandler = (EventHandler<QueryCompletedEventArgs<TResult>>) ((sender, e) => QueryExtensions.TransferCompletion<TResult>(taskCompletionSource, (AsyncCompletedEventArgs) e, (Func<TResult>) (() => e.Result), (Action) (() => query.QueryCompleted -= queryCompletedHandler)));
      query.QueryCompleted += queryCompletedHandler;
      try
      {
        query.QueryAsync();
      }
      catch
      {
        query.QueryCompleted -= queryCompletedHandler;
        throw;
      }
      return taskCompletionSource.Task;
    }

    /// <summary>Creates the TaskCompletionSource for the given state</summary>
    /// <typeparam name="TResult">Type of the returned result</typeparam>
    /// <param name="state">State to be provided to the TaskCompletionSource</param>
    /// <returns>A Task Completion Source</returns>
    private static TaskCompletionSource<TResult> CreateSource<TResult>(object state) => new TaskCompletionSource<TResult>(state, TaskCreationOptions.None);

    /// <summary>
    /// Transfer the execution of the completion of the async task
    /// </summary>
    /// <typeparam name="TResult">Type of the return type</typeparam>
    /// <param name="tcs">Task completion source used in the task</param>
    /// <param name="e">Event args from the async operation</param>
    /// <param name="getResult">Function that will be executed only if result is used</param>
    /// <param name="unregisterHandler">Action to be executed to unregister the Query.QueryCompleted event handler</param>
    private static void TransferCompletion<TResult>(
      TaskCompletionSource<TResult> tcs,
      AsyncCompletedEventArgs e,
      Func<TResult> getResult,
      Action unregisterHandler)
    {
      if (e.UserState != tcs.Task.AsyncState)
        return;
      if (unregisterHandler != null)
        unregisterHandler();
      if (e.Cancelled)
        tcs.TrySetCanceled();
      else if (e.Error != null)
        tcs.TrySetException(e.Error);
      else
        tcs.TrySetResult(getResult());
    }
  }
}
