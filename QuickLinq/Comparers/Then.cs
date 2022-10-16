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
    public struct Then<T, TFirstKey, TFirst, TSecondKey, TSecond> : IQuickComparer<T, (TFirstKey, TSecondKey)>
        where TFirst : struct, IQuickComparer<T, TFirstKey>
        where TSecond : struct, IQuickComparer<T, TSecondKey>
    {
        private TFirst first;
        private TSecond second;

        internal Then(in TFirst first, in TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsElementKey
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => first.IsElementKey && second.IsElementKey;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (TFirstKey, TSecondKey) SelectKey(in T element)
        {
            return (first.SelectKey(element), second.SelectKey(element));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in T x, in T y)
        {
            int result = first.Compare(x, y);
            return result != 0 ? result : second.Compare(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in (TFirstKey, TSecondKey) x, in (TFirstKey, TSecondKey) y)
        {
            int result = first.Compare(x.Item1, y.Item1);
            return result != 0 ? result : second.Compare(x.Item2, y.Item2);
        }
    }
}