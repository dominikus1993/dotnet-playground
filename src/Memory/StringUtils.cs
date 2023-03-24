using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Memory;

public static class StringUtils
{
    public static string FastReplace(this string str, Dictionary<char, char> replacements)
    {
        Span<char> textChars = stackalloc char[str.Length];
        str.CopyTo(textChars);
        for (int i = 0; i < textChars.Length; i++)
        {
            var element = textChars[i];
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
    
}