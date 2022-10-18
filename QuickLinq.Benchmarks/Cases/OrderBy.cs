// QuickLinq.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.QuickLinq;
using Cathei.QuickLinq.Operations;
using NetFabric.Hyperlinq;

namespace QuickLinq.Benchmarks.Cases;

public abstract class OrderByBenchmarkBase
{
    private static List<int> smallList;
    private static List<int> mediumList;
    private static List<int> largeList;

    static OrderByBenchmarkBase()
    {
        var rand = new Random(1024);

        Fill(smallList = new(), 20, rand);
        Fill(mediumList = new(), 500, rand);
        Fill(largeList = new(), 10000, rand);
    }

    private static void Fill(List<int> list, int count, Random random)
    {
        for (int i = 0; i < count; i++)
            list.Add(random.Next(1000));
    }

    public static IEnumerable<int[]> Lists
    {
        get
        {
            yield return smallList.ToArray();
            yield return mediumList.ToArray();
            yield return largeList.ToArray();
        }
    }
}

[MemoryDiagnoser]
public class OrderBy : OrderByBenchmarkBase
{
    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double Linq(int[] list)
    {
        return list.OrderBy(x => x)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double QuickLinqDelegate(int[] list)
    {
        return list.Quicken()
            .OrderBy()
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double QuickLinqStruct(int[] list)
    {
        return list.Quicken()
            .OrderBy(new Comparer())
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqDelegate(int[] list)
    {
        return list.ToStructEnumerable()
            .Order()
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqStruct(int[] list)
    {
        var comparer = new Comparer();

        return list.ToStructEnumerable()
            .Order(ref comparer, x => x)
            .Sum(x => x);
    }

    readonly struct Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x - y;
        }
    }
}
