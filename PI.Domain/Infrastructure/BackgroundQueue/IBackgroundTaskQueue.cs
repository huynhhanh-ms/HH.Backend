namespace PI.Domain.BackgroundQueue
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundTaskAsync(Func<CancellationToken, ValueTask> workItem);

        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
    }
}
