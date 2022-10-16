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
    public struct MapDesc<T, TKey> : IQuickComparer<T, TKey>
    {
        private Func<T, TKey> selector;
        private IComparer<TKey> comparer;

        internal MapDesc(Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            this.selector = selector;
            this.comparer = comparer ?? Comparer<TKey>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TKey SelectKey(in T element) => selector(element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in TKey x, in TKey y) => -comparer.Compare(x, y);
    }

    /// <summary>
    /// Struct projection comparer.
    /// </summary>
    public struct MapDesc<T, TKey, TSelector, TComparer> : IQuickComparer<T, TKey>
        where TSelector : struct, IQuickFunction<T, TKey>
        where TComparer : struct, IQuickFunction<TKey, TKey, int>
    {
        private TSelector selector;
        private TComparer comparer;

        internal MapDesc(in TSelector selector, in TComparer comparer)
        {
            this.selector = selector;
            this.comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TKey SelectKey(in T element) => selector.Invoke(element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in TKey x, in TKey y) => -comparer.Invoke(x, y);
    }
}