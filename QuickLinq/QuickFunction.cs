// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.QuickLinq
{
    /// <summary>
    /// Generic interface for struct function.
    /// </summary>
    public interface IQuickFunction<in T, out TOut>
    {
        TOut Invoke(T arg);
    }

    /// <summary>
    /// Generic interface for struct function.
    /// </summary>
    public interface IQuickFunction<in T1, in T2, out TOut>
    {
        TOut Invoke(T1 arg1, T2 arg2);
    }
}