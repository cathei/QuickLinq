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
    public interface IQuickOperation<out T, out TSelf> : IQuickSlice<TSelf>, IEnumerable<T>, IEnumerator<T>
        where TSelf : struct, IQuickOperation<T, TSelf>
    {
        new TSelf GetEnumerator();

        // interface default implementation
        object? IEnumerator.Current => Current;

        // interface default implementation
        void IEnumerator.Reset() => throw new NotSupportedException();

        // interface default implementation
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        // interface default implementation
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public interface IQuickSlice<out TSelf>
        where TSelf : struct, IQuickSlice<TSelf>
    {
        /// <summary>
        /// Collection optimization
        /// CanCount operation should be always CanSlice as well
        /// </summary>
        bool CanCount { get; }

        /// <summary>
        /// Collection optimization
        /// Only valid when CanCount is true
        /// </summary>
        int MaxCount => throw new NotSupportedException();

        /// <summary>
        /// Collection optimization
        /// GetSliceEnumerator is only valid when IsCollection is true
        /// </summary>
        bool CanSlice { get; }

        /// <summary>
        /// Collection optimization
        /// Only valid when CanSlice is true
        /// Skip + Take can exceed MaxCount
        /// </summary>
        TSelf GetSliceEnumerator(int skip, int take) => throw new NotSupportedException();
    }

}
