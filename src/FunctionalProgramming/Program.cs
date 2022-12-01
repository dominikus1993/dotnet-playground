// See https://aka.ms/new-console-template for more information

using FunctionalProgramming;

using LanguageExt;

using static LanguageExt.Prelude;

var names = new[] { "SomeFlag" };

var res = TestHelper.GetStatuses(names);

Option<string> txt = await OptionTests.Test2(null);

var result = B();

switch (result.Case)
{
    case Exception exception:
        Console.WriteLine("Blad");
        break;
    default:
        Console.WriteLine("xDDDD");
        break;
}

Either<Exception, Unit> B()
{
    try
    {
        return Right(Unit.Default);
    }
    catch (Exception e)
    {
        return Left<Exception, Unit>(new MyTestException("xDDD"));
    }
}

IEnumerable<int> A()
{
    foreach (var value in Enumerable.Range(1, 10))
    {
        if (value % 2 == 0)
        {
            yield return value;
            continue;
        }

        yield return value * 2;
    }
}

class MyTestException : Exception
{
    public MyTestException(string? message) : base(message)
    {
    }

    public MyTestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}