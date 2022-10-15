// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;

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
            using var enumerator = GetEnumerator();
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
            using var enumerator = GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current : default;
        }

        /// <summary>
        /// Finds a element in the enumerable. Throws exception if the element is not the only element.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Single()
        {
            using var enumerator = GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();

            T singleValue = enumerator.Current;
            if (enumerator.MoveNext())
                throw new InvalidOperationException();

            return singleValue;
        }
    }
}