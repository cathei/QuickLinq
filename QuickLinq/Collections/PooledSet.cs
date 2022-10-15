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
    internal readonly struct PooledSet<T>
    {
        private readonly HashSetPool<T>.Item item;

        private PooledSet(in HashSetPool<T>.Item item)
        {
            this.item = item;
        }

        public static PooledSet<T> Create(IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;

            var item = HashSetPool<T>.Local.Rent();
            item.comparer.Init(comparer);

            return new(item);
        }

        public void Dispose()
        {
            if (item.hashSet != null!)
                HashSetPool<T>.Local.Return(item);
        }
    }

    /// <summary>
    /// EqualityComparer paired with pooled HashSet.
    /// This enables inner implementation swap when borrow HashSet.
    /// </summary>
    internal class PooledEqualityComparer<T> : IEqualityComparer<T>
    {
        private IEqualityComparer<T>? inner;

        public void Init(IEqualityComparer<T> comparer) => this.inner = comparer;

        public void Clear() => inner = null;

        public bool Equals(T x, T y) => inner!.Equals(x, y);

        public int GetHashCode(T obj) => inner!.GetHashCode(obj);
    }

    /// <summary>
    /// HashSetPool itself is not thread-safe, we'll have local HashSetPool per thread.
    /// It's okay to return HashSet to other thread.
    /// </summary>
    internal class HashSetPool<T>
    {
        private static readonly ThreadLocal<HashSetPool<T>> threadLocal = new(() => new());

        public static HashSetPool<T> Local => threadLocal.Value;

        public readonly struct Item
        {
            public readonly HashSet<T> hashSet;
            public readonly PooledEqualityComparer<T> comparer;

            public Item(HashSet<T> hashSet, PooledEqualityComparer<T> comparer)
            {
                this.hashSet = hashSet;
                this.comparer = comparer;
            }
        }

        private readonly Stack<Item> pool = new();

        public Item Rent()
        {
            if (pool.Count > 0)
                return pool.Pop();

            var comparer = new PooledEqualityComparer<T>();
            var hashSet = new HashSet<T>(comparer);

            return new Item(hashSet, comparer);
        }

        public void Return(in Item item)
        {
            // this will clear hash set and comparer items for GC
            item.hashSet.Clear();
            item.comparer.Clear();

            pool.Push(item);
        }
    }
}