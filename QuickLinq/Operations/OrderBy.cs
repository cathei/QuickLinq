// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

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
        private PooledList<int> indexes;

        /// <summary>
        /// Readonly list of elements.
        /// </summary>
        private PooledList<T> elements;

        private int indexOfIndex;

        // enumerable constructor
        internal OrderBy(in TOperation source, in TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        // the elements of source already saved in pooled list, but we should dispose it after enumeration
        private OrderBy(in PooledList<int> indexes, in PooledList<T> elements, int startIndex) : this()
        {
            this.indexes = indexes;
            this.elements = elements;

            indexOfIndex = startIndex - 1;
        }

        public OrderBy<T, TComparer, TOperation> GetEnumerator() => GetSliceEnumerator(0, -1);

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
            indexes.Dispose();
            elements.Dispose();
        }

        public bool CanCount => source.CanCount;

        public int MaxCount => source.MaxCount;

        public bool CanSlice => true;

        public OrderBy<T, TComparer, TOperation> GetSliceEnumerator(int skip, int take)
        {
            using var enumerator = source.GetEnumerator();

            // prepare temporary buffers
            var elementBuffer = new PooledList<T>(source.CanCount ? source.MaxCount : 0);

            // load all elements
            while (enumerator.MoveNext())
                elementBuffer.Add(enumerator.Current);

            int min = skip;
            int max = take < 0 ? elementBuffer.Count - 1 : min + take - 1;

            if (min > max)
            {
                // in this case, it actually should not be enumerated at all
                elementBuffer.Dispose();
                return new(new(0), new(0), 0);
            }

            var indexBuffer = new PooledList<int>(elementBuffer.Count);

            // initialize all indexes
            for (int i = 0; i < elementBuffer.Count; ++i)
                indexBuffer.Add(i);

            // copy first to not modify member variable
            var comparerCopy = comparer;

            // initialize copied comparer with keys
            comparerCopy.Initialize(elementBuffer);

            OrderByUtils<T, TComparer>.PartialQuickSort(
                indexBuffer.Array, comparerCopy, 0, elementBuffer.Count - 1, min, max);

            comparerCopy.Dispose();

            return new(indexBuffer, elementBuffer, skip);
        }
    }
}