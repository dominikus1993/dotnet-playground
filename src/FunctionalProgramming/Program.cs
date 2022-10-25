// See https://aka.ms/new-console-template for more information

using FunctionalProgramming;

using LanguageExt;

using static LanguageExt.Prelude;

var names = new[] { "SomeFlag" };

var res = TestHelper.GetStatuses(names);

Option<string> txt = Optional<string>(null).Where(x => x.Length > 0);

Console.WriteLine($"Hello, World! {res.Contains(Test.SomeFlag)} {txt}");