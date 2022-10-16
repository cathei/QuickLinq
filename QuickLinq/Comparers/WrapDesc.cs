// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Struct comparer, wrapper for IComparer, descending order.
    /// </summary>
    public readonly struct WrapDesc<T> : IQuickFunction<T, T, int>
    {
        private readonly IComparer<T> comparer;

        internal WrapDesc(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Invoke(T x, T y) => -comparer.Compare(x, y);
    }
}