// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Quick
{
    public struct QuickenList<T> : IQuickOperation<T, QuickenList<T>>
    {
        private readonly IList<T> list;
        private int index;

        internal QuickenList(in IList<T> list)
        {
            this.list = list;
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickenList<T> GetEnumerator() => new(list);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => list[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++index < list.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => index = -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }

        public bool IsCollection => true;

        public int Count => list.Count;

        public T Get(int i) => list[i];
    }
}