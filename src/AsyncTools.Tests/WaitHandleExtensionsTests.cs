using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTools.Tests;

[TestFixture]
public sealed class WaitHandleExtensionsTests
{
	[Test]
	public void Should_CancelTask_When_CancellationTokenSourceIsCancelled()
	{
		// Arrange
		using var handler = new ManualResetEvent(initialState: false);
		using var cts = new CancellationTokenSource();

		var task = handler.WaitAsync(cancellationToken: cts.Token);

		// Act
		cts.Cancel();

		//  Assert
		Assert.That(task.IsCanceled, Is.True);
	}

	[Test]
	public void Should_ThrowTaskCancelledException_When_TimeoutMsExceeded()
	{
		// Arrange
		using var handler = new ManualResetEvent(initialState: false);

		// Act & Assert
		Assert.ThrowsAsync<TaskCanceledException>(async () => await handler.WaitAsync(timeoutMs: 5));
	}

	[Test]
	public void Should_ThrowTaskCancelledException_When_TimeoutExceeded()
	{
		// Arrange
		using var handler = new ManualResetEvent(initialState: false);

		// Act & Assert
		Assert.ThrowsAsync<TaskCanceledException>(async () => await handler.WaitAsync(timeout: TimeSpan.FromMilliseconds(5)));
	}
}