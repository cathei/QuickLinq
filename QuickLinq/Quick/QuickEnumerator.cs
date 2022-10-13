// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public struct QuickEnumerator<T> :
            IQuickOperation<IEnumerable<T>, QuickEnumerator<T>>, IQuickEnumerator<T>
    {
        private IEnumerator<T> enumerator;

        private QuickEnumerator(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerator<T> Create(in IEnumerable<T> source)
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
        public void Reset() => enumerator.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => enumerator.Dispose();
    }
}