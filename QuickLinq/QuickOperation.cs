// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cathei.QuickLinq
{
    /// <summary>
    /// The base class for all operations.
    /// IQuickOperation is both IEnumerable and IEnumerator.
    /// This approach will simplify generic type information.
    /// </summary>
    public interface IQuickOperation<out T, out TSelf> : IEnumerable<T>, IEnumerator<T>
        where TSelf : struct, IQuickOperation<T, TSelf>
    {
        new TSelf GetEnumerator();

        // interface default implementation
        object? IEnumerator.Current => Current;

        // interface default implementation
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        // interface default implementation
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Collection optimization
        /// Count and Get is only valid when IsCollection is true
        /// </summary>
        bool IsCollection { get; }

        /// <summary>
        /// Collection optimization
        /// Only valid when IsCollection is true
        /// </summary>
        int Count => throw new NotSupportedException();

        /// <summary>
        /// Collection optimization
        /// Only valid when IsCollection is true
        /// </summary>
        T Get(int i) => throw new NotSupportedException();
    }
}
