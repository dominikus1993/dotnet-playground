// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using JsonSerialization;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<SerializationBenchmark>();