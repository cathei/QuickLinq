// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Where;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TSource, TEnumerator>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T,
                WhereSource<T, TPredicate, TSource, TEnumerator>,
                WhereEnumerator<T, TPredicate, TSource, TEnumerator>>
            Where<TPredicate>(TPredicate predicate) where TPredicate : IFunction<T, bool>
        {
            return new(new(this, predicate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T,
                WhereSource<T, QuickFunction<T, bool>, TSource, TEnumerator>,
                WhereEnumerator<T, QuickFunction<T, bool>, TSource, TEnumerator>>
            Where(Func<T, bool> predicate)
        {
            return new(new(this, new(predicate)));
        }
    }
}