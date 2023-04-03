// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using FunctionalProgramming;

using Polly;

// Console.WriteLine("Hello, World!");
//
// var summary = BenchmarkRunner.Run<TestAnyInWhereVsContainsInHashSet>();


await Polly.Policy
    .Handle<Exception>()
    .WaitAndRetryAsync(1, static retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt * 2)))
    .ExecuteAsync(() =>
    {
        throw new Exception("spierdalaj");
    });
    
    