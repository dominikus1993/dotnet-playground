namespace FunctionalProgramming;

public enum Test
{
    SomeFlag,
    AnotherFlag
}

public class TestHelper
{
    public static IReadOnlySet<Test> GetStatuses(IEnumerable<string> names)
    {
        var set = new HashSet<Test>();
        foreach (var attribute in names)
        {
            if (Enum.TryParse(attribute, true, out Test status))
            {
                set.Add(status);
            }
        }
        return set;
    }
}