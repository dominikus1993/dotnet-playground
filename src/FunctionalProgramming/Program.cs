// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using FunctionalProgramming;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<TestAnyInWhereVsContainsInHashSet>();