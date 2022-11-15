// See https://aka.ms/new-console-template for more information

using System.Net;

var httpClientHandler = new SocketsHttpHandler()
{
    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
    PooledConnectionLifetime = TimeSpan.FromSeconds(6),
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
};

var client = new HttpClient(httpClientHandler);

            
for (var i = 0; i < 50; i++)
{
    var resp = await client.GetAsync("https://www.google.com");
    Console.WriteLine($"Sent: {resp.StatusCode}");
    await Task.Delay(TimeSpan.FromSeconds(2));
}

Console.WriteLine("Press a key to exit...");
Console.ReadKey();