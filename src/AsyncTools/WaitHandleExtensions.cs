using System.Threading.Tasks;
using System.Threading;
using System;

namespace AsyncTools;

public static class WaitHandleExtensions
{
	public static Task WaitAsync(this WaitHandle handle, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(handle);

		var tcs = new TaskCompletionSource();
		var ctr = cancellationToken.CanBeCanceled
			? cancellationToken.Register(tcs.SetCanceled)
			: default;

		var rwh = ThreadPool.RegisterWaitForSingleObject(
			waitObject: handle,
			callBack: (stete, timeout) =>
			{
				if (stete is TaskCompletionSource completionSource)
				{
					if (completionSource.Task.IsCompleted)
						return;

					if (timeout)
						completionSource.SetCanceled();
					else
						completionSource.SetResult();
				}
				else
					throw new InvalidOperationException();

			},
			state: tcs,
			millisecondsTimeOutInterval: Timeout.Infinite,
			executeOnlyOnce: true
		);

		tcs.Task.ContinueWith(result =>
		{
			rwh.Unregister(null);
			return ctr.Unregister();
		});

		return tcs.Task;
	}
}