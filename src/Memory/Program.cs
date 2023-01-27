// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;

using BenchmarkDotNet.Running;

using Memory;

using MethodTimer;

using Microsoft.IO;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;


var Encoder = new PngEncoder()
{
    BitDepth = PngBitDepth.Bit8,
    ColorType = PngColorType.Palette,
    Quantizer = new WuQuantizer(new QuantizerOptions { DitherScale = 0.5f }),
    CompressionLevel = PngCompressionLevel.BestCompression
};
List<Size> sizes = new() { new Size(71, 55), new Size(190, 338), new Size(350, 360), new Size(720, 1280) }; ;
await using var source = File.OpenRead("./jp2137.jpg");
await using var memoryStream = new MemoryStream();

// Use the .CopyTo() method and write current filestream to memory stream
await source.CopyToAsync(memoryStream);

var file = memoryStream.ToArray();

var results = await ImageSharpUtils.ParallelImageSave(file, sizes);


Console.WriteLine($"Koniec");

foreach (var size in results)
{
    Console.WriteLine($"Size: {size.Width}, {size.Height}");
}

ImageSharpUtils.A();