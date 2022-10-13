// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public readonly partial struct QuickEnumerable<T, TSource, TEnumerator> : IEnumerable<T>
        where TSource : struct
        where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<T>
    {
        private readonly TSource source;

        public QuickEnumerable(TSource source)
        {
            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TEnumerator GetEnumerator()
        {
            return default(TEnumerator).Create(source);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}