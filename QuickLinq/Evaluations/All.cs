// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        /// <summary>
        /// Checks if all elements in enumerable satisfies given condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All<TFunc>(TFunc predicate) where TFunc : IQuickFunction<T, bool>
        {
            using var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                if (!predicate.Invoke(enumerator.Current))
                    return false;
            return true;
        }

        /// <summary>
        /// Checks if all elements in enumerable satisfies given condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All(Func<T, bool> predicate)
        {
            using var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                if (!predicate(enumerator.Current))
                    return false;
            return true;
        }
    }
}
