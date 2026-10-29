namespace DomainClass.Entities;

public class TableTwo
{
    public int Id { get; set; }
    public int IdTableOneForeignKeyTest { get; set; }
    public DateTime? TestData { get; set; }
    public int TestIntUnique { get; set; }

    public TableOne TableOne { get; set; } = null!;
    public List<TableThree> TableThrees { get; set; } = new();
}