// QuickLinq.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.QuickLinq;
using Cathei.QuickLinq.Operations;
using NetFabric.Hyperlinq;

namespace QuickLinq.Benchmarks.Cases;

[MemoryDiagnoser]
public class OrderBy
{
    private const int Count = 10_000;

    private static List<int> list;

    static OrderBy()
    {
        var rand = new Random(1024);
        list = new List<int>();

        for (int i = 0; i < Count; i++)
            list.Add(rand.Next(1000));
    }

    [Benchmark(Baseline = true)]
    public double Linq()
    {
        return list.OrderBy(x => x)
            .Sum();
    }

    [Benchmark]
    public double QuickLinqDelegate()
    {
        return list.Quicken()
            .OrderBy()
            .Sum();
    }

    [Benchmark]
    public double QuickLinqStruct()
    {
        return list.Quicken()
            .OrderBy(new Comparer())
            .Sum();
    }

    [Benchmark]
    public double StructLinqDelegate()
    {
        return list.ToStructEnumerable()
            .Order()
            .Sum();
    }

    [Benchmark]
    public double StructLinqStruct()
    {
        var comparer = new Comparer();

        return list.ToStructEnumerable()
            .Order(ref comparer, x => x)
            .Sum(x => x);
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
