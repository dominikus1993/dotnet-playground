using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Attributes;

namespace LinqTest;

public record Shop(int ShopNumber);

internal class ShopComparer : IEqualityComparer<Shop>
{
    public bool Equals(Shop stock1, Shop stock2)
    {
        return stock1.ShopNumber == stock2.ShopNumber;
    }

    public int GetHashCode([DisallowNull] Shop stock)
    {
        return stock.ShopNumber.GetHashCode();
    }
}

[MemoryDiagnoser]
public class DistinctRecords
{
    private static readonly IEqualityComparer<Shop> _comparer = new ShopComparer();
    private IReadOnlyCollection<Shop> _list;
    public DistinctRecords()
    {
        _list = Enumerable.Range(0, 100).Select(num => new Shop(num)).Concat(new []{ new Shop(1)}).ToList();
    }

    [Benchmark]
    public void Distinct()
    {
        var result = _list.Distinct(_comparer).ToList();
    }

    [Benchmark]
    public void HashSet()
    {
        var result = _list.ToHashSet(_comparer);
    }
}