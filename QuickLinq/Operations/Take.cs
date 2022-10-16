// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Take<T, TOperation> : IQuickOperation<T, Take<T, TOperation>>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private readonly int take;
        private int index;

        internal Take(in TOperation source, int take)
        {
            this.source = source;
            this.take = take;
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Take<T, TOperation> GetEnumerator() => new(source.GetEnumerator(), take);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => IsCollection ? Get(index) : source.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (++index >= take)
                return false;

            if (IsCollection)
                return index < source.Count;

            return source.MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => source.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();

        public bool IsCollection => source.IsCollection;

        public int Count => Math.Max(take, source.Count);

        public T Get(int i) => source.Get(i);
    }
}
