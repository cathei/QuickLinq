// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq.Select
{
    public readonly struct SelectSource<TIn, TOut, TSelector, TSource, TEnumerator>
        where TSelector : IFunction<TIn, TOut>
        where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<TIn>
    {
        internal readonly QuickEnumerable<TIn, TSource, TEnumerator> enumerable;
        internal readonly TSelector selector;

        internal SelectSource(in QuickEnumerable<TIn, TSource, TEnumerator> enumerable, in TSelector selector)
        {
            this.enumerable = enumerable;
            this.selector = selector;
        }
    }
}