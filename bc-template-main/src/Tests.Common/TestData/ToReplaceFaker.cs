using Bogus;
using NGrid.Customer.ToReplace.Infrastructure;

namespace NGrid.Customer.ToReplace.Tests.Common.TestData;

public static class ToReplaceFaker
{
    public static Faker<Domain.ToReplace> ToReplaceRequestFaker() =>
        new Faker<Domain.ToReplace>("en_US")
            .RuleFor(x => x.Description, x => x.Random.Int(100, 500).ToString())
            .RuleFor(x => x.Key, x => x.Random.Int(100, 500));

    public static Faker<ToReplaceDao> ToReplaceDaoFaker() => new("en_US");

    private static readonly Random Random = new();
    private static readonly object SyncLock = new();

    public static int RandomNumber()
    {
        lock (SyncLock)
        {
            return Random.Next(0, Int32.MaxValue);
        }
    }
}