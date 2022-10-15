// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>// : IEnumerable<T>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        internal TOperation source;

        public QuickEnumerable(in TOperation source)
        {
            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOperation GetEnumerator() => source.GetEnumerator();

        // IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        // IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Convert to IEnumerable interface, causes boxing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> AsEnumerable() => source;

        /// <summary>
        /// Map value with struct function selector.
        /// It has unused delegate parameter just for type inference.
        /// Usage: enumerable.Select(selector, x => x)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, Select<T, TOut, TOperation, TFunc>> Select<TOut, TFunc>(
                in TFunc selector, Func<TFunc, IQuickFunction<T, TOut>> _)
            where TFunc : struct, IQuickFunction<T, TOut>
        {
            return new(new(source, selector));
        }

        /// <summary>
        /// Map value with delegate selector.
        /// Usage: enumerable.Select(x => x + 1)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, Select<T, TOut, TOperation, Call<T, TOut>>> Select<TOut>(Func<T, TOut> selector)
        {
            return new(new(source, new(selector)));
        }

        /// <summary>
        /// Map value with struct function selector, with index.
        /// It has unused delegate parameter just for type inference.
        /// Usage: enumerable.Select(selector, x => x)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, SelectAt<T, TOut, TOperation, TFunc>> Select<TOut, TFunc>(
                in TFunc selector, Func<TFunc, IQuickFunction<T, int, TOut>> _)
            where TFunc : struct, IQuickFunction<T, int, TOut>
        {
            return new(new(source, selector));
        }

        /// <summary>
        /// Map value with delegate selector, with index.
        /// Usage: enumerable.Select((x, i) => x + i)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, SelectAt<T, TOut, TOperation, Call<T, int, TOut>>> Select<TOut>(Func<T, int, TOut> selector)
        {
            return new(new(source, new(selector)));
        }

        /// <summary>
        /// Filter value with struct function predicate.
        /// Usage: enumerable.Where(predicate)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TOperation, TFunc>> Where<TFunc>(in TFunc predicate)
            where TFunc : struct, IQuickFunction<T, bool>
        {
            return new(new(source, predicate));
        }

        /// <summary>
        /// Filter value with delegate predicate.
        /// Usage: enumerable.Where(x => x != null)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TOperation, Call<T, bool>>> Where(Func<T, bool> predicate)
        {
            return new(new(source, new(predicate)));
        }

        /// <summary>
        /// Filter value with struct function predicate, with index.
        /// It has unused byte parameter to prevent signature conflict.
        /// Usage: enumerable.Where(predicate)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public QuickEnumerable<T, WhereAt<T, TOperation, TFunc>> Where<TFunc>(in TFunc predicate, byte _ = default)
            where TFunc : struct, IQuickFunction<T, int, bool>
        {
            return new(new(source, predicate));
        }

        /// <summary>
        /// Filter value with delegate predicate, with index.
        /// Usage: enumerable.Where((x, i) => x + i == 0)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, WhereAt<T, TOperation, Call<T, int, bool>>> Where(Func<T, int, bool> predicate)
        {
            return new(new(source, new(predicate)));
        }

        /// <summary>
        /// Concatenate two enumerable.
        /// Usage: enumerable.Concat(other)
        /// </summary>
        /// <param name="other"></param>
        /// <typeparam name="TOther"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Concat<T, TOperation, TOther>> Concat<TOther>(in QuickEnumerable<T, TOther> other)
            where TOther : struct, IQuickOperation<T, TOther>
        {
            return new(new(source, other.source));
        }
    }
}