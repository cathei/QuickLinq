// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Collections
{
    /// <summary>
    /// Struct that represents borrowed List.
    /// The struct will remain internal, since it is not possible to ensure the reference is not retained after Disposing.
    /// </summary>
    internal readonly struct PooledList<T> : IDisposable
    {
        private readonly List<T> list;

        private PooledList(List<T> list)
        {
            this.list = list;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PooledList<T> Create()
        {
            return new(ListPool<T>.Local.Rent());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (list != null)
                ListPool<T>.Local.Return(list);
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => list.Count;
        }

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => list[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T element)
        {
            list.Add(element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            list.Clear();
        }
    }

    /// <summary>
    /// ListPool itself is not thread-safe, we'll have local ListPool per thread.
    /// It's okay to return List to other thread, but TODO consider (sounds like rare case for linq).
    /// </summary>
    internal class ListPool<T>
    {
        [ThreadStatic]
        private static ListPool<T>? threadLocal;

        public static ListPool<T> Local
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => threadLocal ??= new();
        }

        private readonly Stack<List<T>> pool = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> Rent()
        {
            if (pool.Count > 0)
                return pool.Pop();
            return new();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(List<T> list)
        {
            // this will clear list items for GC
            list.Clear();

            pool.Push(list);
        }
    }
}