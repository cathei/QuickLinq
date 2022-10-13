// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.QuickLinq
{
    public interface IQuickIteration<out T>
    {
        T Current { get; }
        bool MoveNext();
        void Reset();
    }

    public interface IQuickOperation<TSource, out TIteration>
    {
        TIteration Create(in TSource source);
    }
}