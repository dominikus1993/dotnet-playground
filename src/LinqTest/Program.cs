// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using LinqTest;
using static LanguageExt.Prelude;

Console.WriteLine("Hello, World!");

//var summary = BenchmarkRunner.Run<MultipleEnumerationsBenchmark>();

var a = Some(1);


switch (a.Case)
{
    case int b:
        Console.WriteLine($"xDDD {b}");
        break;
}

var e = Right<Exception, IReadOnlyList<int>>(new List<int>() { 1 });

switch (e.Case)
{
    case IReadOnlyList<int> l:
        Console.WriteLine($"spierdalaj {l.Count}");
        break;
    case Exception exc:
        Console.WriteLine("wypierdalaj");
        break;
}

Console.WriteLine($"A: {ImportSteps.Categories.ToString()}");

enum ImportSteps
{
    Synchronize = 1,
    Products = 5,
    Categories = 6,
    Solr = 8
}