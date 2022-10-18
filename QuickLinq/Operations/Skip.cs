// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Skip<T, TOperation> : IQuickOperation<T, Skip<T, TOperation>>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private readonly int count;

        // enumerable constructor
        internal Skip(in TOperation source, int count)
        {
            this.source = source;
            this.count = count;
        }

        // enumerator constructor
        private Skip(in TOperation source) : this()
        {
            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Skip<T, TOperation> GetEnumerator()
        {
            if (CanSlice)
                return GetSliceEnumerator(0, int.MaxValue);

            var enumerator = source.GetEnumerator();

            // move starting point
            for (int i = 0; i < count; ++i)
            {
                if (!source.MoveNext())
                    break;
            }

            return new(enumerator);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => source.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => source.MoveNext();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();

        public bool CanCount => source.CanCount;

        public int MaxCount => source.MaxCount - count;

        public bool CanSlice => source.CanSlice;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Skip<T, TOperation> GetSliceEnumerator(int skip, int take)
        {
            return new(source.GetSliceEnumerator(skip + count, take));
        }
    }
}
