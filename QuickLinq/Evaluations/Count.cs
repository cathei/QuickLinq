﻿// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

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
        public int Count()
        {
            using var enumerator = GetEnumerator();
            int count = 0;

            while (enumerator.MoveNext())
                count++;
            return count;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count(Func<T, bool> predicate)
        {
            using var enumerator = GetEnumerator();
            int count = 0;

            while (enumerator.MoveNext())
                if (predicate.Invoke(enumerator.Current))
                    count++;
            return count;
        }
    }
}