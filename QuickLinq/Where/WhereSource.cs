// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq.Select
{
    public readonly struct WhereSource<T, TPredicate, TSource, TIteration>
        where TPredicate : IFunction<T, bool>
        where TSource : struct
        where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<T>
    {
        internal readonly QuickEnumerable<T, TSource, TIteration> enumerable;
        internal readonly TPredicate predicate;

        internal WhereSource(QuickEnumerable<T, TSource, TIteration> enumerable, TPredicate predicate)
        {
            this.enumerable = enumerable;
            this.predicate = predicate;
        }
    }
}