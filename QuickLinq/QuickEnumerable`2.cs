// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TEnumerator> : IEnumerable<T>
        where TEnumerator : struct, IQuickEnumerator<T, TEnumerator>
    {
        private TEnumerator source;

        public QuickEnumerable(in TEnumerator source)
        {
            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TEnumerator GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, Select<T, TOut, TEnumerator>> Select<TOut>(in QuickFunction<T, TOut> selector)
        {
            return new(new(source, selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, Select<T, TOut, TEnumerator>> Select<TOut>(Func<T, TOut> selector)
        {
            return new(new(source, new(selector)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TEnumerator>> Where(QuickFunction<T, bool> predicate)
        {
            return new(new(source, predicate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TEnumerator>> Where(Func<T, bool> predicate)
        {
            return new(new(source, new(predicate)));
        }
    }
}