// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        /// <summary>
        /// Finds first element in the enumerable. Throws exception if enumerable is empty.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T First()
        {
            using var enumerator = source.CanSlice ?
                source.GetSliceEnumerator(0, 1) : GetEnumerator();

            // there are no elements
            if (enumerator.MoveNext())
                return enumerator.Current;

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Finds first element in the enumerable. Return default value if enumerable is empty.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? FirstOrDefault()
        {
            using var enumerator = source.CanSlice ?
                source.GetSliceEnumerator(0, 1) : GetEnumerator();

            return enumerator.MoveNext() ? enumerator.Current : default;
        }

        /// <summary>
        /// Finds a element in the enumerable. Throws exception if the element is not the only element.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Single()
        {
            using var enumerator = source.CanSlice ?
                source.GetSliceEnumerator(0, 2) : GetEnumerator();

            // there are no elements
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();

            T singleValue = enumerator.Current;

            // there are multiple elements
            if (enumerator.MoveNext())
                throw new InvalidOperationException();

            return singleValue;
        }
    }
}