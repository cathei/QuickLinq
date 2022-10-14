// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public interface IQuickStack<T>
    {
        T Value { get; set; }
    }

    public struct QuickStack<T> : IQuickStack<T>
    {
        public T Value { get; set; }
    }

    public struct QuickStack<T1, T2> : IQuickStack<T2>
    {
        public QuickStack<T1> Inner { get; set; }
        public T2 Value { get; set; }
    }

    public struct QuickStack<T1, T2, T3> : IQuickStack<T3>
    {
        public QuickStack<T1, T2> Inner { get; set; }
        public T3 Value { get; set; }
    }
}