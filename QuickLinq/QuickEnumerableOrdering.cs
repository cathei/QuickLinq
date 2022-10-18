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
        public QuickEnumerable<T, OrderBy<T, Wrap<T, Comparer<T>>, TOperation>> OrderBy() =>
            OrderBy(Comparer<T>.Default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Wrap<T, TComparer>, TOperation>> OrderBy<TComparer>(TComparer comparer)
            where TComparer : IComparer<T>
        {
            return new(new(source, new(comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, WrapDesc<T, Comparer<T>>, TOperation>> OrderByDescending()
            => OrderByDescending(Comparer<T>.Default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, WrapDesc<T, TComparer>, TOperation>> OrderByDescending<TComparer>(TComparer comparer)
            where TComparer : IComparer<T>
        {
            return new(new(source, new(comparer)));
        }

        #endregion

        #region OrderByKey

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, Call<T, TKey>, Comparer<TKey>>, TOperation>> OrderBy<TKey>(
            Func<T, TKey> keySelector)
        {
            return new(new(source, new(new(keySelector), Comparer<TKey>.Default)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, Call<T, TKey>, TComparer>, TOperation>> OrderBy<TKey, TComparer>(
                Func<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            return new(new(source, new(new(keySelector), comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, Map<T, TKey, TSelector, TComparer>, TOperation>> OrderBy<TKey, TSelector, TComparer>(
                in TSelector keySelector, in TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : IComparer<TKey>
        {
            return new(new(source, new(keySelector, comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, MapDesc<T, TKey, Call<T, TKey>, Comparer<TKey>>, TOperation>> OrderByDescending<TKey>(
                Func<T, TKey> keySelector)
        {
            return new(new(source, new(new(keySelector), Comparer<TKey>.Default)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, MapDesc<T, TKey, Call<T, TKey>, TComparer>, TOperation>> OrderByDescending<TKey, TComparer>(
                Func<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            return new(new(source, new(new(keySelector), comparer)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, OrderBy<T, MapDesc<T, TKey, TSelector, TComparer>, TOperation>> OrderByDescending<TKey, TSelector, TComparer>(
                in TSelector keySelector, in TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : IComparer<TKey>
        {
            return new(new(source, new(keySelector, comparer)));
        }

        #endregion
    }

    public static partial class QuickEnumerableExtensions
    {
        #region OrderBy + ThenBy

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Wrap<T, TComparer>>, TOperation>> ThenBy<T, TFirst, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, TComparer comparer)
            where TFirst : struct, IOrderByComparer<T>
            where TComparer : IComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, WrapDesc<T, TComparer>>, TOperation>> ThenByDescending<T, TFirst, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, TComparer comparer)
            where TFirst : struct, IOrderByComparer<T>
            where TComparer : IComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(comparer))));
        }

        #endregion

        #region OrderBy + ThenByKey

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey, Call<T, TKey>, Comparer<TKey>>>, TOperation>> ThenBy<T, TFirst, TKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                Func<T, TKey> keySelector)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(new(keySelector), Comparer<TKey>.Default))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey, Call<T, TKey>, TComparer>>, TOperation>> ThenBy<T, TFirst, TKey, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                Func<T, TKey> keySelector, TComparer comparer)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
            where TComparer : IComparer<TKey>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(new(keySelector), comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, Map<T, TKey, TSelector, TComparer>>, TOperation>> ThenBy<T, TFirst, TKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                TSelector keySelector, TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TFirst : struct, IOrderByComparer<T>
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : IComparer<TKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, MapDesc<T, TKey, Call<T, TKey>, Comparer<TKey>>>, TOperation>> ThenByDescending<T, TFirst, TKey, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                Func<T, TKey> keySelector)
            where TFirst : struct, IOrderByComparer<T>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(new(keySelector), Comparer<TKey>.Default))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, MapDesc<T, TKey, Call<T, TKey>, TComparer>>, TOperation>> ThenByDescending<T, TFirst, TKey, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source,
                Func<T, TKey> keySelector, TComparer comparer)
            where TFirst : struct, IOrderByComparer<T>
            where TComparer : IComparer<TKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(new(keySelector), comparer))));
        }

        public static QuickEnumerable<T, OrderBy<T, Then<T, TFirst, MapDesc<T, TKey, TSelector, TComparer>>, TOperation>> ThenByDescending<T, TFirst, TKey, TSelector, TComparer, TOperation>(
                this QuickEnumerable<T, OrderBy<T, TFirst, TOperation>> source, TSelector keySelector, TComparer comparer, Func<TSelector, IQuickFunction<T, TKey>> _)
            where TFirst : struct, IOrderByComparer<T>
            where TSelector : struct, IQuickFunction<T, TKey>
            where TComparer : IComparer<TKey>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            // replace OrderBy with new OrderBy
            return new(new(source.source.source, new(source.source.comparer, new(keySelector, comparer))));
        }

        #endregion
    }
}