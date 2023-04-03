using MethodTimer;

using PhotoSauce.MagicScaler;

using Shops.Domain.Extensions;

using SkiaSharp;

namespace Memory;

public class SkiaSharpUtils
{
    [Time]
    public static IReadOnlyCollection<ImageSize> OneThreadImageSave(Stream file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        file.Seek(0, SeekOrigin.Begin);
        file.Position = 0;
        var result = new List<ImageSize>(sizes.Count);
        foreach (var size in sizes)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Position = 0;
            file.Position = 0;
            using var bitmap = SKBitmap.Decode(ms);
            using SKBitmap scaledBitmap = bitmap.Resize(new SKImageInfo(size.Width, size.Height), SKFilterQuality.Medium);
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
            using SKData data = scaledImage.Encode();
            using var res = File.Create($"374406_back_{size.Height}_{size.Width}.png");
            data.SaveTo(res);
            result.Add(size);
        }

        return result;
    }
}