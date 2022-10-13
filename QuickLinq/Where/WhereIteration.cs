// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.Select
{
    public struct WhereIteration<T, TPredicate, TSource, TIteration> :
            IQuickOperation<
                WhereSource<T, TPredicate, TSource, TIteration>,
                WhereIteration<T, TPredicate, TSource, TIteration>>,
            IQuickIteration<T>
        where TPredicate : IFunction<T, bool>
        where TSource : struct
        where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<T>
    {
        private readonly TPredicate predicate;
        private QuickEnumerator<T, TSource, TIteration> enumerator;

        private WhereIteration(in WhereSource<T, TPredicate, TSource, TIteration> source)
        {
            predicate = source.predicate;
            enumerator = source.enumerable.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WhereIteration<T, TPredicate, TSource, TIteration> Create(
            in WhereSource<T, TPredicate, TSource, TIteration> source)
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
    }
}