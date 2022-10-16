// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    public struct OrderBy<T, TComparer, TOperation> : IQuickOperation<T, OrderBy<T, TComparer, TOperation>>
        where TComparer : struct, IQuickComparer<T, T>
        where TOperation : struct, IQuickOperation<T, TOperation>
    {
        internal TOperation source;
        internal TComparer comparer;

        private readonly PooledList<int> sortedIndexes;
        private readonly PooledList<T> elements;

        /// <summary>
        /// Stack used instead of recursive QuickSort.
        /// </summary>
        private readonly PooledList<(int left, int right)> sortingStack;

        private int index;

        // enumerable constructor
        internal OrderBy(in TOperation source, in TComparer comparer) : this()
        {
            this.source = source;
            this.comparer = comparer;
        }

        // enumerator constructor
        // the elements of source already saved in pooled list, but we should dispose it after enumeration
        private OrderBy(in TOperation source, in TComparer comparer,
            in PooledList<int> sortedIndexes, in PooledList<T> elements,
            in PooledList<(int, int)> sortingStack) : this()
        {
            this.source = source;
            this.comparer = comparer;

            this.sortedIndexes = sortedIndexes;
            this.elements = elements;
            this.sortingStack = sortingStack;

            index = -1;

            // initial left and right
            sortingStack.Add((0, elements.Count - 1));
            // QuickSort(0, elements.Count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrderBy<T, TComparer, TOperation> GetEnumerator()
        {
            var enumerator = source.GetEnumerator();

            var indexBuffer = PooledList<int>.Create();
            var elementBuffer = PooledList<T>.Create();
            var rangeBuffer = PooledList<(int, int)>.Create();

            while (enumerator.MoveNext())
                elementBuffer.Add(enumerator.Current);

            return new(enumerator, comparer, indexBuffer, elementBuffer, rangeBuffer);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => elements[index];
        }

        /// <summary>
        /// Partial quicksort as enumeration goes.
        /// </summary>
        public bool MoveNext()
        {
            ++index;

            do
            {
                var (left, right) = sortingStack[^1];

                // pop the stack
                sortingStack.RemoveLast();

                while (left < right)
                {
                    // partial quick sort
                    int pivot = QuickSort(left, right);

                    // push to stack for further process
                    sortingStack.Add((pivot + 1, right));

                    // keep the loop go
                    right = pivot;
                }

                // if (index <= right)
                // {
                //     // no need to pop here, the section is still valid
                //     return true;
                // }
            }
            while (sortingStack.Count > 0);

            // stack is empty, enumeration is done
            return false;
        }

        private int QuickSort(int left, int right)
        {
            // preventing overflow of the pivot
            int pivot = left + (right - left) / 2;

            T pivotValue = elements[pivot];

            for (int i = left; i < right; ++i)
            {

            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            index = -1;

            // initial left and right, at least it is sorted at the point
            sortingStack.Clear();
            sortingStack.Add((0, elements.Count - 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            source.Dispose();
            elements.Dispose();
        }
    }
}