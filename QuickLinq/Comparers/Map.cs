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
    /// Struct projection comparer.
    /// </summary>
    public struct Map<T, TKey> : IOrderByComparer<T>
    {
        private readonly Func<T, TKey> selector;
        private readonly IComparer<TKey> comparer;
        private readonly bool descending;
        private PooledList<TKey> keys;

        internal Map(Func<T, TKey> selector, IComparer<TKey>? comparer, bool descending) : this()
        {
            this.selector = selector;
            this.comparer = comparer ?? Comparer<TKey>.Default;
            this.descending = descending;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(PooledList<T> elements)
        {
            keys = new PooledList<TKey>(elements.Count);

            for (int i = 0; i < elements.Count; ++i)
                keys.Add(selector(elements[i]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(int x, int y)
        {
            return descending ?
                comparer.Compare(keys[y], keys[x]) :
                comparer.Compare(keys[x], keys[y]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            keys.Dispose();
        }
    }

    /// <summary>
    /// Struct projection comparer.
    /// </summary>
    public struct Map<T, TKey, TSelector, TComparer> : IOrderByComparer<T>
        where TSelector : struct, IQuickFunction<T, TKey>
        where TComparer : struct, IQuickFunction<TKey, TKey, int>
    {
        private TSelector selector;
        private TComparer comparer;
        private readonly bool descending;
        private PooledList<TKey> keys;

        internal Map(in TSelector selector, in TComparer comparer, bool descending) : this()
        {
            this.selector = selector;
            this.comparer = comparer;
            this.descending = descending;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(PooledList<T> elements)
        {
            keys = new PooledList<TKey>(elements.Count);

            for (int i = 0; i < elements.Count; ++i)
                keys.Add(selector.Invoke(elements[i]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(int x, int y)
        {
            return descending ?
                comparer.Invoke(keys[y], keys[x]) :
                comparer.Invoke(keys[x], keys[y]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            keys.Dispose();
        }
    }
}