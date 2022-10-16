// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    public struct OrderByKey<T, TKey, TComparer, TOperation> : IQuickOperation<T, OrderByKey<T, TKey, TComparer, TOperation>>
        where TComparer : struct, IQuickComparer<T, TKey>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        internal TOperation source;
        internal TComparer comparer;

        private readonly PooledList<int> indexesToSort;

        /// <summary>
        /// Readonly list of elements.
        /// </summary>
        private readonly PooledList<T> elements;

        /// <summary>
        /// Readonly list of keys.
        /// </summary>
        private readonly PooledList<TKey> keys;

        /// <summary>
        /// Stack used instead of recursive QuickSort.
        /// </summary>
        private readonly PooledList<int> sortingStack;

        private int indexOfIndex;

        // enumerable constructor
        internal OrderByKey(in TOperation source, in TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        // the elements of source already saved in pooled list, but we should dispose it after enumeration
        private OrderByKey(in TOperation source, in TComparer comparer,
            in PooledList<int> indexesToSort, in PooledList<T> elements, in PooledList<TKey> keys,
            in PooledList<int> sortingStack) : this()
        {
            this.source = source;
            this.comparer = comparer;

            this.indexesToSort = indexesToSort;
            this.elements = elements;
            this.keys = keys;
            this.sortingStack = sortingStack;

            indexOfIndex = -1;

            for (int i = 0; i < elements.Count; ++i)
                indexesToSort.Add(i);

            // initial left and right
            sortingStack.Add(elements.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrderByKey<T, TKey, TComparer, TOperation> GetEnumerator()
        {
            var enumerator = source.GetEnumerator();

            var indexBuffer = PooledList<int>.Create();
            var elementBuffer = PooledList<T>.Create();
            var keyBuffer = PooledList<TKey>.Create();
            var pivotBuffer = PooledList<int>.Create();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                elementBuffer.Add(current);
                keyBuffer.Add(comparer.SelectKey(current));
            }

            return new(enumerator, comparer, indexBuffer, elementBuffer, keyBuffer, pivotBuffer);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => elements[indexesToSort[indexOfIndex]];
        }

        /// <summary>
        /// Partial quicksort as enumeration goes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            ++indexOfIndex;

            return OrderByUtils.IncrementalSorting(
                indexesToSort, keys, sortingStack, comparer, indexOfIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            indexOfIndex = -1;

            // initial left and right
            sortingStack.Clear();
            sortingStack.Add(elements.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            indexesToSort.Dispose();
            elements.Dispose();
            keys.Dispose();
            sortingStack.Dispose();
        }
    }
}