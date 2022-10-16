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
    public static readonly double[][] DoubleData =
    {
        new double[] { },
        new[] { 0.0, 2, -2, 4, 8.1, -4 },
        new[] { -10.0, 4.0, 999.9, 2.2, -4.0, 4.0 },
    };

    public static readonly (int, int)[][] IntTupleData =
    {
        new (int, int)[] { },
        new[] { (0, -1), (0, 0), (0, -2), (0, 2) },
        new[] { (1, 2), (2, 1), (1, 3), (0, 9), (-1, 1), (-1, 3) },
    };

    public static IComparer<double>[] Comparers =
    {
        Comparer<double>.Default,
        Comparer<double>.Create((x, y) => (int)(y - x))
    };

}

public class OrderByTests : OperationTestBase<int, OrderBy<int, Wrap<int>, Quicken<int>>>
{
    protected override QuickEnumerable<int, OrderBy<int, Wrap<int>, Quicken<int>>> Build(int size)
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

public class OrderByKeyTests : OperationTestBase<int, OrderByKey<int, double, Map<int, double>, Quicken<int>>>
{
    protected override QuickEnumerable<int, OrderByKey<int, double, Map<int, double>, Quicken<int>>> Build(int size)
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

public class ThenByTests : OperationTestBase<int, OrderBy<int, Then<int, Wrap<int, ThenByTests.Comparer>, WrapDesc<int>>, Quicken<int>>>
{
    public struct Comparer : IQuickFunction<int, int, int>
    {
        public int Invoke(int x, int y)
        {
            x %= 2;
            y %= 2;
            return x - y;
        }
    }

    protected override QuickEnumerable<int, OrderBy<int, Then<int, Wrap<int, Comparer>, WrapDesc<int>>, Quicken<int>>> Build(int size)
    {
        return Enumerable.Range(-2, size).Reverse().Quicken().
            OrderBy(new Comparer()).ThenByDescending();
    }

    [Test]
    public void Test_EqualToLinq(
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.IntTupleData))] (int, int)[] data,
        [ValueSource(typeof(OrderByTestData), nameof(OrderByTestData.Comparers))] IComparer<double> comparer)
    {
        var linqQuery = data
            .OrderBy(x => -x.Item1, comparer)
            .ThenByDescending(x => x.Item2);

        var quickQuery = data.Quicken()
            .OrderBy(x => -x.Item1, comparer)
            .ThenByDescending(x => x.Item2);

        CollectionAssert.AreEqual(linqQuery, quickQuery.AsEnumerable());
    }
}
