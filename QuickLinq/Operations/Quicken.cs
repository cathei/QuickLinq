// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Quick
{
    public readonly struct Quicken<T> : IQuickOperation<T, Quicken<T>>
    {
        private readonly IEnumerable<T>? enumerable;
        private readonly IEnumerator<T>? enumerator;

        internal Quicken(in IEnumerable<T> enumerable) : this()
        {
            this.enumerable = enumerable;
        }

        private Quicken(in IEnumerator<T> enumerator) : this()
        {
            this.enumerator = enumerator;
        }

        public Quicken<T> GetEnumerator() => new(enumerable!.GetEnumerator());

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator!.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => enumerator!.MoveNext();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => enumerator!.Dispose();

        public bool CanCount => false;

        public bool CanSlice => false;
    }
}