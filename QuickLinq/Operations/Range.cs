// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Range : IQuickOperation<int, Range>
    {
        private readonly int start;
        private readonly int count;

        private int value;

        internal Range(int start, int count)
        {
            this.start = start;
            this.count = count;

            value = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range GetEnumerator() => new(start, count);

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => start + value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++value < count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }

        public bool CanCount => true;

        public int MaxCount => count;

        public bool CanSlice => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range GetSliceEnumerator(int skip, int take) => new(start + skip, Math.Min(count - skip, take));
    }
}