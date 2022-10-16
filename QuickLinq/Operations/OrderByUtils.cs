// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    internal static class OrderByUtils<T, TComparer>
        where TComparer : IQuickComparer<T>
    {
        // "Optimal Incremental Sorting"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncrementalSorting(
                in PooledList<int> indexesToSort, in PooledList<int> sortingStack, in TComparer comparer, int indexOfIndex)
        {
            // passed last element
            if (indexOfIndex >= indexesToSort.Count)
                return false;

            while (true)
            {
                int top = sortingStack[^1];

                if (indexOfIndex == top)
                {
                    sortingStack.RemoveLast();
                    return true;
                }

                // int pivot = QuickSelectPartition(indexesToSort, comparer, indexOfIndex, top);
                int pivot = QuickSelectPartitionTemp(indexesToSort, comparer, indexOfIndex, top - 1);
                sortingStack.Add(pivot);
            }
        }

        // "Hoare Partition Scheme"
        private static int QuickSelectPartition(
                in PooledList<int> indexesToSort, TComparer comparer, int left, int right)
        {
            // preventing overflow of the pivot
            int pivot = left + (right - left) / 2;
            int pivotIndex = indexesToSort[pivot];

            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                int targetIndex;

                do
                {
                    // Move the left index to the right at least once and while the element at
                    // the left index is less than the pivot
                    targetIndex = indexesToSort[++i];
                } while (Compare(targetIndex, pivotIndex, comparer) < 0);

                do
                {
                    // Move the right index to the left at least once and while the element at
                    // the right index is greater than the pivot
                    targetIndex = indexesToSort[--j];
                } while (Compare(targetIndex, pivotIndex, comparer) > 0);

                // If the indices crossed, return
                if (i >= j)
                    return j;

                // Swap the elements at the left and right indices
                indexesToSort[j] = indexesToSort[i];
                indexesToSort[i] = targetIndex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Compare(int x, int y, TComparer comparer)
        {
            int comparison = comparer.Compare(x, y);
            return comparison != 0 ? comparison : x - y;
        }

        private static int QuickSelectPartitionTemp(
            in PooledList<int> indexesToSort, TComparer comparer, int left, int right)
        {
            // preventing overflow of the pivot
            int pivot = left + (right - left) / 2;

            int pivotIndex = indexesToSort[pivot];

            // swap to right
            indexesToSort[pivot] = indexesToSort[right];
            indexesToSort[right] = pivotIndex;

            int location = left;

            for (int i = left; i < right; ++i)
            {
                int compareIndex = indexesToSort[i];

                int comparison = comparer.Compare(compareIndex, pivotIndex);

                // original index will be used as tiebreaker
                if (comparison < 0 || (comparison == 0 && compareIndex < pivotIndex))
                {
                    // swap location
                    indexesToSort[i] = indexesToSort[location];
                    indexesToSort[location] = compareIndex;
                    ++location;
                }
            }

            // pivot to the last location
            indexesToSort[right] = indexesToSort[location];
            indexesToSort[location] = pivotIndex;

            return location;
        }

    }
}