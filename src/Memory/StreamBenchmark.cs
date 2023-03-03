using System.Buffers;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Memory;


[MemoryDiagnoser]
public class BenchmarkBenchmark
{
    private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager()
    {
        ThrowExceptionOnToArray = true,
    };

    private Stream FileStream;

    public BenchmarkBenchmark()
    {
        FileStream = File.OpenRead("./jp2137.jpg");
    }
    [Benchmark]
    public async Task<byte[]> MemoryStream()
    {
        await using var memory = new MemoryStream();
        await FileStream.CopyToAsync(memory!);
        return memory.ToArray();
    }

    [Benchmark]
    public async Task<byte[]> RecyclableMemoryStreamManager()
    {
        await using var memory = manager.GetStream() as RecyclableMemoryStream;
        await FileStream.CopyToAsync(memory!);
        return memory.ToArray();
    }
}