// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct WhereAt<T, TOperation, TFunc> : IQuickOperation<T, WhereAt<T, TOperation, TFunc>>
        where TOperation : struct, IQuickOperation<T, TOperation>
        where TFunc : struct, IQuickFunction<T, int, bool>
    {
        private TOperation source;
        private TFunc predicate;
        private int index;

        internal WhereAt(in TOperation source, in TFunc predicate)
        {
            this.source = source;
            this.predicate = predicate;
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WhereAt<T, TOperation, TFunc> GetEnumerator() => new(source, predicate);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => source.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (true)
            {
                if (!source.MoveNext())
                    return false;

                if (predicate.Invoke(source.Current, ++index))
                    return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            source.Reset();
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();

        public bool IsCollection => false;
    }
}