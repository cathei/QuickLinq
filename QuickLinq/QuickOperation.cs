// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cathei.QuickLinq
{
    /// <summary>
    /// The base class for all operations.
    /// IQuickOperation is both Enumerable and Enumerator.
    /// This approach will simplify generic type information.
    /// </summary>
    public interface IQuickOperation<out TSelf> : IQuickSlice<TSelf>, IDisposable
        where TSelf : struct, IQuickOperation<TSelf>
    {
        /// <summary>
        /// Same as IEnumerator.MoveNext.
        /// </summary>
        bool MoveNext();

        /// <summary>
        /// Same as IEnumerator.Current with casting.
        /// </summary>
        T GetCurrent<T>();

        TSelf GetEnumerator();
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
