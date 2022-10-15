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
        public bool All<TFunc>(in TFunc predicate) where TFunc : IQuickFunction<T, bool>
        {
            using var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                if (!predicate.Invoke(enumerator.Current))
                    return false;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All(Func<T, bool> predicate)
        {
            using var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                if (!predicate.Invoke(enumerator.Current))
                    return false;
            return true;
        }
    }
}
