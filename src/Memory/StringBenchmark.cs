using BenchmarkDotNet.Attributes;

using Shops.Domain.Extensions;

namespace Memory;

[MemoryDiagnoser]
public class StringBenchmark
{
    private const string text = "Jana Pawła 2137 jp2gmd xDDDDDDDDDDDDDDDDDDD ja jebie xDDDD";

    private static readonly Dictionary<char, char> replacements =
        new Dictionary<char, char>() { { 'J', 'j' }, { 'a', 'A' }, { 'n', 'N' }, { 'P', 'p' }, { '3', '7' }, { 'x', 'X' }, { 'D', 'd' } };


    [Benchmark]
    public string SlowKey()
    {
        return text.SlowReplace(replacements);
    } 
    [Benchmark]
    public string FastKey()
    {
        return text.FastReplace(replacements);
    } 
}