// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;
using Cathei.QuickLinq.Comparers;

namespace Cathei.QuickLinq.Operations
{
    internal static class OrderByUtils<T, TComparer> where TComparer : IOrderByComparer<T>
    {
        public static void PartialQuickSort(
            int[] indexesToSort, in TComparer comparer, int left, int right, int min, int max)
        {
            do
            {
                int mid = PartitionHoare(indexesToSort, comparer, left, right);

                if (left < mid && mid >= min)
                    PartialQuickSort(indexesToSort, comparer, left, mid, min, max);

                left = mid + 1;

            } while (left < right && left <= max);
        }

        // Hoare partition scheme
        // This implementation is faster when using struct comparer (more comparison and less copy)
        private static int PartitionHoare(int[] indexesToSort, TComparer comparer, int left, int right)
        {
            // preventing overflow of the pivot
            int pivot = left + ((right - left) >> 1);
            int pivotIndex = indexesToSort[pivot];

            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                // Move the left index to the right at least once and while the element at
                // the left index is less than the pivot
                while (Compare(indexesToSort[++i], pivotIndex, comparer) < 0) { }

                // Move the right index to the left at least once and while the element at
                // the right index is greater than the pivot
                while (Compare(indexesToSort[--j], pivotIndex, comparer) > 0) { }

                // If the indices crossed, return
                if (i >= j)
                    return j;

                // Swap the elements at the left and right indices
                (indexesToSort[i], indexesToSort[j]) = (indexesToSort[j], indexesToSort[i]);
            }
        }


        // // Lomuto partition
        // // Starting pivot should be Count
        // // This implementation is faster when using regular comparer (more copy and less comparison)
        // private static int PartitionLomuto(
        //     in PooledList<int> indexesToSort, in TComparer comparer, int left, int right)
        // {
        //     // preventing overflow of the pivot
        //     int pivot = left + (right - left) / 2;
        //
        //     int pivotIndex = indexesToSort[pivot];
        //
        //     // swap to right
        //     indexesToSort[pivot] = indexesToSort[right];
        //     indexesToSort[right] = pivotIndex;
        //
        //     int location = left;
        //
        //     for (int i = left; i < right; ++i)
        //     {
        //         int compareIndex = indexesToSort[i];
        //
        //         // original index will be used as tiebreaker
        //         if (Compare(compareIndex, pivotIndex, comparer) < 0)
        //         {
        //             // swap location
        //             indexesToSort[i] = indexesToSort[location];
        //             indexesToSort[location] = compareIndex;
        //             ++location;
        //         }
        //     }
        //
        //     // pivot to the last location
        //     indexesToSort[right] = indexesToSort[location];
        //     indexesToSort[location] = pivotIndex;
        //
        //     return location;
        // }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Compare(int x, int y, TComparer comparer)
        {
            int comparison = comparer.Compare(x, y);

            // original index will be used as tiebreaker
            return comparison != 0 ? comparison : x - y;
        }
    }
}