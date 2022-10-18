// QuickLinq.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using Cathei.QuickLinq.Comparers;
using Cathei.QuickLinq.Operations;
using Cathei.QuickLinq.Quick;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cathei.QuickLinq.Tests;

public static class OrderByTestData
{
    static OrderByTestData()
    {
        Random rng = new Random();

        var randomData1 = GenRandomData(rng, 999);
        var randomData2 = GenRandomData(rng, 10000);

        DoubleData = new[]
        {
            new double[] { },
            new[] { 0.0, 2, -2, 4, 8.1, -4 },
            new[] { -10.0, 4.0, 999.9, 2.2, -4.0, 4.0 },
            randomData1, randomData2
        };
    }

    public static double[] GenRandomData(Random rng, int count)
    {
        var list = new List<double>();

        for (int i = 0; i < count; ++i)
            list.Add(rng.NextDouble() * 1000);

        return list.ToArray();
    }

    public static readonly double[][] DoubleData;

    public static readonly (int, int)[][] IntTupleData =
    {
        new (int, int)[] { },
        new[] { (0, -1), (0, 0), (0, -2), (0, 2) },
        new[] { (1, 2), (2, 1), (1, 3), (0, 9), (-1, 1), (-1, 3) },
    };

    public static IComparer<double>[] Comparers =
    {
        Comparer<double>.Default,
        // this will test stable sorting
        Comparer<double>.Create((x, y) => (int)y - (int)x)
    };

}

public class OrderByTests : OperationTestBase<int, OrderBy<int, Wrap<int, Comparer<int>>, Quicken<int>>>
{
    protected override QuickEnumerable<int, OrderBy<int, Wrap<int, Comparer<int>>, Quicken<int>>> Build(int size)
    {
        return Enumerable.Range(-2, size).Reverse().Quicken().OrderBy();
    }

    [Test]
    public void Test_EqualToLinq(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.DoubleData))] IEnumerable<double> data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data.OrderBy(x => x, comparer);
        var quickQuery = data.Quicken().OrderBy(comparer);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }
}

public class OrderByDescTests : OperationTestBase<int, OrderBy<int, WrapDesc<int, Comparer<int>>, Quicken<int>>>
{
    protected override QuickEnumerable<int, OrderBy<int, WrapDesc<int, Comparer<int>>, Quicken<int>>> Build(int size)
    {
        return Enumerable.Range(-2, size).Quicken().OrderByDescending();
    }

    [Test]
    public void Test_EqualToLinq(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.DoubleData))] IEnumerable<double> data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data.OrderByDescending(x => x, comparer);
        var quickQuery = data.Quicken().OrderByDescending(comparer);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }
}

public class OrderByKeyTests : OperationTestBase<int, OrderBy<int, Map<int, double, Call<int, double>, Comparer<double>>, Quicken<int>>>
{
    protected override QuickEnumerable<int, OrderBy<int, Map<int, double, Call<int, double>, Comparer<double>>, Quicken<int>>> Build(int size)
    {
        return Enumerable.Range(-2, size).Reverse().Quicken().
            OrderBy(x => Math.Abs(x * 2.0));
    }

    [Test]
    public void Test_EqualToLinq(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.DoubleData))] IEnumerable<double> data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data.OrderBy(x => -x, comparer);
        var quickQuery = data.Quicken().OrderBy(x => -x, comparer);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }
}

public class OrderByKeyDescTests : OperationTestBase<int, OrderBy<int, MapDesc<int, double, Call<int, double>, Comparer<double>>, Quicken<int>>>
{
    protected override QuickEnumerable<int, OrderBy<int, MapDesc<int, double, Call<int, double>, Comparer<double>>, Quicken<int>>> Build(int size)
    {
        return Enumerable.Range(-2, size).Quicken().
            OrderByDescending(x => Math.Abs(x * 2.0));
    }

    public struct Selector : IQuickFunction<double, double>
    {
        public double Invoke(double arg) => -arg;
    }

    [Test]
    public void Test_EqualToLinq(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.DoubleData))] IEnumerable<double> data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data.OrderByDescending(x => -x, comparer);
        var quickQuery = data.Quicken().OrderByDescending(new Selector(), comparer, x => x);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }
}

public class ThenByTests : OperationTestBase<int, OrderBy<int, Then<int, Wrap<int, ThenByTests.Comparer>, MapDesc<int, int, Call<int, int>, Comparer<int>>>, Quicken<int>>>
{
    public struct Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            x %= 2;
            y %= 2;
            return x - y;
        }
    }

    protected override QuickEnumerable<int, OrderBy<int, Then<int, Wrap<int, Comparer>, MapDesc<int, int, Call<int, int>, Comparer<int>>>, Quicken<int>>> Build(int size)
    {
        return Enumerable.Range(-2, size).Reverse().Quicken().
            OrderBy(new Comparer()).ThenByDescending(x => x);
    }

    [Test]
    public void Test_EqualToLinq_DescAsc(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.IntTupleData))] (int, int)[] data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data
            .OrderByDescending(x => -x.Item1, comparer)
            .ThenBy(x => x.Item2);

        var quickQuery = data.Quicken()
            .OrderByDescending(x => (double)-x.Item1, comparer)
            .ThenBy(x => x.Item2);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }

    [Test]
    public void Test_EqualToLinq_AscDesc(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.IntTupleData))] (int, int)[] data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data
            .OrderBy(x => -x.Item1, comparer)
            .ThenByDescending(x => x.Item2);

        var quickQuery = data.Quicken()
            .OrderBy(x => (double)-x.Item1, comparer)
            .ThenByDescending(x => x.Item2);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }

    [Test]
    public void PerformanceProfile()
    {
        Random rng = new Random();

        var randomData = OrderByTestData.GenRandomData(rng, 999);
        double x = 0;

        for (int i = 0; i < 1000; ++i)
        {
            x += randomData.Quicken().OrderBy().Sum();
        }

        Assert.AreNotEqual(0, x);
    }
}
