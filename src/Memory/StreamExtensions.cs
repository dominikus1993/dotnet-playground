namespace Shops.Domain.Extensions;

public static class StreamExtensions
{

    public static byte[] ReadAsBytes(this Stream input) {
        if (input is MemoryStream stream)
        {
            return stream.ToArray();
        }
        using var ms = new MemoryStream();
        input.CopyTo(ms);
        return ms.ToArray();
    }
}