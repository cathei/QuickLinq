// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cathei.QuickLinq
{
    /// <summary>
    /// IQuickEnumerable is both IEnumerable and IEnumerator
    /// </summary>
    public interface IQuickEnumerator<out T, out TSelf> : IEnumerable<T>, IEnumerator<T>
        where TSelf : IEnumerable<T>, IEnumerator<T>
    {
        new TSelf GetEnumerator();

        // interface default implementation
        object? IEnumerator.Current => Current;

        // interface default implementation
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        // interface default implementation
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
