// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Operations;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<int, Range> Range(int start, int end)
        {
            return new(new(start, end));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, Repeat<T>> Repeat<T>(T element, uint count)
        {
            return new(new(element, count));
        }
    }
}