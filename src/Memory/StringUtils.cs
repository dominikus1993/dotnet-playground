using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Memory;

public static class StringUtils
{

    public static string FastReplace(this string str, Dictionary<char, char> replacements)
    {
        Span<char> textChars = stackalloc char[str.Length];
        str.CopyTo(textChars);
        int i = 0;
        foreach(var element in textChars)
        {
            ref char replacement = ref CollectionsMarshal.GetValueRefOrNullRef(replacements, element);
            if (!Unsafe.IsNullRef(ref replacement))
            {
                textChars[i] = replacement;
            }
            i++;
        }
        return new string(textChars);
    }
    
    public static string SlowReplace(this string str, IReadOnlyDictionary<char, char> replacements)
    {
        var result = str;
        foreach (var repl in replacements)
        {
            result = result.Replace(repl.Key, repl.Value);
        }
        return result;
    }
    public static string SlowStringBuilderReplace(this string str, IReadOnlyDictionary<char, char> replacements)
    {
        var result = new StringBuilder(str);
        foreach (var repl in replacements)
        {
            result = result.Replace(repl.Key, repl.Value);
        }
        return result.ToString();
    }
    
    public static string FastRemove(this string str, IEnumerable<string> replacements)
    {
        var result = str;
        foreach (var repl in replacements)
        {
            result = result.Replace(repl, string.Empty, StringComparison.InvariantCulture);
        }
        return result;
    }
    
    public static string SlowRemove(this string str, IEnumerable<string> replacements)
    {
        var result = new StringBuilder(str);
        foreach (var repl in replacements)
        {
            result = result.Replace(repl, string.Empty);
        }
        return result.ToString();
    }
    
}