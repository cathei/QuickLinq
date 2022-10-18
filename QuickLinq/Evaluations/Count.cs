// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        /// <summary>
        /// Counts the number of elements in enumerable.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count()
        {
            if (source.CanSlice)
                return source.SliceMax;

            using var enumerator = GetEnumerator();
            int count = 0;

            while (enumerator.MoveNext())
                count++;
            return count;
        }

        /// <summary>
        /// Counts the number of elements in enumerable satisfying given condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count<TFunc>(in TFunc predicate) where TFunc : IQuickFunction<T, bool>
        {
            using var enumerator = GetEnumerator();
            int count = 0;

            while (enumerator.MoveNext())
                if (predicate.Invoke(enumerator.Current))
                    count++;
            return count;
        }

        /// <summary>
        /// Counts the number of elements in enumerable satisfying given condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count(Func<T, bool> predicate)
        {
            using var enumerator = GetEnumerator();
            int count = 0;

            while (enumerator.MoveNext())
                if (predicate(enumerator.Current))
                    count++;
            return count;
        }
    }
}