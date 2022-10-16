// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    public struct OrderBy<T, TComparer, TOperation> : IQuickOperation<T, OrderBy<T, TComparer, TOperation>>
        where TComparer : struct, IOrderByComparer<T>
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
        /// Stack used instead of recursive QuickSort.
        /// </summary>
        private readonly PooledList<int> sortingStack;

        private int indexOfIndex;

        // enumerable constructor
        internal OrderBy(in TOperation source, in TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        // the elements of source already saved in pooled list, but we should dispose it after enumeration
        private OrderBy(in TOperation source, in TComparer comparer,
            in PooledList<int> indexesToSort, in PooledList<T> elements, in PooledList<int> sortingStack) : this()
        {
            this.source = source;
            this.comparer = comparer;

            this.indexesToSort = indexesToSort;
            this.elements = elements;
            this.sortingStack = sortingStack;

            // initialize copied comparer
            this.comparer.Initialize(elements);

            indexOfIndex = -1;

            for (int i = 0; i < elements.Count; ++i)
                indexesToSort.Add(i);

             // initial left and right
            sortingStack.Add(elements.Count - 1);
        }

        public OrderBy<T, TComparer, TOperation> GetEnumerator()
        {
            var enumerator = source.GetEnumerator();

            var indexBuffer = PooledList<int>.Create();
            var elementBuffer = PooledList<T>.Create();
            var pivotBuffer = PooledList<int>.Create();

            while (enumerator.MoveNext())
                elementBuffer.Add(enumerator.Current);

            return new(enumerator, comparer, indexBuffer, elementBuffer, pivotBuffer);
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

            return OrderByUtils<T, TComparer>.IncrementalSorting(
                indexesToSort, sortingStack, comparer, indexOfIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            indexOfIndex = -1;

            // initial left and right
            sortingStack.Clear();
            sortingStack.Add(elements.Count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            indexesToSort.Release();
            elements.Release();
            sortingStack.Release();
            comparer.Dispose();
        }

        public bool IsCollection => false;
    }
}