// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq.Range
{
    public readonly struct RepeatSource<T>
    {
        internal readonly T element;
        internal readonly uint count;

        internal RepeatSource(in T element, uint count)
        {
            this.element = element;
            this.count = count;
        }
    }
}