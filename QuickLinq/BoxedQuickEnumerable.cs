// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq
{
    /// <summary>
    /// QuickEnumerable that implements IEnumerable
    /// </summary>
    public class BoxedQuickEnumerable<T, TOperation> : IEnumerable<T>
        where TOperation : struct, IQuickOperation<TOperation>
    {
        private TOperation source;

        public BoxedQuickEnumerable(in TOperation source)
        {
            this.source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerator<T, TOperation> GetEnumerator() => new(source.GetEnumerator());

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}