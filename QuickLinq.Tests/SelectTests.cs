// QuickLinq.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cathei.QuickLinq.Tests;

public class SelectTests
{
    [TestCase(0, 10.0)]
    [TestCase(20, 10522.0)]
    [TestCase(-99, -22.0)]
    public void TestRange(int from, double to)
    {
        CollectionAssert.AreEqual(
            Enumerable.Repeat(from, 1).Select(_ => to),
            QuickEnumerable.Return(from).Select(_ => to).AsEnumerable());

        CollectionAssert.AreEqual(
            Enumerable.Repeat(from, 1).Select((x, i) => i),
            QuickEnumerable.Return(from).Select((x, i) => i).AsEnumerable());
    }

    [Test]
    public void EnsureBenchmark()
    {
        var predicate = new Predicate();
        var selector = new Selector();

        var expected = Enumerable.Range(0, 10000)
               .Where(x => x % 2 == 0)
               .Select(x => x * 2)
               .Sum();

        var result = QuickEnumerable
               .Range(0, 10000)
               .Where(predicate)
               .Select(selector, x => x)
               .Sum();

        Assert.AreEqual(expected, result);
    }


    readonly struct Predicate : IQuickFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg % 2 == 0;
        }
    }

    readonly struct Selector : IQuickFunction<int, double>
    {
        public double Invoke(int arg)
        {
            return arg * 2.0;
        }
    }
}

