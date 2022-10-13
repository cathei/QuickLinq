// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq.Range
{
    public readonly struct RangeSource
    {
        internal readonly int start;
        internal readonly int end;

        internal RangeSource(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    }
}