// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using LinqTest;
using static LanguageExt.Prelude;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<DistinctRecords>();
