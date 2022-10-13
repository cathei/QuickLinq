// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq.Where
{
    public readonly struct WhereSource<T, TPredicate, TSource, TEnumerator>
        where TPredicate : IFunction<T, bool>
        where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<T>
    {
        internal readonly QuickEnumerable<T, TSource, TEnumerator> enumerable;
        internal readonly TPredicate predicate;

        internal WhereSource(in QuickEnumerable<T, TSource, TEnumerator> enumerable, in TPredicate predicate)
        {
            this.enumerable = enumerable;
            this.predicate = predicate;
        }
    }
}