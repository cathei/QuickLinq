// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cathei.QuickLinq
{
    public interface IQuickEnumerator<out T> : IEnumerator<T>
    {
        object? IEnumerator.Current => Current;
    }

    public interface IQuickOperation<TSource, out TEnumerator>
    {
        TEnumerator Create(in TSource source);
    }
}
