// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public interface IQuickFunction<in T, out TOut>
    {
        TOut Invoke(T arg);
    }

    public interface IQuickFunction<in T1, in T2, out TOut>
    {
        TOut Invoke(T1 arg1, T2 arg2);
    }
}