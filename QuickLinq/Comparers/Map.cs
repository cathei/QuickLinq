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
    public struct Map<T, TKey, TSelector> : IQuickComparer<T, TKey>
        where TSelector : struct, IQuickFunction<T, TKey>
    {
        private TSelector selector;
        private IComparer<TKey> comparer;

        internal Map(in TSelector selector, IComparer<TKey>? comparer)
        {
            this.selector = selector;
            this.comparer = comparer ?? Comparer<TKey>.Default;
        }

        public bool IsElementKey => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TKey SelectKey(in T element) => selector.Invoke(element);

        // this function should never be called
        public int Compare(in T x, in T y) => throw new NotImplementedException();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in TKey x, in TKey y) => comparer.Compare(x, y);
    }

    /// <summary>
    /// Struct projection comparer.
    /// </summary>
    public struct Map<T, TKey, TSelector, TComparer> : IQuickComparer<T, TKey>
        where TSelector : struct, IQuickFunction<T, TKey>
        where TComparer : struct, IQuickFunction<TKey, TKey, int>
    {
        private TSelector selector;
        private TComparer comparer;

        internal Map(in TSelector selector, in TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        public bool IsElementKey => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TKey SelectKey(in T element) => selector.Invoke(element);

        // this function should never be called
        public int Compare(in T x, in T y) => throw new NotImplementedException();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in TKey x, in TKey y) => comparer.Invoke(x, y);
    }
}