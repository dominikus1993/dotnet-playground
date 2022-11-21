using BenchmarkDotNet.Attributes;

using Shops.Domain.Extensions;

namespace Memory;

[MemoryDiagnoser]
public class StringBenchmark
{
    private const string text = "Jana Pawła 2137";
    
    [Benchmark]
    public string Slow()
    {
        return SlowStringExtensions.SanitizeToUrl(text);
    } 
    [Benchmark]
    public string Simple()
    {
        return StringExtensions.SanitizeToUrl(text);
    } 
    
    [Benchmark]
    public string SimpleSpan()
    {
        return StringSpanExtensions.SanitizeToUrl(text);
    } 
    
    [Benchmark]
    public string SimpleRegexSpan()
    {
        return StringSpanRegexExtensions.SanitizeToUrl(text);
    } 
}