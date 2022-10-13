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
                WhereSource<T, TPredicate, TSource, TEnumerator>,
                WhereEnumerator<T, TPredicate, TSource, TEnumerator>>
            Where<T, TPredicate, TSource, TEnumerator>(
                this QuickEnumerable<T, TSource, TEnumerator> source, TPredicate predicate)
            where TPredicate : IFunction<T, bool>
            where TSource : struct
            where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<T>
        {
            return new(new(source, predicate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T,
                WhereSource<T, QuickFunction<T, bool>, TSource, TEnumerator>,
                WhereEnumerator<T, QuickFunction<T, bool>, TSource, TEnumerator>>
            Select<T, TSource, TEnumerator>(
                this QuickEnumerable<T, TSource, TEnumerator> source, Func<T, bool> predicate)
            where TSource : struct
            where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<T>
        {
            return new(new(source, new(predicate)));
        }
    }
}