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
    public struct Map<T, TKey, TSelector, TComparer> : IOrderByComparer<T>
        where TSelector : struct, IQuickFunction<T, TKey>
        where TComparer : IComparer<TKey>
    {
        private TSelector selector;
        private TComparer comparer;
        private PooledList<TKey> keys;

        internal Map(in TSelector selector, in TComparer comparer) : this()
        {
            this.selector = selector;
            this.comparer = comparer;
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
            return comparer.Compare(keys[x], keys[y]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            keys.Dispose();
        }
    }
}