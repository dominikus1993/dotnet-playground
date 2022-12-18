// See https://aka.ms/new-console-template for more information

using JsonSerialization;
using JsonSerialization.Checksum;

using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;


Console.WriteLine("Hello, World!");

//var summary = BenchmarkRunner.Run<SerializationBenchmark>();

var person1 = new Person() { Age = 21, Name = new Name("xD") };
var person2 = new Person() { Age = 21, Name = new Name("xD")};


Console.WriteLine($"{ChecksumGenerator.GetChecksum(person1)} === {ChecksumGenerator.GetChecksum(person2)}");

var persons = new[] { new Person { Age = 2, Name = new Name("xD"), Tags = new []{new Tag() { Value = "xD"}}} };

var jsonB = JsonSerializer.SerializeToUtf8Bytes(persons);

await File.WriteAllBytesAsync("./tst", jsonB);


await using var jsonS = new FileStream("./tst", FileMode.Open);
var personsFomFile = JsonSerializer.DeserializeAsyncEnumerable<Person>(jsonS);

await foreach (var item in personsFomFile)
{
    Console.WriteLine($"{item.Name} - {item.Age}");
}

var json = JsonSerializer.Serialize(persons);
Console.WriteLine(json);

Console.WriteLine(JsonSerializer.Deserialize<Person[]>(json)[0].Tags[0].Value);
