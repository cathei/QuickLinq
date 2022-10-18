// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        /// <summary>
        /// Finds last element in the enumerable. Throws exception if enumerable is empty.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Last()
        {
            if (source.CanCount)
            {
                int count = source.MaxCount;

                if (count == 0)
                    throw new InvalidOperationException();

                using var slice = source.GetSliceEnumerator(count - 1, 1);

                if (!slice.MoveNext())
                    throw new InvalidOperationException("Collection was modified");

                return slice.Current;
            }

            using var enumerator = GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();

            T lastValue = enumerator.Current;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }

        /// <summary>
        /// Finds last element in the enumerable. Return default value if enumerable is empty.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? LastOrDefault()
        {
            if (source.CanCount)
            {
                int count = source.MaxCount;

                if (count == 0)
                    return default;

                using var slice = source.GetSliceEnumerator(count - 1, 1);

                if (!slice.MoveNext())
                    throw new InvalidOperationException("Collection was modified");

                return slice.Current;
            }

            using var enumerator = GetEnumerator();

            T? lastValue = default;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }
    }
}