﻿using System.Collections.Concurrent;

using MethodTimer;

using Microsoft.IO;

using PhotoSauce.MagicScaler;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace Memory;

public static class MagicScalerUtils
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
    public static IReadOnlyCollection<ImageSize> OneThreadImageSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new List<ImageSize>(sizes.Count);
        
        foreach (var size in sizes)
        {
            file.Seek(0, SeekOrigin.Begin);
            var settings =
                new ProcessImageSettings() { Width = size.Width, Height = size.Height };
            settings.TrySetEncoderFormat("image/png");
            MagicImageProcessor.ProcessImage(file, $"374406_back_{size.Height}_{size.Width}.png",  settings);
            result.Add(size);
        }

        return result;
    }

    [Time]
    public static async Task<IReadOnlyCollection<ImageSize>> ParallelImageSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new ConcurrentBag<ImageSize>();
        file.Seek(0, SeekOrigin.Begin);
        using var image = await Image.LoadAsync(file);
        await Parallel.ForEachAsync(sizes,
            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (size, token) =>
            {
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