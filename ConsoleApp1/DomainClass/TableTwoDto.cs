namespace DomainClass;

public class TableTwoDto
{
    public int Id { get; set; }
    public int IdTableOneForeignKeyTest { get; set; }
    public DateTime? TestData { get; set; }
    public int TestIntUnique { get; set; }
}