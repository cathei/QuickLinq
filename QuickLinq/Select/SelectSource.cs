// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq.Select
{
    public readonly struct SelectSource<TIn, TOut, TSelector, TSource, TIteration>
        where TSelector : IFunction<TIn, TOut>
        where TSource : struct
        where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<TIn>
    {
        internal readonly QuickEnumerable<TIn, TSource, TIteration> enumerable;
        internal readonly TSelector selector;

        internal SelectSource(QuickEnumerable<TIn, TSource, TIteration> enumerable, TSelector selector)
        {
            this.enumerable = enumerable;
            this.selector = selector;
        }
    }
}