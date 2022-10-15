// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Where<T, TOperation, TFunc> : IQuickOperation<T, Where<T, TOperation, TFunc>>
        where TOperation : struct, IQuickOperation<T, TOperation>
        where TFunc : struct, IQuickFunction<T, bool>
    {
        private TOperation source;
        private TFunc predicate;

        internal Where(in TOperation source, in TFunc predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Where<T, TOperation, TFunc> GetEnumerator() => new(source, predicate);

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

                if (predicate.Invoke(source.Current))
                    return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => source.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => source.Dispose();
    }
}