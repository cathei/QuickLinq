// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Struct projection comparer.
    /// </summary>
    public struct Map<TIn, TOut, TSelector, TComparer> : IQuickFunction<TIn, TIn, int>
        where TSelector : struct, IQuickFunction<TIn, TOut>
        where TComparer : struct, IQuickFunction<TOut, TOut, int>
    {
        private TSelector selector;
        private TComparer comparer;

        internal Map(in TSelector selector, in TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Invoke(TIn x, TIn y)
        {
            return comparer.Invoke(selector.Invoke(x), selector.Invoke(y));
        }
    }
}