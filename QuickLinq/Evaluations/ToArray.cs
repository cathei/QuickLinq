// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Cathei.QuickLinq.Collections;

namespace Cathei.QuickLinq
{
    public partial struct QuickEnumerable<T, TOperation>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray()
        {
            PooledList<T> list = new PooledList<T>(source.CanCount ? source.MaxCount : 0);

            foreach (var elem in source)
                list.Add(elem);

            var array = list.ToArray();
            list.Dispose();

            return array;
        }
    }
}
