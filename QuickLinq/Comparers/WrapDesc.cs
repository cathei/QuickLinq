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
    /// Struct comparer, wrapper for IComparer, descending order.
    /// </summary>
    public struct WrapDesc<T> : IOrderByComparer<T>
    {
        private readonly IComparer<T> comparer;
        private PooledList<T> keys;

        internal WrapDesc(IComparer<T>? comparer) : this()
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(in PooledList<T> elements)
        {
            keys = elements;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(int x, int y)
        {
            return comparer.Compare(keys[y], keys[x]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            // do nothing
        }
    }

    /// <summary>
    /// Struct comparer, wrapper for struct selector, descending order
    /// </summary>
    public struct WrapDesc<T, TComparer> : IOrderByComparer<T>
        where TComparer : struct, IQuickFunction<T, T, int>
    {
        private TComparer comparer;
        private PooledList<T> keys;

        internal WrapDesc(in TComparer comparer) : this()
        {
            this.comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(in PooledList<T> elements)
        {
            keys = elements;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(int x, int y)
        {
            return comparer.Invoke(keys[y], keys[x]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            // do nothing
        }
    }
}