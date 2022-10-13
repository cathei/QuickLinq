// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Select;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T,
                WhereSource<T, TPredicate, TSource, TIteration>,
                WhereIteration<T, TPredicate, TSource, TIteration>>
            Where<T, TPredicate, TSource, TIteration>(
                this QuickEnumerable<T, TSource, TIteration> source, TPredicate predicate)
            where TPredicate : IFunction<T, bool>
            where TSource : struct
            where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<T>
        {
            return new(new(source, predicate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T,
                WhereSource<T, QuickFunction<T, bool>, TSource, TIteration>,
                WhereIteration<T, QuickFunction<T, bool>, TSource, TIteration>>
            Select<T, TSource, TIteration>(
                this QuickEnumerable<T, TSource, TIteration> source, Func<T, bool> predicate)
            where TSource : struct
            where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<T>
        {
            return new(new(source, new(predicate)));
        }
    }
}