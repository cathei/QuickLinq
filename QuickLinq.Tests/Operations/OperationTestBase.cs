// QuickLinq.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cathei.QuickLinq.Tests;

[TestFixture]
public abstract class OperationTestBase<T, TOperation>
    where TOperation : struct, IQuickOperation<T, TOperation>
{
    protected abstract QuickEnumerable<T, TOperation> Build(int size);

    [TestCase(0)]
    [TestCase(5)]
    [TestCase(10)]
    public void Test_SizeMustBe_SameAsCount(int size)
    {
        var enumerable = Build(size);
        int count = enumerable.Count();

        Assert.AreEqual(size, count);
    }

    [TestCase(2)]
    [TestCase(8)]
    [TestCase(12)]
    public void Test_SizeMustBe_SameAsForEach(int size)
    {
        var enumerable = Build(size);
        var count = 0;

        foreach (var i in enumerable)
        {
            count += 1;
        }

        Assert.AreEqual(size, count);
    }

    [Test]
    public void Test_MoveNext_MustCallFalse_WhenDone()
    {
        var enumerable = Build(5);

        using var enumerator = enumerable.GetEnumerator();

        while (enumerator.MoveNext())
        {
            // do nothing
        }

        Assert.IsFalse(enumerator.MoveNext());
    }

    [Test]
    public void Test_MultipleEnumeration_MustBeSame()
    {
        var enumerable = Build(5);

        var array1 = enumerable.AsEnumerable().ToArray();
        var array2 = enumerable.AsEnumerable().ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void Test_ResettingEnumeration_WillNotBeSupported()
    {
        var enumerable = Build(5);
        using var enumerator = enumerable.GetEnumerator();

        Assert.Throws<NotSupportedException>(() => enumerator.Reset());
    }

}

