// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Quick;

namespace Cathei.QuickLinq
{
    public static class QuickEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, IList<T>, ListEnumerator<T>> Quick<T>(this IList<T> source)
        {
            return new(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, IEnumerable<T>, QuickEnumerator<T>> Quick<T>(this IEnumerable<T> source)
        {
            return new(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ResetWithoutBoxing<T>(this T enumerator) where T : IEnumerator
        {
            enumerator.Reset();
        }
    }
}