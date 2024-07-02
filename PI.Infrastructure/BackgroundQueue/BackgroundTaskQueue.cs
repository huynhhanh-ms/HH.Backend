using PI.Domain.BackgroundQueue;
using PI.Domain.Dto.Medicine;
using System.Threading.Channels;

namespace PI.Infrastructure.BackgroundQueue
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, ValueTask>> _queue;

        public BackgroundTaskQueue()
        {
            _queue = Channel.CreateUnbounded<Func<CancellationToken, ValueTask>>();
        }

        public async ValueTask QueueBackgroundTaskAsync(Func<CancellationToken, ValueTask> workItem)
        {
            ArgumentNullException.ThrowIfNull(workItem);

            await _queue.Writer.WriteAsync(workItem);
        }

        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
        {
            if (_queue.Reader.CanPeek)
            {
                var task = await _queue.Reader.ReadAsync(cancellationToken);
                return task;
            }

            return null;
        }
    }
}
