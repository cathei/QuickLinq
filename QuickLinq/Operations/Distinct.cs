// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;

namespace Cathei.QuickLinq.Operations
{
    public struct Distinct<T, TComparer, TOperation> : IQuickOperation<T, Distinct<T, TComparer, TOperation>>
        where TComparer : IEqualityComparer<T>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private TComparer? comparer;
        private PooledSet<T, TComparer> pooledSet;

        // enumerable constructor
        internal Distinct(in TOperation source, TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        private Distinct(in TOperation source, in PooledSet<T, TComparer> pooledSet) : this()
        {
            this.source = source;
            this.pooledSet = pooledSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Distinct<T, TComparer, TOperation> GetEnumerator()
        {
            return new(source.GetEnumerator(), new PooledSet<T, TComparer>(
                source.CanCount ? source.MaxCount : 0, comparer!));
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => source.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (true)
            {
                if (!source.MoveNext())
                    return false;

                if (pooledSet.AddIfNotPresent(source.Current))
                    return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            pooledSet.Dispose();
        }

        public bool CanCount => false;

        public bool CanSlice => false;
    }
}