// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Where
{
    public struct Where<T, TEnumerator> : IQuickEnumerator<T, Where<T, TEnumerator>>
        where TEnumerator : struct, IQuickEnumerator<T, TEnumerator>
    {
        private TEnumerator source;
        private QuickFunction<T, bool> predicate;

        internal Where(in TEnumerator source, in QuickFunction<T, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Where<T, TEnumerator> GetEnumerator() => new(source, predicate);

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