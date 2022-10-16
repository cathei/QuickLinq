// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Struct comparer combining two comparer.
    /// </summary>
    public struct Then<T, TFirst, TSecond> : IQuickComparer<T>
        where TFirst : struct, IQuickComparer<T>
        where TSecond : struct, IQuickComparer<T>
    {
        private TFirst first;
        private TSecond second;

        internal Then(in TFirst first, in TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(in PooledList<T> elements)
        {
            first.Initialize(elements);
            second.Initialize(elements);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(int x, int y)
        {
            int result = first.Compare(x, y);
            return result != 0 ? result : second.Compare(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            first.Dispose();
            second.Dispose();
        }
    }
}