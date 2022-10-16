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
    /// The API will remain internal, since it is not possible to ensure the reference is not retained after Disposing.
    /// </summary>
    public readonly struct PooledList<T> : IDisposable
    {
        private readonly List<T> list;

        private PooledList(List<T> list)
        {
            this.list = list;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PooledList<T> Create()
        {
            return new(ListPool<T>.Local.Rent());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (list != null)
                ListPool<T>.Local.Return(list);
        }

        internal int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => list.Count;
        }

        internal T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => list[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => list[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Add(T element)
        {
            list.Add(element);
        }

        // used for stack-like approach
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RemoveLast()
        {
            list.RemoveAt(Count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Clear()
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