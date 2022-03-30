// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using LinqTest;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<MultipleEnumerationsBenchmark>();