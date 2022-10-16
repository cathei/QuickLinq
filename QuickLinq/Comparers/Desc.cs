// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Struct comparer, descending order.
    /// </summary>
    public struct Desc<T, TComparer> : IQuickFunction<T, T, int>
        where TComparer : struct, IQuickFunction<T, T, int>
    {
        private TComparer comparer;

        internal Desc(in TComparer comparer)
        {
            this.comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Invoke(T x, T y)
        {
            return -comparer.Invoke(x, y);
        }
    }
}