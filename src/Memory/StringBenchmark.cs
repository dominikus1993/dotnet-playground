using System.Text.RegularExpressions;

using BenchmarkDotNet.Attributes;

using Shops.Domain.Extensions;

namespace Memory;

public static partial class RemoveRegex
{
    [GeneratedRegex(@"1|x|D|j")]
    public static partial Regex Remove();
    
    [GeneratedRegex(@"J|a|n|P")]
    public static partial Regex Replace();
} 

[MemoryDiagnoser]
public class StringBenchmark
{
    private const string text = "Jana Pawła 2137 jp2gmd xDDDDDDDDDDDDDDDDDDD ja jebie xDDDD  xD jann pawlacz drugi";

    private static readonly Dictionary<char, char> replacements =
        new Dictionary<char, char>() { { 'J', 'j' }, { 'a', 'A' }, { 'n', 'N' }, { 'P', 'p' }, { '3', '7' }, { 'x', 'X' }, { 'D', 'd' } };

    private static readonly Dictionary<string, string> replacementsStr =
        new Dictionary<string, string>() { { "J", "j" }, { "a", "A" }, { "n", "N" }, { "P", "p" }, { "3", "7" }, { "x", "X" }, { "D", "d" } };

    public static readonly Regex ReplaceRegex = new Regex(string.Join('|', replacementsStr.Keys), RegexOptions.Compiled);
    
    private static readonly string[] remove = { "1", "x", "D", "j" };
    
    [Benchmark]
    public string SlowKey()
    {
        return text.SlowReplace(replacements);
    } 
    
    [Benchmark]
    public string SlowSbKey()
    {
        return text.SlowStringBuilderReplace(replacements);
    } 
    
    
    [Benchmark]
    public string FastKey()
    {
        return text.FastReplace(replacements);
    } 
    
    [Benchmark]
    public string SlowRemove()
    {
        return text.SlowRemove(remove);
    } 
    
    
    [Benchmark]
    public string FastRemove()
    {
        return text.FastRemove(remove);
    } 
    
    [Benchmark]
    public string RegexRemove()
    {
        return RemoveRegex.Remove().RegexRemove(text);
    } 
    
    [Benchmark]
    public string RegexReplace()
    {
        return ReplaceRegex.RegexReplace(text, replacementsStr);
    } 
}