// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    internal static class OrderByUtils<TKey, TComparer>
        where TComparer : IQuickComparer<TKey>
    {
        // "Optimal Incremental Sorting"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncrementalSorting(
                in PooledList<int> indexesToSort, in PooledList<TKey> keys, in PooledList<int> sortingStack,
                in TComparer comparer, int indexOfIndex)
        {
            // passed last element
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

                int pivot = QuickSelectPartition(indexesToSort, keys, comparer, indexOfIndex, top);
                sortingStack.Add(pivot);
            }
        }

        // "Hoare Partition Scheme"
        private static int QuickSelectPartition(
                in PooledList<int> indexesToSort, in PooledList<TKey> keys, TComparer comparer, int left, int right)
        {
            // preventing overflow of the pivot
            int pivot = left + (right - left) / 2;
            int pivotIndex = indexesToSort[pivot];
            var pivotKey = keys[pivotIndex];

            int i = left - 1;
            int j = right + 1;

            int targetIndex;

            while (true)
            {
                do
                {
                    // Move the left index to the right at least once and while the element at
                    // the left index is less than the pivot
                    targetIndex = indexesToSort[++i];
                } while (Compare(keys[targetIndex], pivotKey, targetIndex, pivotIndex, comparer) < 0);

                do
                {
                    // Move the right index to the left at least once and while the element at
                    // the right index is greater than the pivot
                    targetIndex = indexesToSort[--j];
                } while (Compare(keys[targetIndex], pivotKey, targetIndex, pivotIndex, comparer) > 0);

                // If the indices crossed, return
                if (i >= j)
                    return j;

                // Swap the elements at the left and right indices
                indexesToSort[j] = indexesToSort[i];
                indexesToSort[i] = targetIndex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Compare(TKey x, TKey y, int xi, int yi, TComparer comparer)
        {
            int comparison = comparer.Compare(x, y);
            return comparison != 0 ? comparison : xi - yi;
        }
    }
}