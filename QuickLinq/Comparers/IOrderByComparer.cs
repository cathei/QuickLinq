// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Common interface for OrderBy comparers.
    /// Also used as data container of keys.
    /// </summary>
    public interface IOrderByComparer<T> : IDisposable
    {
        /// <summary>
        /// Create a list of keys
        /// </summary>
        void Initialize(PooledList<T> elements);

        /// <summary>
        /// Compare with index of keys
        /// </summary>
        int Compare(int x, int y);
    }
}
