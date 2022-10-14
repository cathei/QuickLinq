// QuickLinq, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using NUnit.Framework;

namespace Cathei.QuickLinq.Tests;

public class RangeTests
{
    [TestCase(0, 1)]
    public void TestRange(int start, int end)
    {
        QuickEnumerable.Range(start, end)
            .Select(x => x + 1)
            .Select(x => x + 2)
            .Select(x => x + 3)
            .Select(x => x + 5)
            .Select(x => x + 8)
            .Select(x => x / 10)
            .Select(x => x * 100)
            .Where(x => x % 2 == 0)
            .Select(x => x - 10)
            .Select();

    }
}

