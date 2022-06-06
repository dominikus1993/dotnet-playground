// See https://aka.ms/new-console-template for more information
using System.Text.Json;

using JsonSerialization;
using JsonSerialization.Checksum;


Console.WriteLine("Hello, World!");

//var summary = BenchmarkRunner.Run<SerializationBenchmark>();

var person1 = new Person() { Age = 21, Name = "xD" };
var person2 = new Person() { Age = 21, Name = "xD" };


Console.WriteLine($"{ChecksumGenerator.GetChecksum(person1)} === {ChecksumGenerator.GetChecksum(person2)}");

var persons = new[] { new Person { Age = 2, Name = "xD" } };

var jsonB = JsonSerializer.SerializeToUtf8Bytes(persons);

await File.WriteAllBytesAsync("./tst", jsonB);


using var jsonS = new FileStream("./tst", FileMode.Open);
var personsFomFile = JsonSerializer.DeserializeAsyncEnumerable<Person>(jsonS);

await foreach (var item in personsFomFile)
{
    Console.WriteLine($"{item.Name} - {item.Age}");
}