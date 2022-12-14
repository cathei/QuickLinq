// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Repeat<T> : IQuickOperation<T, Repeat<T>>
    {
        internal readonly T element;
        internal readonly int count;
        private int index;

        internal Repeat(in T element, int count)
        {
            this.element = element;
            this.count = count;

            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Repeat<T> GetEnumerator() => new(element, count);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => element;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++index < count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }

        public bool CanCount => true;

        public int MaxCount => count;

        public bool CanSlice => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Repeat<T> GetSliceEnumerator(int skip, int take) => new(element, Math.Min(count - skip, take));
    }
}