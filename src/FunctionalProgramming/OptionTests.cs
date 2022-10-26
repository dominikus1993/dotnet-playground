using LanguageExt;
using static LanguageExt.Prelude;
namespace FunctionalProgramming;

public class OptionTests
{
    public static async Task<Option<string>> Test(string txt)
    {
        await Task.Yield();
        return txt + " super text";
    }

    public static async Task<Option<string>> Test2(string? txt)
    {
        return await Optional(txt).Where(x => !string.IsNullOrEmpty(x)).BindAsync(async t => await Test(t).ToAsync()).ToOption();
    }
}