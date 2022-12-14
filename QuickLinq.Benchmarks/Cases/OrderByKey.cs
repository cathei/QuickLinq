// QuickLinq.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.QuickLinq;
using Cathei.QuickLinq.Operations;
using NetFabric.Hyperlinq;

namespace QuickLinq.Benchmarks.Cases;

[MemoryDiagnoser]
public class OrderByKey : OrderByBenchmarkBase
{
    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double Linq(int[] list)
    {
        return list.OrderBy(x => -x)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double QuickLinqDelegate(int[] list)
    {
        return list.Quicken()
            .OrderBy(x => -x)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double QuickLinqStruct(int[] list)
    {
        var selector = new KeySelector();
        var comparer = new Comparer();

        return list.Quicken()
            .OrderBy(selector, comparer, x => x)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqDelegate(int[] list)
    {
        return list.ToStructEnumerable()
            .OrderBy(x => -x)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqStruct(int[] list)
    {
        var selector = new KeySelector();
        var comparer = new Comparer();

        return list.ToStructEnumerable()
            .OrderBy(ref selector, ref comparer, x => x, x => x)
            .Sum(x => x);
    }

    readonly struct KeySelector :
        StructLinq.IFunction<int, int>,
        NetFabric.Hyperlinq.IFunction<int, int>,
        IQuickFunction<int, int>
    {
        public int Eval(int element)
        {
            return -element;
        }

        public int Invoke(int arg)
        {
            return -arg;
        }
    }

    readonly struct Comparer :
        IQuickFunction<int, int, int>,
        IComparer<int>
    {
        public int Invoke(int arg1, int arg2)
        {
            return arg1 - arg2;
        }

        public int Compare(int x, int y)
        {
            return x - y;
        }
    }
}
