using System.Collections.Concurrent;

using MethodTimer;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace Memory;

public static class ImageSharpUtils
{
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
    public static async Task<IReadOnlyCollection<Size>> OneThreadImageSave(byte[] file, IReadOnlyCollection<Size> sizes)
    {
        // Convert Stream To Array
        var result = new List<Size>(sizes.Count);
        await using MemoryStream stream = new(file);
        foreach (var size in sizes)
        {
            Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Size {size.Width}");
            stream.Seek(0, SeekOrigin.Begin);
            using var image = await Image.LoadAsync(stream);

            image.Mutate(operation =>
                operation
                    .Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = size })
            );
            await image.SaveAsPngAsync($"jp2137_{size.Height}_{size.Width}.jpg", Encoder);
            result.Add(size);
        }

        return result;
    }

    [Time]
    public static async Task<IReadOnlyCollection<Size>> ParallelImageSave(byte[] file, IReadOnlyCollection<Size> sizes)
    {
        // Convert Stream To Array
        var result = new ConcurrentBag<Size>();
        await Parallel.ForEachAsync(sizes,
            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (size, token) =>
            {
                Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Size {size.Width}");
                await using MemoryStream stream = new(file);
                using var image = await Image.LoadAsync(stream, token);

                image.Mutate(operation =>
                    operation
                        .Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = size })
                );
                await image.SaveAsPngAsync($"jp2137_{size.Height}_{size.Width}.jpg", Encoder, cancellationToken: token);
                result.Add(size);
            });
        return result;
    }
}