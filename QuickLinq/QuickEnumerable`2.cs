// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq
{
    public struct QuickEnumerable<T, TOperation> : IEnumerable<T>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        internal TOperation source;

        public QuickEnumerable(in TOperation source)
        {
            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOperation GetEnumerator() => source.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T First()
        {
            using var enumerator = GetEnumerator();
            if (enumerator.MoveNext())
                return enumerator.Current;
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? FirstOrDefault()
        {
            using var enumerator = GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Last()
        {
            using var enumerator = GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();

            T lastValue = enumerator.Current;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? LastOrDefault()
        {
            using var enumerator = GetEnumerator();

            T? lastValue = default;
            while (enumerator.MoveNext())
                lastValue = enumerator.Current;

            return lastValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count()
        {
            using var enumerator = GetEnumerator();
            int count = 0;

            while (enumerator.MoveNext())
                count++;
            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, Select<T, TOut, TOperation, TFunc>> Select<TOut, TFunc>(in TFunc selector)
            where TFunc : struct, IQuickFunction<T, TOut>
        {
            return new(new(source, selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<TOut, Select<T, TOut, TOperation, Call<T, TOut>>> Select<TOut>(Func<T, TOut> selector)
        {
            return new(new(source, new(selector)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TOperation, TFunc>> Where<TFunc>(in TFunc predicate)
            where TFunc : struct, IQuickFunction<T, bool>
        {
            return new(new(source, predicate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TOperation, Call<T, bool>>> Where(Func<T, bool> predicate)
        {
            return new(new(source, new(predicate)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Concat<T, TOperation, TOther>> Concat<TOther>(in QuickEnumerable<T, TOther> other)
            where TOther : struct, IQuickOperation<T, TOther>
        {
            return new(new(source, other.source));
        }
    }
}