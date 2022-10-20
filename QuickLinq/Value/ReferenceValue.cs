// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Runtime.CompilerServices;

namespace Cathei.QuickLinq.ValueBox
{
    /// <summary>
    /// This is special value type to avoid this issue.
    /// https://github.com/dotnet/runtime/discussions/77192
    /// The original type must be in QuickEnumerable and QuickEnumerator only.
    /// </summary>
    public struct ReferenceValue
    {
        private object value;

        public ReferenceValue(object value)
        {
            this.value = value;
        }

        internal T As<T>() => Unsafe.As<object, T>(ref value);
    }
}

