// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.Diagnostics;

using BenchmarkDotNet.Running;

using Memory;

using Microsoft.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

var timer = new Stopwatch();

var Encoder = new PngEncoder()
{
    BitDepth = PngBitDepth.Bit8,
    ColorType = PngColorType.Palette,
    Quantizer = new WuQuantizer(new QuantizerOptions { DitherScale = 0.5f }),
    CompressionLevel = PngCompressionLevel.BestCompression
};
List<Size> sizes = new() { new Size(100, 100), new Size(120, 120), new Size(130, 130), new Size(140, 140) };
timer.Start();
await using var source = File.OpenRead("./jp2137.jpg");
// using var image = await Image.LoadAsync(source);

// await Parallel.ForEachAsync(sizes, new ParallelOptions() { MaxDegreeOfParallelism = 21 }, async (size, token) =>
// {
//     Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
//     Console.WriteLine($"Size {size.Width}");
//     image.Mutate(operation =>
//         operation
//             .Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = size })
//     );
//     await image.SaveAsPngAsync($"jp2137_{size.Height}_{size.Width}.jpg", Encoder, cancellationToken: token);
// });

foreach (var size in sizes)
{
    Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine($"Size {size.Width}");
    source.Seek(0, SeekOrigin.Begin);
    using var image = await Image.LoadAsync(source);
    image.Mutate(operation =>
        operation
            .Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = size })
    );
    await image.SaveAsPngAsync($"jp2137_{size.Height}_{size.Width}.jpg", Encoder);
}

timer.Stop();

Console.WriteLine($"Elapsed: {timer.Elapsed}");