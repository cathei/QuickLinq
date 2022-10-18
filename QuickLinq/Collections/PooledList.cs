﻿using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Collections
{
    public struct PooledList<T> : IDisposable
    {
        private T[] array;
        private int count;

        internal PooledList(int capacity)
        {
            array = capacity > 0 ? SharedArrayPool<T>.Rent(capacity) : System.Array.Empty<T>();
            count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            var newItems = SharedArrayPool<T>.Rent(count + 1);

            if (count > 0)
                System.Array.Copy(array, newItems, count);

            ReturnArray();
            array = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArray()
        {
            if (array.Length == 0)
                return;

            try
            {
                // Clear the elements so that the gc can reclaim the references.
                SharedArrayPool<T>.Return(array, true);
            }
            catch (ArgumentException)
            {
                // oh well, the array pool didn't like our array
            }

            array = System.Array.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Add(T item)
        {
            if (count >= array.Length)
                IncreaseCapacity();

            array[count++] = item;
        }

        internal int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => count;
        }

        internal T[] Array => array;

        internal T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => array[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => array[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArray();
            count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T[] ToArray()
        {
            var result = new T[count];
            System.Array.Copy(array, result, count);
            return result;
        }
    }
}
