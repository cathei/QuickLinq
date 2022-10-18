// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct SelectAt<TIn, TOut, TOperation, TFunc> : IQuickOperation<TOut, SelectAt<TIn, TOut, TOperation, TFunc>>
        where TOperation : struct, IQuickOperation<TIn, TOperation>
        where TFunc : struct, IQuickFunction<TIn, int, TOut>
    {
        private TOperation source;
        private TFunc selector;
        private int index;

        internal SelectAt(in TOperation source, in TFunc selector)
        {
            this.source = source;
            this.selector = selector;

            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SelectAt<TIn, TOut, TOperation, TFunc> GetEnumerator() => new(source.GetEnumerator(), selector);

        public TOut Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => selector.Invoke(source.Current, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (!source.MoveNext())
                return false;

            ++index;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();

        public bool CanCount => source.CanCount;

        public int MaxCount => source.MaxCount;

        public bool CanSlice => source.CanSlice;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SelectAt<TIn, TOut, TOperation, TFunc> GetSliceEnumerator(int skip, int take)
            => new(source.GetSliceEnumerator(skip, take), selector);
    }
}
