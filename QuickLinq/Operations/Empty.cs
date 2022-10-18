// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Operations
{
    public readonly struct Empty<T> : IQuickOperation<T, Empty<T>>
    {
        public Empty<T> GetEnumerator() => this;

        public bool MoveNext() => false;

        public T Current => throw new NotSupportedException();

        public void Dispose() { }

        public bool CanCount => true;

        public int MaxCount => 0;

        public bool CanSlice => true;

        public Empty<T> GetSliceEnumerator(int skip, int take) => this;
    }
}
