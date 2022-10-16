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

        internal Concat(in TFirst first, in TSecond second)
        {
            this.first = first;
            this.second = second;
            firstDone = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Concat<T, TFirst, TSecond> GetEnumerator() => new(first.GetEnumerator(), second.GetEnumerator());

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => firstDone ? second.Current : first.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (!firstDone)
            {
                if (first.MoveNext())
                    return true;
                firstDone = true;
            }

            return second.MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            first.Reset();
            second.Reset();
            firstDone = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            first.Dispose();
            second.Dispose();
        }

        public bool IsCollection => first.IsCollection && second.IsCollection;

        public int Count => first.Count + second.Count;

        public T Get(int index) => index < first.Count ? first.Get(index) : second.Get(index);
    }
}
