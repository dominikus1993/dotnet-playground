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

// var sizes = new ImageSize[] { new ImageSize(71, 55), new ImageSize(190, 338), new ImageSize(350, 360), new ImageSize(720, 1280) };
// await using var file = File.OpenRead("./374406_back.png");
//
// var res = await MagicNetUtils.ParallelImageSave(file, sizes);
//
//
// foreach (var a in res)
// {
//     Console.WriteLine(a);
// }
//
//

BenchmarkDotNet.Running.BenchmarkRunner.Run<BenchmarkBenchmark>();