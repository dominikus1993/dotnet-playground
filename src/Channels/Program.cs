// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;

var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(10) { FullMode = BoundedChannelFullMode.Wait, SingleReader = true, SingleWriter = true });

var t = Task.Run(async () =>
{
    foreach (var i in Enumerable.Range(0, 15))
    {
        await channel.Writer.WaitToWriteAsync();
        await channel.Writer.WriteAsync(i);
        Console.WriteLine($"Produce {i}");
    }
    channel.Writer.Complete(new Exception("some error"));
});


await foreach (var element in channel.Reader.ReadAllAsync())
{
    Console.WriteLine($"Consume {element}");
    await Task.Delay(TimeSpan.FromSeconds(1));
}

await t;

Console.WriteLine("Hello, World!");