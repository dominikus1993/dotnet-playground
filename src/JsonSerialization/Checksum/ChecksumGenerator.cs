using System.Security.Cryptography;
using System.Text.Json;

namespace JsonSerialization.Checksum
{
    internal static class ChecksumGenerator
    {
        public static string GetChecksum(Person person)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(person, BenchmarkJsonContext.Default.Person);
            return Convert.ToHexString(MD5.HashData(json));
        }
    }
}
