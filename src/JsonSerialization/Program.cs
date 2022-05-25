// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using JsonSerialization;
using JsonSerialization.Checksum;

Console.WriteLine("Hello, World!");

//var summary = BenchmarkRunner.Run<SerializationBenchmark>();

var person1 = new Person() { Age = 21, Name = "xD" };
var person2 = new Person() { Age = 21, Name = "xD" };


Console.WriteLine($"{ChecksumGenerator.GetChecksum(person1)} === {ChecksumGenerator.GetChecksum(person2)}");
