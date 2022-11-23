// See https://aka.ms/new-console-template for more information

using FunctionalProgramming;

using LanguageExt;

using static LanguageExt.Prelude;

var names = new[] { "SomeFlag" };

var res = TestHelper.GetStatuses(names);

Option<string> txt = await OptionTests.Test2(null);

Console.WriteLine($"Hello, World! {res.Contains(Test.SomeFlag)} {txt}");

var res2 = A().ToArray();

foreach (var value in res2)
{
    Console.WriteLine(value);
}

IEnumerable<int> A()
{
    foreach (var value in Enumerable.Range(1, 10))
    {
        if (value % 2 == 0)
        {
            yield return value;
            continue;
        }

        yield return value * 2;
    }
}