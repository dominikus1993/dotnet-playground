using System.Collections.Concurrent;

using MethodTimer;

using Microsoft.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace Memory;

public static class ImageSharpUtils
{
    private static readonly RecyclableMemoryStreamManager Manager = new RecyclableMemoryStreamManager()
    {
    
    };

    private static readonly PngEncoder Encoder = new()
    {
        BitDepth = PngBitDepth.Bit8,
        ColorType = PngColorType.Palette,
        Quantizer = new WuQuantizer(new QuantizerOptions { DitherScale = 0.5f }),
        CompressionLevel = PngCompressionLevel.BestCompression
    };

    [Time]
    public static void A()
    {
        Console.WriteLine("test");
    }
    
    [Time]
    public static async Task<IReadOnlyCollection<ImageSize>> OneThreadImageSave(byte[] file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new List<ImageSize>(sizes.Count);
        await using MemoryStream stream = new(file);
        stream.Seek(0, SeekOrigin.Begin);
        using var image = await Image.LoadAsync(stream);
        foreach (var size in sizes)
        {
            Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Size {size.Width}");

            var resizedImage = image.Clone(operation =>
                operation
                    .Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(size.Width, size.Height) })
            );
            await resizedImage.SaveAsPngAsync($"jp2137_{size.Height}_{size.Width}.jpg", Encoder);
            result.Add(size);
        }

        return result;
    }

    [Time]
    public static async Task<IReadOnlyCollection<ImageSize>> ParallelImageSave(byte[] file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new ConcurrentBag<ImageSize>();
        await using MemoryStream stream = new(file);
        stream.Seek(0, SeekOrigin.Begin);
        using var image = await Image.LoadAsync(stream);
        await Parallel.ForEachAsync(sizes,
            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (size, token) =>
            {
                Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Size {size.Width}");
                using var resizedImage = image.Clone(operation =>
                    operation
                        .Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(size.Width, size.Height) })
                );
                
                await resizedImage.SaveAsPngAsync($"jp2137_{size.Height}_{size.Width}.jpg", Encoder, cancellationToken: token);
                result.Add(size);
            });
        return result;
    }
}