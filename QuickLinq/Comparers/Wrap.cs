// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Struct comparer, wrapper for IComparer.
    /// </summary>
    public readonly struct Wrap<T> : IQuickComparer<T, T>
    {
        private readonly IComparer<T> comparer;

        internal Wrap(IComparer<T>? comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SelectKey(in T element) => element;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in T x, in T y) => comparer.Compare(x, y);
    }

    /// <summary>
    /// Struct comparer, wrapper for struct selector
    /// </summary>
    public struct Wrap<T, TComparer> : IQuickComparer<T, T>
        where TComparer : struct, IQuickFunction<T, T, int>
    {
        private TComparer comparer;

        internal Wrap(in TComparer comparer)
        {
            this.comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SelectKey(in T element) => element;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(in T x, in T y) => comparer.Invoke(x, y);
    }
}