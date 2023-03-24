using System.Collections.Concurrent;

using ImageMagick;

using MethodTimer;

using Shops.Domain.Extensions;

namespace Memory;

public class MagicNetUtils
{
    public static async Task<IReadOnlyCollection<ImageSize>> OneThreadImageSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new List<ImageSize>(sizes.Count);

        foreach (var size in sizes)
        {
            file.Seek(0, SeekOrigin.Begin);

            using var image = new MagickImage(file, new MagickReadSettings()
            {
                BackgroundColor = MagickColors.Transparent,
                Format = MagickFormat.Png,
                Depth = 8,
                FillColor = MagickColors.None,
                ColorSpace = ColorSpace.Transparent,
            });
            image.VirtualPixelMethod = VirtualPixelMethod.Transparent;
            image.Resize(size.Width, size.Height);
            await image.WriteAsync($"374406_back_{size.Height}_{size.Width}.png", MagickFormat.Png);
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
        var img = file.ReadAsBytes();
        await Parallel.ForEachAsync(sizes,
            new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (size, token) =>
            {
                using var image = new MagickImage(img, new MagickReadSettings()
                {
                    BackgroundColor = MagickColors.Transparent,
                    Format = MagickFormat.Png,
                    Depth = 8,
                    FillColor = MagickColors.None,
                    ColorSpace = ColorSpace.Transparent,
                });
                image.VirtualPixelMethod = VirtualPixelMethod.Transparent;
                image.Resize(size.Width, size.Height);
                await image.WriteAsync($"374406_back_{size.Height}_{size.Width}.png", MagickFormat.Png, token);
                result.Add(size);
            });
        return result;
    }
}