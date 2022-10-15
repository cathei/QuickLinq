// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Sum<TFunc>(TFunc selector) where TFunc : IQuickFunction<T, int>
        {
            using var enumerator = GetEnumerator();
            int result = 0;

            while (enumerator.MoveNext())
                result += selector.Invoke(enumerator.Current);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Sum(Func<T, int> selector)
        {
            using var enumerator = GetEnumerator();
            int result = 0;

            while (enumerator.MoveNext())
                result += selector.Invoke(enumerator.Current);
            return result;
        }
    }

    public partial class QuickEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum<TOperation>(this QuickEnumerable<int, TOperation> source)
            where TOperation : struct, IQuickOperation<int, TOperation>
        {
            using var enumerator = source.GetEnumerator();
            int result = 0;

            while (enumerator.MoveNext())
                result += enumerator.Current;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum<TOperation>(this QuickEnumerable<float, TOperation> source)
            where TOperation : struct, IQuickOperation<float, TOperation>
        {
            using var enumerator = source.GetEnumerator();
            float result = 0;

            while (enumerator.MoveNext())
                result += enumerator.Current;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum<TOperation>(this QuickEnumerable<double, TOperation> source)
            where TOperation : struct, IQuickOperation<double, TOperation>
        {
            using var enumerator = source.GetEnumerator();
            double result = 0;

            while (enumerator.MoveNext())
                result += enumerator.Current;
            return result;
        }
    }
}