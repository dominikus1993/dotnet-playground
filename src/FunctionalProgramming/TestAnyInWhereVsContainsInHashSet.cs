using BenchmarkDotNet.Attributes;

namespace FunctionalProgramming;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[MemoryDiagnoser()]
public class TestAnyInWhereVsContainsInHashSet
{
    private List<Person> _list;
    public TestAnyInWhereVsContainsInHashSet()
    {
        _list = Enumerable.Range(0, 500).Select(x => new Person() { Id = x, Name = Guid.NewGuid().ToString()}).ToList();
    }
        
    [Benchmark]
    public List<Person> Contains()
    {
        var personsFromDb = Enumerable.Range(20, 10).Select(x => new Person() { Id = x, Name = Guid.NewGuid().ToString()}).ToList();
        var dict = personsFromDb.Select(x => x.Id).ToHashSet();

        return _list.Where(x => dict.Contains(x.Id)).ToList();
    }

    [Benchmark]
    public List<Person> ListAny()
    {
        var personsFromDb = Enumerable.Range(20, 10).Select(x => new Person() { Id = x, Name = Guid.NewGuid().ToString()}).ToList();

        return _list.Where(x => personsFromDb.Any(y => y.Id == x.Id)).ToList();
    }
}