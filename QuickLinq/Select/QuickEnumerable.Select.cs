// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Select;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TSource, TEnumerator>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut,
                SelectSource<T, TOut, TSelector, TSource, TEnumerator>,
                SelectEnumerator<T, TOut, TSelector, TSource, TEnumerator>>
            Select<TOut, TSelector>(in TSelector selector) where TSelector : IFunction<T, TOut>
        {
            return new(new(this, selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut,
                SelectSource<T, TOut, QuickFunction<T, TOut>, TSource, TEnumerator>,
                SelectEnumerator<T, TOut, QuickFunction<T, TOut>, TSource, TEnumerator>>
            Select<TOut>(Func<T, TOut> selector)
        {
            return new(new(this, new(selector)));
        }
    }
}