// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Quick
{
    public struct ListEnumerator<T> : IQuickOperation<IList<T>, ListEnumerator<T>>, IQuickEnumerator<T>
    {
        private readonly IList<T> list;
        private int index;

        private ListEnumerator(in IList<T> list)
        {
            this.list = list;
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ListEnumerator<T> Create(in IList<T> source)
        {
            return new(source);
        }

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
    }
}