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
    public readonly struct WrapDesc<T> : IQuickComparer<T, T>
    {
        private readonly IComparer<T> comparer;

        internal WrapDesc(IComparer<T>? comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public bool IsElementKey => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SelectKey(in T element) => element;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in T x, in T y) => -comparer.Compare(x, y);
    }

    /// <summary>
    /// Struct comparer, wrapper for struct selector
    /// </summary>
    public struct WrapDesc<T, TComparer> : IQuickComparer<T, T>
        where TComparer : struct, IQuickFunction<T, T, int>
    {
        private TComparer comparer;

        internal WrapDesc(in TComparer comparer)
        {
            this.comparer = comparer;
        }

        public bool IsElementKey => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SelectKey(in T element) => element;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in T x, in T y) => -comparer.Invoke(x, y);
    }
}