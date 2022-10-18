// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Quick
{
    public struct QuickenList<T> : IQuickOperation<T, QuickenList<T>>
    {
        private readonly IList<T> list;
        private readonly int max;

        private int index;

        // enumerable constructor
        internal QuickenList(IList<T> list) : this()
        {
            this.list = list;
        }

        // enumerator constructor
        private QuickenList(IList<T> list, int start, int max)
        {
            this.list = list;
            this.max = max;

            index = start - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickenList<T> GetEnumerator() => new(list, 0, list.Count);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => list[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++index < max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }

        public bool CanCount => true;

        public int MaxCount => list.Count;

        public bool CanSlice => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickenList<T> GetSliceEnumerator(int skip, int take) =>
            new(list, skip, Math.Min(list.Count, skip + take));
    }
}