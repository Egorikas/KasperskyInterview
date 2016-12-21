using System.Collections.Generic;
using System.Threading;

namespace KasperskyInterview.Tasks
{
    public class CustomConcurrentQueue<T>
    {
        private readonly Queue<T> _queue;
        private readonly object _lock = new object();

        public CustomConcurrentQueue()
        {
            _queue = new Queue<T>();
        }

        public CustomConcurrentQueue(Queue<T> queue)
        {
            _queue = queue ?? new Queue<T>();
        }

        public void Push(T item)
        {
            lock (_lock)
            {
                _queue.Enqueue(item);
                Monitor.Pulse(_lock);
            }
        }

        public T Pop()
        {
            lock (_lock)
            {
                while (_queue.Count == 0)
                {
                    Monitor.Wait(_lock);
                }

                return _queue.Dequeue();
            }
        }
    }
}
