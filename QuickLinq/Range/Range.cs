// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Data;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Range
{
    public struct Range : IQuickEnumerator<int, Range>
    {
        private readonly int start;
        private readonly int end;

        private int value;

        internal Range(int start, int end)
        {
            this.start = start;
            this.end = end;

            value = start - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range GetEnumerator() => new(start, end);

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++value < end;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => value = start - 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
    }
}