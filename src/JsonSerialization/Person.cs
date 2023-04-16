using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonSerialization;

[JsonConverter(typeof(NameStringJsonConverter))]
public readonly record struct Name(string Value);

public class NameStringJsonConverter : JsonConverter<Name>
{
    public override Name Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Name should be string");
        }

        var value = reader.GetString();

        return new Name(value!);
    }

    public override void Write(Utf8JsonWriter writer, Name value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}

[JsonSerializable(typeof(Person))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class BenchmarkJsonContext : JsonSerializerContext
{
}

public class PersonDeserializer
{
    public static async Task<Person?> Deserialize(Stream per)
    {
        await using var _ = per;
        using var personD = await JsonDocument.ParseAsync(per);
        return personD.Deserialize<Person>();
    }
}

public class Tag
{
    public string Value { get; set; }

    public override string ToString()
    {
        return $"{nameof(Value)}: {Value}";
    }
}
public class Person
{
    public Name Name { get; set; }
    public int Age { get; set; }
    public IReadOnlyList<Tag> Tags { get; set; }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}, {nameof(Tags)}: {string.Join(",", Tags)}";
    }
}