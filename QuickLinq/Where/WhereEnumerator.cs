// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Select
{
    public struct WhereEnumerator<T, TPredicate, TSource, TEnumerator> :
            IQuickOperation<
                WhereSource<T, TPredicate, TSource, TEnumerator>,
                WhereEnumerator<T, TPredicate, TSource, TEnumerator>>,
            IQuickEnumerator<T>
        where TPredicate : IFunction<T, bool>
        where TSource : struct
        where TEnumerator : struct, IQuickOperation<TSource, TEnumerator>, IQuickEnumerator<T>
    {
        private readonly TPredicate predicate;
        private TEnumerator enumerator;

        private WhereEnumerator(in WhereSource<T, TPredicate, TSource, TEnumerator> source)
        {
            predicate = source.predicate;
            enumerator = source.enumerable.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WhereEnumerator<T, TPredicate, TSource, TEnumerator> Create(
            in WhereSource<T, TPredicate, TSource, TEnumerator> source)
        {
            return new(source);
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (true)
            {
                if (!enumerator.MoveNext())
                    return false;

                if (predicate.Invoke(enumerator.Current))
                    return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => enumerator.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() => enumerator.Dispose();
    }
}