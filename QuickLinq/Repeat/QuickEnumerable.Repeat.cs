// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Range;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<T, RepeatSource<T>, RepeatEnumerator<T>> Repeat<T>(T element, uint count)
        {
            return new(new(element, count));
        }
    }
}