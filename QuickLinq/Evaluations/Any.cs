// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        /// <summary>
        /// Checks if enumerable has any element.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any()
        {
            if (source.CanCount)
                return source.MaxCount > 0;

            using var enumerator = source.CanSlice ?
                source.GetSliceEnumerator(0, 1) : GetEnumerator();

            return enumerator.MoveNext();
        }

        /// <summary>
        /// Checks if any element in enumerable satisfies given condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any<TFunc>(TFunc predicate) where TFunc : IQuickFunction<T, bool>
        {
            using var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                if (predicate.Invoke(enumerator.Current))
                    return true;
            return false;
        }

        /// <summary>
        /// Checks if any element in enumerable satisfies given condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any(Func<T, bool> predicate)
        {
            using var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                if (predicate(enumerator.Current))
                    return true;
            return false;
        }
    }
}
