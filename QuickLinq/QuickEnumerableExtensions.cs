// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;
using Cathei.QuickLinq.Quick;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerableExtensions
    {
        /// <summary>
        /// Wrap IList to QuickEnumerable.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, QuickenList<T>> Quick<T>(this IList<T> source)
        {
            return new(new(source));
        }

        /// <summary>
        /// Wrap IEnumerable to QuickEnumerable.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, Quicken<T>> Quick<T>(this IEnumerable<T> source)
        {
            return new(new(source));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ResetWithoutBoxing<T>(this T enumerator) where T : IEnumerator
        {
            enumerator.Reset();
        }
    }
}