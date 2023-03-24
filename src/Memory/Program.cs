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


BenchmarkRunner.Run<StringBenchmark>();

