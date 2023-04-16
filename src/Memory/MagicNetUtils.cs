using System.Collections.Concurrent;

using ImageMagick;

using MethodTimer;

using Shops.Domain.Extensions;

namespace Memory;

public class MagicNetUtils
{
    public static async IAsyncEnumerable<ImageSize> OneThreadImageSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        file.Seek(0, SeekOrigin.Begin);
        // Convert Stream To Array
        using var baseimage = new MagickImage(file, Settings);
        foreach (var size in sizes)
        {
            using var image = baseimage.Clone();
            image.VirtualPixelMethod = VirtualPixelMethod.Transparent;
            image.Resize(size.Width, size.Height);
            await image.WriteAsync($"374406_back_{size.Height}_{size.Width}.png", MagickFormat.Png);
            yield return size;
        }
    }

    private static readonly MagickReadSettings Settings = new MagickReadSettings()
    {
        BackgroundColor = MagickColors.Transparent,
        Format = MagickFormat.Png,
        Depth = 8,
        FillColor = MagickColors.None,
        ColorSpace = ColorSpace.Transparent,
    };
    
    [Time]
    public static async Task<IReadOnlyCollection<ImageSize>> ParallelImageCloneSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        file.Seek(0, SeekOrigin.Begin);
        var result = new ConcurrentBag<ImageSize>();
        using var baseImage = new MagickImage(file, Settings);
        await Parallel.ForEachAsync(sizes,
            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (size, token) =>
            {
                using var image = baseImage.Clone();
                image.VirtualPixelMethod = VirtualPixelMethod.Transparent;
                image.Resize(size.Width, size.Height);
                await image.WriteAsync($"374406_back_{image.Height}_{image.Width}.png", MagickFormat.Png, token);
                result.Add(size);
            });
        return result;
    }
    
    [Time]
    public static async Task<IReadOnlyCollection<ImageSize>> ParallelImageSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        file.Seek(0, SeekOrigin.Begin);
        var result = new ConcurrentBag<ImageSize>();
        var bytes = file.ReadAsBytes();
        await Parallel.ForEachAsync(sizes,
            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (size, token) =>
            {
                using var image = new MagickImage(bytes, Settings);
                image.VirtualPixelMethod = VirtualPixelMethod.Transparent;
                image.Resize(size.Width, size.Height);
                await image.WriteAsync($"374406_back_{image.Height}_{image.Width}.png", MagickFormat.Png, token);
                result.Add(size);
            });
        return result;
    }
}