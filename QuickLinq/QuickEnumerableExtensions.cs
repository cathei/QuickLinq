// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;
using Cathei.QuickLinq.Quick;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, QuickenList<T>> Quick<T>(this IList<T> source)
        {
            return new(new(source));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, Quicken<T>> Quick<T>(this IEnumerable<T> source)
        {
            return new(new(source));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ResetWithoutBoxing<T>(this T enumerator) where T : IEnumerator
        {
            enumerator.Reset();
        }

        // this has to be extension because signature is same
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<TOut, SelectAt<T, TOut, TOperation, TFunc>> Select<T, TOut, TOperation, TFunc>(
                in this QuickEnumerable<T, TOperation> enumerable, in TFunc predicate, Func<TFunc, IQuickFunction<T, TOut>> _)
            where TFunc : struct, IQuickFunction<T, int, TOut>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            return new(new(enumerable.source, predicate));
        }

        // this has to be extension because signature is same
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<TOut, SelectAt<T, TOut, TOperation, Call<T, int, TOut>>> Select<T, TOut, TOperation>(
                in this QuickEnumerable<T, TOperation> enumerable, Func<T, int, TOut> predicate)
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            return new(new(enumerable.source, new(predicate)));
        }

        // this has to be extension because signature is same
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, WhereAt<T, TOperation, TFunc>> Where<T, TOperation, TFunc>(
                in this QuickEnumerable<T, TOperation> enumerable, in TFunc predicate)
            where TFunc : struct, IQuickFunction<T, int, bool>
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            return new(new(enumerable.source, predicate));
        }

        // this has to be extension because signature is same
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, WhereAt<T, TOperation, Call<T, int, bool>>> Where<T, TOperation>(
                in this QuickEnumerable<T, TOperation> enumerable, Func<T, int, bool> predicate)
            where TOperation : struct, IQuickOperation<T, TOperation>
        {
            return new(new(enumerable.source, new(predicate)));
        }
    }
}