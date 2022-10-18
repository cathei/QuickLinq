// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public struct Concat<T, TFirst, TSecond> : IQuickOperation<T, Concat<T, TFirst, TSecond>>
        where TFirst : struct, IQuickOperation<T, TFirst>
        where TSecond : struct, IQuickOperation<T, TSecond>
    {
        private TFirst first;
        private TSecond second;

        private bool firstDone;
        private int takeCount;

        // enumerable constructor
        internal Concat(in TFirst first, in TSecond second) : this()
        {
            this.first = first;
            this.second = second;
        }

        // enumerator constructor
        private Concat(in TFirst first, in TSecond second, bool firstDone, int takeCount)
        {
            this.first = first;
            this.second = second;
            this.firstDone = firstDone;
            this.takeCount = takeCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Concat<T, TFirst, TSecond> GetEnumerator()
        {
            // second will deferred enumerated
            return new(first.GetEnumerator(), second, false, int.MaxValue);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => firstDone ? second.Current : first.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (--takeCount < 0)
                return false;

            if (!firstDone)
            {
                if (first.MoveNext())
                    return true;

                firstDone = true;

                // deferred execution of second enumerable
                // compensate takeCount (already decreased) if we can slice second one
                second = second.CanSlice ? second.GetSliceEnumerator(0, takeCount + 1) : second.GetEnumerator();
            }

            return second.MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            first.Dispose();
            second.Dispose();
        }

        public bool CanCount => first.CanCount && second.CanCount;

        public int MaxCount => first.MaxCount + second.MaxCount;

        public bool CanSlice => first.CanSlice && second.CanSlice;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Concat<T, TFirst, TSecond> GetSliceEnumerator(int skip, int take)
        {
            if (first.CanCount)
            {
                int firstCount = first.MaxCount;

                // first will not be enumerated
                if (skip >= firstCount)
                    return new(default, second.GetSliceEnumerator(skip - firstCount, take), true, take);
            }

            // second will deferred initialized
            return new(first.GetSliceEnumerator(skip, take), second, false, take);
        }
    }
}
