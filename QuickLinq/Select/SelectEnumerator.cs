// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Select
{
    public struct SelectEnumerator<TIn, TOut, TSelector, TSource, TEnumerator> :
            IQuickOperation<
                SelectSource<TIn, TOut, TSelector, TSource, TEnumerator>,
                SelectEnumerator<TIn, TOut, TSelector, TSource, TEnumerator>>,
            IQuickEnumerator<TOut>
        where TSelector : IFunction<TIn, TOut>
        where TSource : struct
        where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<TIn>
    {
        private readonly TSelector selector;
        private TEnumerator enumerator;

        private SelectEnumerator(in SelectSource<TIn, TOut, TSelector, TSource, TEnumerator> source)
        {
            selector = source.selector;
            enumerator = source.enumerable.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SelectEnumerator<TIn, TOut, TSelector, TSource, TEnumerator> Create(
            in SelectSource<TIn, TOut, TSelector, TSource, TEnumerator> source)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => enumerator.Dispose();
    }
}