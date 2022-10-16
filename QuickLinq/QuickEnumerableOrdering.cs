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
        public QuickEnumerable<T, OrderBy<T, Desc<T, Wrap<T>>, TOperation>> OrderByDescending(IComparer<T> comparer)
        {
            return new(new(source, new(new(comparer))));
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
    }
}