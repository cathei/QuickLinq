// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cathei.QuickLinq
{
    public struct QuickEnumerator<T, TOperation> : IEnumerator<T>
        where TOperation : struct, IQuickOperation<TOperation>
    {
        private TOperation enumerator;

        public QuickEnumerator(TOperation enumerator)
        {
            this.enumerator = enumerator;
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public T Current => enumerator.GetCurrent<T>();

        public void Dispose() => enumerator.Dispose();

        object? IEnumerator.Current => Current;

        void IEnumerator.Reset() => throw new NotSupportedException();
    }
}
