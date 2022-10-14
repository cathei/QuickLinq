// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;
using NUnit.Framework;

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
            QuickEnumerable.Return(from).Select(_ => to));
    }
}

