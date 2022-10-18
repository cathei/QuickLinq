// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Comparers;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        #region OrderBy

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Wrap<T>, TOperation>> OrderBy(IComparer<T>? comparer = null)
        {
            return new(new(source, new(comparer, false)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Wrap<T, TComparer>, TOperation>> OrderBy<TComparer>(TComparer comparer)
            where TComparer : struct, IQuickFunction<T, T, int>
        {
            return new(new(source, new(comparer, false)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Wrap<T>, TOperation>> OrderByDescending(IComparer<T>? comparer = null)
        {
            return new(new(source, new(comparer, true)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Wrap<T, TComparer>, TOperation>> OrderByDescending<TComparer>(TComparer comparer)
            where TComparer : struct, IQuickFunction<T, T, int>
        {
            return new(new(source, new(comparer, true)));
        }

        #endregion

        #region OrderByKey

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey>, TOperation>> OrderBy<TKey>(
                Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return new(new(source, new(keySelector, comparer, false)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, TSelector, TComparer>, TOperation>> OrderBy<TKey, TSelector, TComparer>(
                in TSelector keySelector, in TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
        {
            return new(new(source, new(keySelector, comparer, false)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey>, TOperation>> OrderByDescending<TKey>(
                Func<T, TKey> keySelector,  IComparer<TKey>? comparer = null)
        {
            return new(new(source, new(keySelector, comparer, true)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, TSelector, TComparer>, TOperation>> OrderByDescending<TKey, TSelector, TComparer>(
                in TSelector keySelector, in TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
        {
            return new(new(source, new(keySelector, comparer, true)));
        }

        #endregion
    }

    public static partial class QuickEnumerableExtensions
    {
        #region OrderBy + ThenBy

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Wrap<T>>, TOperation>> ThenBy<T, TFirst, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, IComparer<T>? comparer = null)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer, false))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Wrap<T, TComparer>>, TOperation>> ThenBy<T, TFirst, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, TComparer comparer)
            where TFirst : struct, IOrderByComparer<T>
            where TComparer : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer, false))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Wrap<T>>, TOperation>> ThenByDescending<T, TFirst, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, IComparer<T>? comparer = null)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer, true))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Wrap<T, TComparer>>, TOperation>> ThenByDescending<T, TFirst, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, TComparer comparer)
            where TFirst : struct, IOrderByComparer<T>
            where TComparer : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer, true))));
        }

        #endregion

        #region OrderBy + ThenByKey

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey>>, TOperation>> ThenBy<T, TFirst, TKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer, false))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey, TSelector, TComparer>>, TOperation>> ThenBy<T, TFirst, TKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                TSelector keySelector, TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TFirst : struct, IOrderByComparer<T>
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer, false))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey>>, TOperation>> ThenByDescending<T, TFirst, TKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer, true))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey, TSelector, TComparer>>, TOperation>> ThenByDescending<T, TFirst, TKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, TSelector keySelector, TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TFirst : struct, IOrderByComparer<T>
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer, true))));
        }

        #endregion
    }
}