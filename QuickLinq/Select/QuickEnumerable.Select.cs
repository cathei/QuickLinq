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
                SelectSource<TIn, TOut, TSelector, TSource, TIteration>,
                SelectIteration<TIn, TOut, TSelector, TSource, TIteration>>
            Select<TIn, TOut, TSelector, TSource, TIteration>(
                this QuickEnumerable<TIn, TSource, TIteration> source, TSelector selector)
            where TSelector : IFunction<TIn, TOut>
            where TSource : struct
            where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<TIn>
        {
            return new(new(source, selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<TOut,
                SelectSource<TIn, TOut, QuickFunction<TIn, TOut>, TSource, TIteration>,
                SelectIteration<TIn, TOut, QuickFunction<TIn, TOut>, TSource, TIteration>>
            Select<TIn, TOut, TSource, TIteration>(
                this QuickEnumerable<TIn, TSource, TIteration> source, Func<TIn, TOut> selector)
            where TSource : struct
            where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<TIn>
        {
            return new(new(source, new(selector)));
        }
    }
}