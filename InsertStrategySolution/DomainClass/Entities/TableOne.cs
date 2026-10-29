namespace DomainClass.Entities;

public class TableOne
{
    public int Id { get; set; }
    public string TestString { get; set; } = null!;
    public int? TestIntWithoutConstraint { get; set; }
    public int TestIntWithConstraint { get; set; }

    public List<TableTwo> TableTwos { get; set; } = new();
    public List<TableThree> TableThrees { get; set; } = new();
}