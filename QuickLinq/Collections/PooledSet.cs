// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq.Collections
{
    /// <summary>
    /// Struct represents the borrowed HashSet.
    /// The struct will remain internal, since it is not possible to ensure the reference is not retained after Disposing.
    /// </summary>
    internal struct PooledSet<T>
    {
        private HashSet<T> hashSet;

        private PooledSet(HashSet<T> hashSet)
        {
            this.hashSet = hashSet;
        }

        public static PooledSet<T> Create()
        {
            return new(HashSetPool<T>.Local.Rent());
        }

        public void Dispose()
        {
            if (hashSet != null)
                HashSetPool<T>.Local.Return(hashSet);
        }
    }

    /// <summary>
    /// HashSetPool itself is not thread-safe, we'll have local HashSetPool per thread.
    /// It's okay to return HashSet to other thread.
    /// </summary>
    internal class HashSetPool<T>
    {
        private static readonly ThreadLocal<HashSetPool<T>> threadLocal = new(() => new());

        public static HashSetPool<T> Local => threadLocal.Value;

        private readonly Stack<HashSet<T>> pool = new();

        public HashSet<T> Rent()
        {
            if (pool.Count > 0)
                return pool.Pop();
            return new();
        }

        public void Return(HashSet<T> list)
        {
            // this will clear list items for GC
            list.Clear();

            pool.Push(list);
        }
    }
}