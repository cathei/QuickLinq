// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public struct QuickEnumerator<T, TSource, TIteration> // : IEnumerator<T>
        where TSource : struct
        where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<T>
    {
        private TIteration iteration;

        public QuickEnumerator(in TSource source)
        {
            iteration = default(TIteration).Create(source);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => iteration.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => iteration.MoveNext();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => iteration.Reset();

        // object IEnumerator.Current => Current;

        // I heard implementing Dispose will prevent inlining of foreach?
        // public void Dispose()
        // {
        //
        // }
    }
}