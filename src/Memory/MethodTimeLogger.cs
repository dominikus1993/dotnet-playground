using System.Reflection;

namespace Memory;

public class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
    {
        Console.WriteLine($"{methodBase.Name} End, Elapsed: {timeSpan}, Message: {message}");
    }
}