// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, List<T>, ListQuickEnumerator<T>> Quick<T>(this List<T> source)
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