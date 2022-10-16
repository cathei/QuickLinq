// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

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
    }

    /// <summary>
    /// The base class for ordering operations.
    /// </summary>
    public interface IQuickOrderOperation<out T, out TSelf> : IQuickOperation<T, TSelf>
        where TSelf : struct, IQuickOrderOperation<T, TSelf>
    { }
}
