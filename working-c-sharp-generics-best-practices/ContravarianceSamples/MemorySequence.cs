using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContravarianceSamples
{
    public class MemorySequence<T> : ISequence<T>
        //: ISequenceWriter<T>, ISequenceReader<T>
    {
        private Queue<T> _queue = new Queue<T>();

        public void Add(T item)
        {
            _queue.Enqueue(item);
        }

        public T GetNextItem()
        {
            if (_queue.Count == 0) return default(T);
            return _queue.Dequeue();
        }
    }
}
