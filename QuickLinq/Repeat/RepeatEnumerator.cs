// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Range
{
    public struct RepeatEnumerator<T> : IQuickOperation<RepeatSource<T>, RepeatEnumerator<T>>, IQuickEnumerator<T>
    {
        private readonly RepeatSource<T> repeat;
        private int index;

        private RepeatEnumerator(in RepeatSource<T> repeat)
        {
            this.repeat = repeat;
            index = -1;
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => repeat.element;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RepeatEnumerator<T> Create(in RepeatSource<T> source) => new(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++index < repeat.count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => index = -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
    }
}