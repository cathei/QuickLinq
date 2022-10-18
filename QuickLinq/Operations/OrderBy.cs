// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
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

        /// <summary>
        /// Readonly list of indexes.
        /// </summary>
        private readonly PooledList<int> indexes;

        /// <summary>
        /// Readonly list of elements.
        /// </summary>
        private readonly PooledList<T> elements;

        private int indexOfIndex;

        // enumerable constructor
        internal OrderBy(in TOperation source, in TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        // the elements of source already saved in pooled list, but we should dispose it after enumeration
        private OrderBy(in TOperation source, PooledList<int> indexes, PooledList<T> elements, int startIndex) : this()
        {
            this.source = source;
            this.indexes = indexes;
            this.elements = elements;

            indexOfIndex = startIndex - 1;
        }

        public OrderBy<T, TComparer, TOperation> GetEnumerator() => GetSliceEnumerator(0, int.MaxValue);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => elements[indexes[indexOfIndex]];
        }

        /// <summary>
        /// Partial quicksort as enumeration goes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++indexOfIndex < indexes.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            indexes.Release();
            elements.Release();
        }

        public bool CanCount => source.CanCount;

        public int MaxCount => source.MaxCount;

        public bool CanSlice => true;

        public OrderBy<T, TComparer, TOperation> GetSliceEnumerator(int skip, int take)
        {
            var enumerator = source.GetEnumerator();

            // prepare temporary buffers
            var indexBuffer = PooledList<int>.Create();
            var elementBuffer = PooledList<T>.Create();

            // copy first to not modify member variable
            var comparerCopy = comparer;

            // load all elements
            while (enumerator.MoveNext())
                elementBuffer.Add(enumerator.Current);

            // initialize all indexes
            for (int i = 0; i < elementBuffer.Count; ++i)
                indexBuffer.Add(i);

            // initialize copied comparer with keys
            comparerCopy.Initialize(elementBuffer);

            OrderByUtils<T, TComparer>.PartialQuickSort(
                indexBuffer, comparerCopy, skip, Math.Min(skip + take, indexBuffer.Count) - 1);

            comparerCopy.Dispose();

            return new(enumerator, indexBuffer, elementBuffer, skip);
        }
    }
}