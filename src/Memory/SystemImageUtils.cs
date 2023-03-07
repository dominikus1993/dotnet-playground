using System.Collections.Concurrent;

using MethodTimer;
using System.Drawing;
using System.Drawing.Imaging;

using Microsoft.IO;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;

using ResizeMode = Microsoft.Maui.Graphics.ResizeMode;

namespace Memory;

public static class SystemImage
{
    private static readonly RecyclableMemoryStreamManager Manager = new RecyclableMemoryStreamManager()
    {
    
    };
    

    [Time]
    public static void A()
    {
        Console.WriteLine("test");
    }
    
    [Time]
    public static async Task<IReadOnlyCollection<ImageSize>> OneThreadImageSave(byte[] file, IReadOnlyCollection<ImageSize> sizes)
    {
        // Convert Stream To Array
        var result = new List<ImageSize>(sizes.Count);
        await using MemoryStream stream = new(file);
        stream.Seek(0, SeekOrigin.Begin);
        using var image = PlatformImage.FromStream(stream, ImageFormat.Png);
        foreach (var size in sizes)
        {
            Console.WriteLine($"Therad id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Size {size}");
            using var newImage = image.Resize(size.Width, size.Height, ResizeMode.Fit, false);

            await using MemoryStream memStream = new();
            await newImage.SaveAsync(memStream, ImageFormat.Png);
            
            await File.WriteAllBytesAsync($"jp2137_{size.Height}_{size.Width}.jpg", memStream.ToArray());
            result.Add(size);
        }

        return result;
    }
}