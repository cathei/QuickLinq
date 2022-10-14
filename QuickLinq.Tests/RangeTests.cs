// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;
using NUnit.Framework;

namespace Cathei.QuickLinq.Tests;

public class RangeTests
{
    [TestCase(0, 10)]
    [TestCase(20, 105)]
    [TestCase(-99, 22)]
    public void TestRange(int start, int end)
    {
        CollectionAssert.AreEqual(
            Enumerable.Range(start, end),
            QuickEnumerable.Range(start, end));
    }
}

