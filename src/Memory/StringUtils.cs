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
        for (int i = 0; i < textChars.Length; i++)
        {
            char element = textChars[i];
            ref char replacement = ref CollectionsMarshal.GetValueRefOrNullRef(replacements, element);
            if (!Unsafe.IsNullRef(ref replacement))
            {
                textChars[i] = replacement;
            }
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
    
    public static string RegexRemove(this Regex regex, string str)
    {
        return regex.Replace(str, string.Empty);
    }
    
    public static string RegexReplace(this Regex regex, string str,Dictionary<string, string> replacements)
    {
        return regex.Replace(str, match =>
        {
            ref string replacement = ref CollectionsMarshal.GetValueRefOrNullRef(replacements, match.Value);
            if (!Unsafe.IsNullRef(ref replacement))
            {
                return replacement;
            }
            return match.Value;
        });
    }
    
    public static string SlowRemove(this string str, string[] replacements)
    {
        var result = new StringBuilder(str);
        foreach (var repl in replacements)
        {
            result = result.Replace(repl, string.Empty);
        }
        return result.ToString();
    }
    
}