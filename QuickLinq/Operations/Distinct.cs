// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;

namespace Cathei.QuickLinq.Operations
{
    public struct Distinct<T, TOperation> : IQuickOperation<T, Distinct<T, TOperation>>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        private TOperation source;
        private readonly IEqualityComparer<T>? comparer;
        private readonly PooledSet<T> pooledSet;

        // enumerable constructor
        internal Distinct(in TOperation source, IEqualityComparer<T>? comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        private Distinct(in TOperation source, in PooledSet<T> pooledSet) : this()
        {
            this.source = source;
            this.pooledSet = pooledSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Distinct<T, TOperation> GetEnumerator() => new(source.GetEnumerator(), PooledSet<T>.Create(comparer));

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

                if (pooledSet.Add(source.Current))
                    return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            source.Reset();
            pooledSet.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            pooledSet.Release();
        }
    }
}