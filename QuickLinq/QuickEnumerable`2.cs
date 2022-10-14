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
    }
}