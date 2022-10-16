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
    private static List<(int, int, int)> smallList;
    private static List<(int, int, int)> mediumList;
    private static List<(int, int, int)> largeList;

    static Thenby()
    {
        var rand = new Random(1024);

        Fill(smallList = new(), 20, rand);
        Fill(mediumList = new(), 500, rand);
        Fill(largeList = new(), 10000, rand);
    }

    private static void Fill(List<(int, int, int)> list, int count, Random random)
    {
        for (int i = 0; i < count; i++)
            list.Add((random.Next(10), random.Next(10), random.Next(10)));
    }

    public static IEnumerable<(int, int, int)[]> Lists
    {
        get
        {
            yield return smallList.ToArray();
            yield return mediumList.ToArray();
            yield return largeList.ToArray();
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double Linq((int, int, int)[] list)
    {
        return list.OrderBy(x => x.Item1)
            .ThenBy(x => x.Item2)
            .Sum(x => x.Item3);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double QuickLinqDelegate((int, int, int)[] list)
    {
        return list.Quicken()
            .OrderBy(x => x.Item1)
            .ThenBy(x => x.Item2)
            .Sum(x => x.Item3);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double QuickLinqStruct((int, int, int)[] list)
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
