// See https://aka.ms/new-console-template for more information

using Microsoft.IO;

Console.WriteLine("Hello, World!");

RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager()
{
    
};

for (int i = 0; i < 10; i++)
{
    await using var stream = File.OpenRead("./jp2137.jpg");
    using var memory = manager.GetStream();
    await stream.CopyToAsync(memory);
    var byteA = memory.ToArray();
    Console.WriteLine(byteA.Length);
}

Console.WriteLine(manager.BlockSize);