// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Range
{
    public struct RangeEnumerator : IQuickOperation<RangeSource, RangeEnumerator>, IQuickEnumerator<int>
    {
        private readonly RangeSource range;
        private int value;

        private RangeEnumerator(in RangeSource range)
        {
            this.range = range;
            value = range.start - 1;
        }

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RangeEnumerator Create(in RangeSource source) => new(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++value < range.end;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => value = range.start - 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
    }
}