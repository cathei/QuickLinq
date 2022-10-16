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
        // reference: https://codeblog.jonskeet.uk/2011/01/07/reimplementing-linq-to-objects-part-26d-fixing-the-key-selectors-and-yielding-early/
        public static bool QuickSortMoveNext<TKey, TComparer>(
                in PooledList<int> indexesToSort, in PooledList<TKey> keys, in PooledList<(int, int)> sortingStack,
                in TComparer comparer, int indexOfIndex)
            where TComparer : IQuickComparer<TKey>
        {
            // index out of range
            if (indexOfIndex >= keys.Count)
                return false;

            while (sortingStack.Count > 0)
            {
                // pop to next range
                var (left, right) = sortingStack[^1];
                sortingStack.RemoveLast();

                if (left < right)
                {
                    // partial quick sort
                    int pivot = QuickSortPartition(indexesToSort, keys, comparer, left, right);

                    // push to stack for further process
                    sortingStack.Add((pivot + 1, right));
                    sortingStack.Add((left, pivot - 1));
                }
                else if (indexOfIndex <= right)
                {
                    // it's sorted for this range
                    return true;
                }
            }

            // the sorting is done
            return true;
        }

        private static int QuickSortPartition<TKey, TComparer>(
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