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
    private IReadOnlyCollection<ImageSize> Sizes = new ImageSize[] { new ImageSize(71, 55), new ImageSize(190, 338), new ImageSize(350, 360), new ImageSize(720, 1280) };

    private Stream FileStream;

    public BenchmarkBenchmark()
    {
        FileStream = File.OpenRead("./374406_back.png");
    }
    [Benchmark]
    public async Task<IReadOnlyCollection<ImageSize>> ImageSharpOneThread()
    {
        return await ImageSharpUtils.OneThreadImageSave(FileStream, Sizes);
    }
    
    [Benchmark]
    public async Task<IReadOnlyCollection<ImageSize>> magicNetOneThread()
    {
        return await MagicNetUtils.OneThreadImageSave(FileStream, Sizes);
    }
    
    [Benchmark]
    public async Task<IReadOnlyCollection<ImageSize>>ImageSharpMultiThread()
    {
        return await ImageSharpUtils.ParallelImageSave(FileStream, Sizes);
    }
    
    
    [Benchmark]
    public async Task<IReadOnlyCollection<ImageSize>>MagicNetMultiThread()
    {
        return await MagicNetUtils.ParallelImageSave(FileStream, Sizes);
    }
}