// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;

namespace Cathei.QuickLinq.Operations
{
    public struct OrderBy<T, TComparer, TOperation>
            : IQuickOrderOperation<T, OrderBy<T, TComparer, TOperation>>
        where TComparer : struct, IQuickFunction<T, T, int>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private TComparer comparer;
        private readonly PooledList<T> pooledList;
        private int index;

        // enumerable constructor
        internal OrderBy(in TOperation source, in TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        // the elements of source already saved in pooled list, but we should dispose it after enumeration
        private OrderBy(in TOperation source, in TComparer comparer, in PooledList<T> pooledList) : this()
        {
            this.source = source;
            this.comparer = comparer;
            this.pooledList = pooledList;
            index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrderBy<T, TComparer, TOperation> GetEnumerator()
        {
            var enumerator = source.GetEnumerator();
            var list = PooledList<T>.Create();

            while (enumerator.MoveNext())
                list.Add(enumerator.Current);

            return new(enumerator, comparer, list);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => pooledList[index];
        }

        public bool MoveNext()
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => index = -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            pooledList.Dispose();
        }
    }
}