namespace DomainClass.Entities;

public class TableThree
{
    public int Id { get; set; }
    public int IdTableOneForeignKeyTest { get; set; }
    public int IdTableTwoForeignKeyTest { get; set; }
    public string TestStringConstraint { get; set; } = null!;

    public TableOne TableOne { get; set; } = null!;
    public TableTwo TableTwo { get; set; } = null!;
}