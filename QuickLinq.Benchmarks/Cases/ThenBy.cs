// QuickLinq.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.QuickLinq;
using Cathei.QuickLinq.Operations;
using NetFabric.Hyperlinq;

namespace QuickLinq.Benchmarks.Cases;

[MemoryDiagnoser]
public class ThenBy
{
    private const int Count = 10_000;

    private static List<(int, int, int)> list;

    static ThenBy()
    {
        var rand = new Random(1024);
        list = new();

        for (int i = 0; i < Count; i++)
            list.Add((rand.Next(10), rand.Next(10), rand.Next(10)));
    }

    [Benchmark(Baseline = true)]
    public double Linq()
    {
        return list.OrderBy(x => x.Item1)
            .ThenBy(x => x.Item2)
            .Sum(x => x.Item3);
    }

    [Benchmark]
    public double QuickLinqDelegate()
    {
        return list.Quicken()
            .OrderBy(x => x.Item1)
            .ThenBy(x => x.Item2)
            .Sum(x => x.Item3);
    }

    [Benchmark]
    public double QuickLinqStruct()
    {
        return list.Quicken()
            .OrderBy(new KeySelector1(), new Comparer(), x => x)
            .ThenBy(new KeySelector2(), new Comparer(), x => x)
            .Sum(new KeySelector3());
    }

    readonly struct KeySelector1 :
        IQuickFunction<(int, int, int), int>
    {
        public int Invoke((int, int, int) x)
        {
            return x.Item1;
        }
    }

    readonly struct KeySelector2 :
        IQuickFunction<(int, int, int), int>
    {
        public int Invoke((int, int, int) x)
        {
            return x.Item2;
        }
    }

    readonly struct KeySelector3 :
        IQuickFunction<(int, int, int), int>
    {
        public int Invoke((int, int, int) x)
        {
            return x.Item3;
        }
    }

    readonly struct Comparer :
        IQuickFunction<int, int, int>
    {
        public int Invoke(int arg1, int arg2)
        {
            return arg1 - arg2;
        }
    }
}
