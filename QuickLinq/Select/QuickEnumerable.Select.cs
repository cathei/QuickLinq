// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Select;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<TOut,
                SelectSource<TIn, TOut, TSelector, TSource, TEnumerator>,
                SelectEnumerator<TIn, TOut, TSelector, TSource, TEnumerator>>
            Select<TIn, TOut, TSelector, TSource, TEnumerator>(
                this QuickEnumerable<TIn, TSource, TEnumerator> source, TSelector selector)
            where TSelector : IFunction<TIn, TOut>
            where TSource : struct
            where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<TIn>
        {
            return new(new(source, selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<TOut,
                SelectSource<TIn, TOut, QuickFunction<TIn, TOut>, TSource, TEnumerator>,
                SelectEnumerator<TIn, TOut, QuickFunction<TIn, TOut>, TSource, TEnumerator>>
            Select<TIn, TOut, TSource, TEnumerator>(
                this QuickEnumerable<TIn, TSource, TEnumerator> source, Func<TIn, TOut> selector)
            where TSource : struct
            where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<TIn>
        {
            return new(new(source, new(selector)));
        }
    }
}