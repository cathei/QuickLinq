// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public readonly struct Call<T, TOut> : IQuickFunction<T, TOut>
    {
        private readonly Func<T, TOut> invoke;

        public Call(Func<T, TOut> invoke)
        {
            this.invoke = invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Invoke(T arg) => invoke(arg);
    }

    public readonly struct Call<T1, T2, TOut> : IQuickFunction<T1, T2, TOut>
    {
        private readonly Func<T1, T2, TOut> invoke;

        public Call(Func<T1, T2, TOut> invoke)
        {
            this.invoke = invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TOut Invoke(T1 arg1, T2 arg2) => invoke(arg1, arg2);
    }
}