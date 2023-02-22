// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using JsonSerialization;
using JsonSerialization.Checksum;

using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;


Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<SerializationBenchmark>();
