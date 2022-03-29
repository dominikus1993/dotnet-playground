// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using Logging;

using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<LoggingBenchmark>();