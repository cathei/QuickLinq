// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Select
{
    public struct Select<TIn, TOut, TEnumerator> : IQuickEnumerator<TOut, Select<TIn, TOut, TEnumerator>>
        where TEnumerator : struct, IQuickEnumerator<TIn, TEnumerator>
    {
        private TEnumerator source;
        private QuickFunction<TIn, TOut> selector;

        internal Select(in TEnumerator source, in QuickFunction<TIn, TOut> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Select<TIn, TOut, TEnumerator> GetEnumerator() => new(source.GetEnumerator(), selector);

        public TOut Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => selector.Invoke(source.Current);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => source.MoveNext();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => source.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();
    }
}