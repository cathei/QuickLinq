// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Select
{
    public struct SelectIteration<TIn, TOut, TSelector, TSource, TIteration> :
            IQuickOperation<
                SelectSource<TIn, TOut, TSelector, TSource, TIteration>,
                SelectIteration<TIn, TOut, TSelector, TSource, TIteration>>,
            IQuickIteration<TOut>
        where TSelector : IFunction<TIn, TOut>
        where TSource : struct
        where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<TIn>
    {
        private readonly TSelector selector;
        private QuickEnumerator<TIn, TSource, TIteration> enumerator;

        private SelectIteration(in SelectSource<TIn, TOut, TSelector, TSource, TIteration> source)
        {
            selector = source.selector;
            enumerator = source.enumerable.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SelectIteration<TIn, TOut, TSelector, TSource, TIteration> Create(
            in SelectSource<TIn, TOut, TSelector, TSource, TIteration> source)
        {
            return new(source);
        }

        public TOut Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => selector.Invoke(enumerator.Current);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => enumerator.MoveNext();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => enumerator.Reset();
    }
}