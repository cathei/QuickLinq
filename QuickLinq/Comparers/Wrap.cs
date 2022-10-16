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
    /// Struct comparer, wrapper for IComparer.
    /// </summary>
    public struct Wrap<T> : IQuickComparer<T>
    {
        private readonly IComparer<T> comparer;
        private PooledList<T> keys;

        internal Wrap(IComparer<T>? comparer) : this()
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
            return comparer.Compare(keys[x], keys[y]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            // do nothing
        }
    }

    /// <summary>
    /// Struct comparer, wrapper for struct selector
    /// </summary>
    public struct Wrap<T, TComparer> : IQuickComparer<T>
        where TComparer : struct, IQuickFunction<T, T, int>
    {
        private TComparer comparer;
        private PooledList<T> keys;

        internal Wrap(in TComparer comparer) : this()
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
            return comparer.Invoke(keys[x], keys[y]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            // do nothing
        }
    }
}