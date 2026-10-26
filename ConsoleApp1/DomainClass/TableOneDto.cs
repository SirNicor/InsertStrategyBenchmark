namespace DomainClass;

public class TableOneDto
{
    public int Id { get; set; }
    public string TestString { get; set; }
    public int? TestIntWithoutConstraint { get; set; }
    public int TestIntWithConstraint { get; set; }
}