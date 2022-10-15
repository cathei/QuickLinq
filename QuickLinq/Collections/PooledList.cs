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
        internal readonly List<T> list;

        private PooledList(List<T> list)
        {
            this.list = list;
        }

        public static PooledList<T> Create()
        {
            return new(ListPool<T>.Local.Rent());
        }

        public void Dispose()
        {
            if (list != null)
                ListPool<T>.Local.Return(list);
        }
    }

    /// <summary>
    /// ListPool itself is not thread-safe, we'll have local ListPool per thread.
    /// It's okay to return List to other thread.
    /// </summary>
    internal class ListPool<T>
    {
        private static readonly ThreadLocal<ListPool<T>> threadLocal = new(() => new());

        public static ListPool<T> Local => threadLocal.Value;

        private readonly Stack<List<T>> pool = new();

        public List<T> Rent()
        {
            if (pool.Count > 0)
                return pool.Pop();
            return new();
        }

        public void Return(List<T> list)
        {
            // this will clear list items for GC
            list.Clear();

            pool.Push(list);
        }
    }
}