// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Take<T, TOperation> : IQuickOperation<T, Take<T, TOperation>>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private int count;

        internal Take(in TOperation source, int count)
        {
            this.source = source;
            this.count = count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Take<T, TOperation> GetEnumerator()
        {
            return CanSlice ? GetSliceEnumerator(0, count) : new(source.GetEnumerator(), count);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => source.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (--count < 0)
                return false;

            return source.MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();

        public bool CanCount => source.CanCount;

        public int MaxCount => Math.Min(count, source.MaxCount);

        public bool CanSlice => source.CanSlice;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Take<T, TOperation> GetSliceEnumerator(int skip, int take)
        {
            // trying to boost performance by using source's slicing feature
            // count parameter is meaningless in this case
            return new(source.GetSliceEnumerator(skip, Math.Min(count - skip, take)), take);
        }
    }
}
