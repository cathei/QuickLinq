// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    internal static class OrderByUtils
    {
        // "Optimal Incremental Sorting"
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncrementalSorting<TKey, TComparer>(
                in PooledList<int> indexesToSort, in PooledList<TKey> keys, in PooledList<int> sortingStack,
                in TComparer comparer, int indexOfIndex)
            where TComparer : IQuickComparer<TKey>
        {
            // index out of range
            if (indexOfIndex >= keys.Count)
                return false;

            while (true)
            {
                int top = sortingStack[^1];

                if (indexOfIndex == top)
                {
                    sortingStack.RemoveLast();
                    return true;
                }

                int pivot = QuickSelectPartition(indexesToSort, keys, comparer, indexOfIndex, top - 1);
                sortingStack.Add(pivot);
            }
        }

        private static int QuickSelectPartition<TKey, TComparer>(
                in PooledList<int> indexesToSort, in PooledList<TKey> keys, TComparer comparer, int left, int right)
            where TComparer : IQuickComparer<TKey>
        {
            // preventing overflow of the pivot
            int pivot = left + (right - left) / 2;

            int pivotIndex = indexesToSort[pivot];
            var pivotKey = keys[pivotIndex];

            // swap to right
            indexesToSort[pivot] = indexesToSort[right];
            indexesToSort[right] = pivotIndex;

            int location = left;

            for (int i = left; i < right; ++i)
            {
                int compareIndex = indexesToSort[i];
                var compareKey = keys[compareIndex];

                int comparison = comparer.Compare(compareKey, pivotKey);

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