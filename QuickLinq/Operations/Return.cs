// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Return<T> : IQuickOperation<T, Return<T>>
    {
        private readonly T element;
        private bool done;

        // enumerable constructor
        internal Return(in T element) : this()
        {
            this.element = element;
        }

        // enumerator constructor
        private Return(in T element, bool done)
        {
            this.element = element;
            this.done = done;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Return<T> GetEnumerator() => new(element, done);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => element;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => !done && (done = true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }

        public bool CanCount => true;

        public int MaxCount => 1;

        public bool CanSlice => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Return<T> GetSliceEnumerator(int skip, int take) => new(element, skip > 0);
    }
}