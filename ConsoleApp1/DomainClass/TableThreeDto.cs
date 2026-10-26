namespace DomainClass;

public class TableThreeDto
{
    public int Id { get; set; }
    public int IdTableOneForeignKeyTest { get; set; }
    public int IdTableTwoForeignKeyTest { get; set; }
    public string TestStringConstraint { get; set; }
}