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
            if (source.IsCollection)
            {
                if (source.Count == 0)
                    throw new InvalidOperationException();
                return source.Get(source.Count - 1);
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
            if (source.IsCollection)
            {
                if (source.Count == 0)
                    return default;
                return source.Get(source.Count - 1);
            }

            using var enumerator = GetEnumerator();

            T? lastValue = default;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }
    }
}