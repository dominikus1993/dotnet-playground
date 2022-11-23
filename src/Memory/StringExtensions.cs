using System.Text.RegularExpressions;

namespace Shops.Domain.Extensions
{
    
    public static class SlowStringExtensions
    {
        private static string ReplacePolishLetters(string text)
        {
            var textCopy = new string(text);
            var textChars = textCopy.ToArray();
            var polishLetters = new Dictionary<char, char>
            {
                {'ą', 'a'},
                {'ć', 'c'},
                {'ę', 'e'},
                {'ł', 'l'},
                {'ń', 'n'},
                {'ó', 'o'},
                {'ś', 's'},
                {'ż', 'z'},
                {'ź', 'z'}
            };

            for (var i = 0; i < textChars.Length; i++)
            {
                if (polishLetters.ContainsKey(textChars[i]))
                    textChars[i] = polishLetters[textChars[i]];
            }

            return new string(textChars);
        }

        public static string SanitizeToUrl(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            var regexp = new Regex("[^a-z|0-9]");
            var multipleDashRegex = new Regex("[-]{2,}");

            var lowerText = ReplacePolishLetters(text.Trim().ToLower());

            return multipleDashRegex.Replace(regexp.Replace(lowerText, "-"),"-");
        }
    }
    
    public static class StringExtensions
    {
        private static readonly Regex Regexp = new("[^a-z|0-9]", RegexOptions.Compiled);
        private static readonly Regex MultipleDashRegex = new("[-]{2,}", RegexOptions.Compiled);
        private static readonly IReadOnlyDictionary<char, char> PolishLetters = new Dictionary<char, char>
        {
            {'ą', 'a'},
            {'ć', 'c'},
            {'ę', 'e'},
            {'ł', 'l'},
            {'ń', 'n'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ż', 'z'},
            {'ź', 'z'},
        };
        private static string ReplacePolishLetters(string text)
        {
            var textChars = text.ToCharArray();
            for (var i = 0; i < textChars.Length; i++)
            {
                char character = textChars[i];
                if (PolishLetters.ContainsKey(character))
                    textChars[i] = PolishLetters[character];
            }

            return new string(textChars);
        }

        public static string SanitizeToUrl(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            var lowerText = ReplacePolishLetters(text.Trim().ToLower());

            return MultipleDashRegex.Replace(Regexp.Replace(lowerText, "-"),"-");
        }
    }
    
    public static class StringSpanExtensions
    {
        private static readonly Regex Regexp = new("[^a-z|0-9]", RegexOptions.Compiled);
        private static readonly Regex MultipleDashRegex = new("[-]{2,}", RegexOptions.Compiled);
        private static readonly IReadOnlyDictionary<char, char> PolishLetters = new Dictionary<char, char>
        {
            {'ą', 'a'},
            {'ć', 'c'},
            {'ę', 'e'},
            {'ł', 'l'},
            {'ń', 'n'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ż', 'z'},
            {'ź', 'z'},
        };
        private static string ReplacePolishLetters(string text)
        {
            Span<char> textChars = stackalloc char[text.Length];
            text.AsSpan().CopyTo(textChars);
            for (var i = 0; i < textChars.Length; i++)
            {
                char character = textChars[i];
                if (PolishLetters.ContainsKey(character))
                    textChars[i] = PolishLetters[character];
            }

            return new string(textChars);
        }

        public static string SanitizeToUrl(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            var lowerText = ReplacePolishLetters(text.Trim().ToLower());

            return MultipleDashRegex.Replace(Regexp.Replace(lowerText, "-"),"-");
        }
    }
    
    public sealed partial class StringSpanRegexExtensions
    {
        [GeneratedRegex("[^a-z|0-9]", RegexOptions.Compiled)]
        private static partial Regex Regexp();

        [GeneratedRegex("[-]{2,}", RegexOptions.Compiled)]
        private static partial Regex MultipleDashRegex();
        private static readonly IReadOnlyDictionary<char, char> PolishLetters = new Dictionary<char, char>
        {
            {'ą', 'a'},
            {'ć', 'c'},
            {'ę', 'e'},
            {'ł', 'l'},
            {'ń', 'n'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ż', 'z'},
            {'ź', 'z'},
        };
        private static string ReplacePolishLetters(string text)
        {
            Span<char> textChars = stackalloc char[text.Length];
            text.AsSpan().CopyTo(textChars);
            for (var i = 0; i < textChars.Length; i++)
            {
                char character = textChars[i];
                if (PolishLetters.ContainsKey(character))
                    textChars[i] = PolishLetters[character];
            }

            return new string(textChars);
        }

        public static string SanitizeToUrl(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            var lowerText = ReplacePolishLetters(text.Trim().ToLower()).Trim();

            return MultipleDashRegex().Replace(Regexp().Replace(lowerText, "-"),"-");
        }
        
        public static int GetProductIdFromRedisKey(ReadOnlySpan<char> key)
        {
            var pointIndex = key.IndexOf('.');
            if (pointIndex != -1)
            {
                var result = key.Slice(pointIndex + 1);
                return int.Parse(result);
            }

            throw new Exception("xDDD");
        }
        
        public static int GetProductIdFromRedisKey(string key)
        {
            return GetProductIdFromRedisKey(key.AsSpan());
        }
        
        public static int GetProductIdFromRedisKeyWithoutSpan(string key)
        {
            var splited = key.Split('.');
            if (splited.Length >= 2 && int.TryParse(splited[1], out var result))
            {
                return result;
            }

            throw new Exception("xDDD");
        }
    }
}