// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Range;

namespace Cathei.QuickLinq
{
    public static partial class QuickEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuickEnumerable<int, RangeSource, RangeEnumerator> Range(int start, int end)
        {
            return new(new(start, end));
        }
    }
}