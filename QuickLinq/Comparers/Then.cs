// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Struct comparer combining two comparer.
    /// </summary>
    public struct Then<TIn, TFirst, TSecond> : IQuickFunction<TIn, TIn, int>
        where TFirst : struct, IQuickFunction<TIn, TIn, int>
        where TSecond : struct, IQuickFunction<TIn, TIn, int>
    {
        private TFirst first;
        private TSecond second;

        internal Then(in TFirst first, in TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Invoke(TIn x, TIn y)
        {
            int result = first.Invoke(x, y);
            return result != 0 ? result : second.Invoke(x, y);
        }
    }
}