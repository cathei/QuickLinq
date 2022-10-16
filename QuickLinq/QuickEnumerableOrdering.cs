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
        public QuickEnumerable<T, OrderBy<T, TComparer, TOperation>> OrderBy<TComparer>(TComparer comparer)
            where TComparer : struct, IQuickFunction<T, T, int>
        {
            return new(new(source, comparer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Wrap<T>, TOperation>> OrderBy(IComparer<T> comparer)
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, TSelector, TComparer>, TOperation>> OrderBy<TKey, TSelector, TComparer>(
                TSelector keySelector, TComparer comparer)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
        {
            return new(new(source, new(keySelector, comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, Call<T, TKey>, Wrap<TKey>>, TOperation>> OrderBy<TKey>(
                Func<T, TKey> keySelector,  IComparer<TKey> comparer)
        {
            return new(new(source, new(new(keySelector), new(comparer))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Desc<T, TComparer>, TOperation>> OrderByDescending<TComparer>(TComparer comparer)
            where TComparer : struct, IQuickFunction<T, T, int>
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, WrapDesc<T>, TOperation>> OrderByDescending(IComparer<T> comparer)
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, MapDesc<T, TKey, TSelector, TComparer>, TOperation>> OrderByDescending<TKey, TSelector, TComparer>(
                TSelector keySelector, TComparer comparer)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
        {
            return new(new(source, new(keySelector, comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, MapDesc<T, TKey, Call<T, TKey>, Wrap<TKey>>, TOperation>> OrderByDescending<TKey>(
                Func<T, TKey> keySelector,  IComparer<TKey> comparer)
        {
            return new(new(source, new(new(keySelector), new(comparer))));
        }
    }

    public static partial class QuickEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, TComparer>, TOperation>> ThenBy<T, TPrev, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, TComparer comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TComparer : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, comparer)));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, Wrap<T>>, TOperation>> ThenBy<T, TPrev, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, IComparer<T> comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, Map<T, TKey, TSelector, TComparer>>, TOperation>> ThenBy<T, TPrev, TKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, TSelector keySelector, TComparer comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, Map<T, TKey, Call<T, TKey>, Wrap<TKey>>>, TOperation>> ThenBy<T, TPrev, TKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, Func<T, TKey> keySelector, IComparer<TKey> comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source,
                new(source.source.comparer, new(new(keySelector), new(comparer)))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, Desc<T, TComparer>>, TOperation>> ThenByDescending<T, TPrev, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, TComparer comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TComparer : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, WrapDesc<T>>, TOperation>> ThenByDescending<T, TPrev, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, IComparer<T> comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, MapDesc<T, TKey, TSelector, TComparer>>, TOperation>> ThenByDescending<T, TPrev, TKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, TSelector keySelector, TComparer comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : struct, IQuickFunction<TKey, TKey, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TPrev, MapDesc<T, TKey, Call<T, TKey>, Wrap<TKey>>>, TOperation>> ThenByDescending<T, TPrev, TKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TPrev, TOperation>> source, Func<T, TKey> keySelector, IComparer<TKey> comparer)
            where TPrev : struct, IQuickFunction<T, T, int>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source,
                new(source.source.comparer, new(new(keySelector), new(comparer)))));
        }
    }
}