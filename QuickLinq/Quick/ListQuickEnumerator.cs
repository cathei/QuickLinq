// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public struct ListQuickEnumerator<T> :
            IQuickOperation<List<T>, ListQuickEnumerator<T>>, IQuickEnumerator<T>
    {
        private List<T>.Enumerator enumerator;

        private ListQuickEnumerator(in List<T>.Enumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ListQuickEnumerator<T> Create(in List<T> source)
        {
            return new(source.GetEnumerator());
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => enumerator.MoveNext();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => enumerator.ResetWithoutBoxing();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => enumerator.Dispose();
    }
}