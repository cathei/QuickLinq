// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.QuickLinq
{
    // public interface IQuickOperation<TSource, TIteration>
    // {
    //     // TIteration Create(in TSource source);
    //     // bool MoveNext(ref )
    //
    //
    //
    //     public delegate TIteration CreateDelegate(in TSource source);
    //     public delegate bool MoveNextDelegate(ref TIteration iteration);
    //     public delegate void ResetDelegate(ref TIteration iteration);
    //
    //     CreateDelegate CreateMethod { get; }
    //     MoveNextDelegate MoveNextMethod { get; }
    //     ResetDelegate ResetMethod { get; }
    // }
    //
    // internal static class QuickOperation<T, TSource, TIteration>
    //     where TIteration : struct, IQuickOperation<TSource, TIteration>, IQuickIteration<T, TSource>
    // {
    //     static QuickOperation()
    //     {
    //         TIteration value = default;
    //
    //         Create = value.CreateMethod;
    //         MoveNext = value.MoveNextMethod;
    //         Reset = value.ResetMethod;
    //     }
    //
    //     public static readonly IQuickOperation<TSource, TIteration>.CreateDelegate Create;
    //     public static readonly IQuickOperation<TSource, TIteration>.MoveNextDelegate MoveNext;
    //     public static readonly IQuickOperation<TSource, TIteration>.ResetDelegate Reset;
    //
    //     //
    //     //
    //     // public static bool MoveNext(ref TIteration iteration)
    //     // {
    //     //
    //     // }
    //     //
    //     // public static void Reset(ref TIteration iteration)
    //     // {
    //     //
    //     // }
    // }
}