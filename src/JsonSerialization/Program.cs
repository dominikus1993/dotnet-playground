// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using JsonSerialization;
using JsonSerialization.Checksum;

using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;


Console.WriteLine("Hello, World!");

// var summary = BenchmarkRunner.Run<SerializationBenchmark>();

var person = new Person() { Age = 1, Name = new Name("Jan Pawel"), Tags = new[] { new Tag() { Value = "A" }, new Tag() { Value = "Dupa"} } };

var json = JsonSerializer.SerializeToUtf8Bytes(person);

using var stream = new MemoryStream(json);
var personDes = await PersonDeserializer.Deserialize(stream);

Console.WriteLine(personDes);
