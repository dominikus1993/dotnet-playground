using ImageMagick;

namespace Memory;

public class MagicNetUtils
{
    public static async Task<IReadOnlyCollection<ImageSize>> OneThreadImageSave(byte[] file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new List<ImageSize>(sizes.Count);
        await using MemoryStream stream = new(file);
        
        foreach (var size in sizes)
        {
            stream.Seek(0, SeekOrigin.Begin);
            Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Size {size.Width}");

            using var image = new MagickImage(stream, new MagickReadSettings()
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
}