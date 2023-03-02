// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;

using BenchmarkDotNet.Running;

using Memory;

using MethodTimer;

using Microsoft.IO;

using Shops.Domain.Extensions;

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
var file =  source.ReadAsBytes();

var results = await ImageSharpUtils.OneThreadImageSave(file, sizes).ToListAsync();


Console.WriteLine($"Koniec");

foreach (var size in results)
{
    Console.WriteLine($"Size: {size.Width}, {size.Height}");
}

ImageSharpUtils.A();