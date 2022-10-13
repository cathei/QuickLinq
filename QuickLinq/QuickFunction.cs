// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public interface IFunction<in TIn, out TOut>
    {
        TOut Invoke(TIn parameter);
    }

    public readonly struct QuickFunction<TIn, TOut> : IFunction<TIn, TOut>
    {
        private readonly Func<TIn, TOut> selector;

        public QuickFunction(Func<TIn, TOut> selector)
        {
            this.selector = selector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Invoke(TIn parameter) => selector(parameter);
    }
}