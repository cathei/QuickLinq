// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Return<T> : IQuickOperation<T, Return<T>>
    {
        internal readonly T element;
        private bool done;

        internal Return(in T element)
        {
            this.element = element;
            done = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Return<T> GetEnumerator() => new(element);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => element;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => !done && (done = true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => done = false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
    }
}