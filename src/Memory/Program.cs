// See https://aka.ms/new-console-template for more information

using System.Buffers;

using BenchmarkDotNet.Running;

using Memory;

using Microsoft.IO;


var summary = BenchmarkRunner.Run<BenchmarkBenchmark>();