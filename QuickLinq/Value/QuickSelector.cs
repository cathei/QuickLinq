// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cathei.QuickLinq.ValueBox
{
    public interface IQuickSelector
    {
        TOut Select<TIn, TOut>(TIn value);
    }

    public readonly struct QuickSelectorDelegate : IQuickSelector
    {
        private readonly object selector;

        internal QuickSelectorDelegate(object selector)
        {
            this.selector = selector;
        }

        public TOut Select<TIn, TOut>(TIn value)
        {
            var func = Unsafe.As<Func<TIn, TOut>>(selector);
            return func(value);
        }
    }

    /// <summary>
    /// This is actual implementation of selector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QuickSelector<TFunc, TIn, TOut>
        where TFunc : struct, IQuickFunction<TIn, TOut>
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuickSelector<TFunc>
        where TFunc : struct
    {
        private TFunc selector;
    }


    // public readonly unsafe struct QuickSelector<TFunc> : IQuickSelector
    //     where TFunc : struct
    // {
    //     private TFunc func;
    //
    //     internal static void* selector;
    //
    //     public TOut Select<TIn, TOut>(TIn value)
    //     {
    //         delegate*<TFunc, TIn, TOut> sel = Unsafe.As<>()
    //
    //
    //
    //
    //
    //
    //
    //         throw new NotImplementedException();
    //     }
    // }
    //
    // internal static class QuickSelectorUtils<TFunc, TIn, TOut>
    //     // where TFunc : IQuickFunction<TIn, TOut>
    // {
    //     // public static
    //
    //
    //
    //     public static QuickSelector<TFunc> Create()
    //     {
    //
    //     }
    //
    // }
}

