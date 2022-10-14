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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Last()
        {
            using var enumerator = GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();

            T lastValue = enumerator.Current;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? LastOrDefault()
        {
            using var enumerator = GetEnumerator();

            T? lastValue = default;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }
    }
}