// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Comparers
{
    /// <summary>
    /// Common interface for comparers.
    /// </summary>
    public interface IQuickComparer<T, TKey>
    {
        TKey SelectKey(in T element);
        int Compare(in TKey x, in TKey y);
    }
}
