// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Where;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TEnumerator>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TEnumerator>> Where(QuickFunction<T, bool> predicate)
        {
            return new(new(source, predicate));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuickEnumerable<T, Where<T, TEnumerator>> Where(Func<T, bool> predicate)
        {
            return new(new(source, new(predicate)));
        }
    }
}