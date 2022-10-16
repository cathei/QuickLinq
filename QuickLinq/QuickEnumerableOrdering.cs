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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, T, Wrap<T>, TOperation>> OrderBy(IComparer<T>? comparer = null)
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, T, Wrap<T, TComparer>, TOperation>> OrderBy<TComparer>(TComparer comparer)
            where TComparer : struct, IQuickFunction<T, T, int>
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, TKey, Map<T, TKey, Call<T, TKey>>, TOperation>> OrderBy<TKey>(
                Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return new(new(source, new(new(keySelector), comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, TKey, Map<T, TKey, TSelector, TComparer>, TOperation>> OrderBy<TKey, TSelector, TComparer>(
                TSelector keySelector, TComparer comparer)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
        {
            return new(new(source, new(keySelector, comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, T, WrapDesc<T>, TOperation>> OrderByDescending(IComparer<T>? comparer = null)
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, T, WrapDesc<T, TComparer>, TOperation>> OrderByDescending<TComparer>(TComparer comparer)
            where TComparer : struct, IQuickFunction<T, T, int>
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, TKey, MapDesc<T, TKey, Call<T, TKey>>, TOperation>> OrderByDescending<TKey>(
                Func<T, TKey> keySelector,  IComparer<TKey>? comparer = null)
        {
            return new(new(source, new(new(keySelector), comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, TKey, MapDesc<T, TKey, TSelector, TComparer>, TOperation>> OrderByDescending<TKey, TSelector, TComparer>(
                TSelector keySelector, TComparer comparer)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
        {
            return new(new(source, new(keySelector, comparer)));
        }
    }

    public static partial class QuickEnumerableExtensions
    {
        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, T), Then<T, TFirstKey, TFirst, T, Wrap<T>>, TOperation>> ThenBy<T, TFirstKey, TFirst, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, IComparer<T>? comparer = null)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, T), Then<T, TFirstKey, TFirst, T, Wrap<T, TComparer>>, TOperation>> ThenBy<T, TFirstKey, TFirst, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, TComparer comparer)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TComparer : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, TSecondKey), Then<T, TFirstKey, TFirst, TSecondKey, Map<T, TSecondKey, Call<T, TSecondKey>>>, TOperation>> ThenBy<T, TFirstKey, TFirst, TSecondKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, Func<T, TSecondKey> keySelector, IComparer<TSecondKey>? comparer = null)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(new(keySelector), comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, TSecondKey), Then<T, TFirstKey, TFirst, TSecondKey, Map<T, TSecondKey, TSelector, TComparer>>, TOperation>> ThenBy<T, TFirstKey, TFirst, TSecondKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, TSelector keySelector, TComparer comparer)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TSelector : struct, IQuickFunction<T, TSecondKey>
            where TComparer : struct, IQuickFunction<TSecondKey, TSecondKey, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, T), Then<T, TFirstKey, TFirst, T, WrapDesc<T>>, TOperation>> ThenByDescending<T, TFirstKey, TFirst, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, IComparer<T>? comparer = null)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, T), Then<T, TFirstKey, TFirst, T, WrapDesc<T, TComparer>>, TOperation>> ThenByDescending<T, TFirstKey, TFirst, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, TComparer comparer)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TComparer : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, TSecondKey), Then<T, TFirstKey, TFirst, TSecondKey, MapDesc<T, TSecondKey, Call<T, TSecondKey>>>, TOperation>> ThenByDescending<T, TFirstKey, TFirst, TSecondKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, Func<T, TSecondKey> keySelector, IComparer<TSecondKey>? comparer = null)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(new(keySelector), comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, (TFirstKey, TSecondKey), Then<T, TFirstKey, TFirst, TSecondKey, MapDesc<T, TSecondKey, TSelector, TComparer>>, TOperation>> ThenByDescending<T, TFirstKey, TFirst, TSecondKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirstKey, TFirst, TOperation>> source, TSelector keySelector, TComparer comparer)
            where TFirst : struct, IQuickComparer<T, TFirstKey>
            where TSelector : struct, IQuickFunction<T, TSecondKey>
            where TComparer : struct, IQuickFunction<TSecondKey, TSecondKey, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer))));
        }
    }
}