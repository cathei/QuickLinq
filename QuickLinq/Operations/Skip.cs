// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Skip<T, TOperation> : IQuickOperation<T, Skip<T, TOperation>>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private readonly int skip;
        private int index;

        internal Skip(in TOperation source, int skip)
        {
            this.source = source;
            this.skip = skip;
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Skip<T, TOperation> GetEnumerator() => new(source.GetEnumerator(), skip);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => IsCollection ? Get(index) : source.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            ++index;

            if (IsCollection)
                return index < Count;

            while (index < skip)
            {
                if (!source.MoveNext())
                    return false;

                ++index;
            }

            return source.MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => source.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();

        public bool IsCollection => source.IsCollection;

        public int Count => source.Count - skip;

        public T Get(int i) => source.Get(skip + i);
    }
}
