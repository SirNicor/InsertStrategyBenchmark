namespace DomainClass;

public class AllDataDto
{
    public string? TableOneId { get; set; }
    public string? TableTwoId { get; set; }
    public string? TableThreeId { get; set; }
    public string? TableOneTestString { get; set; }
    public string? TableOneTestIntWithoutConstraint { get; set; }
    public string? TableOneTestIntWithConstraint { get; set; }
    public string? TableTwoIdTableOneForeignKeyTest { get; set; }
    public string? TableTwoTestData { get; set; }
    public string? TableTwoTestIntUnique { get; set; }
    public string? TableThreeIdTableOneForeignKeyTest { get; set; }
    public string? TableThreeIdTableTwoForeignKeyTest { get; set; }
    public string? TableThreeTestStringConstraint { get; set; }
    public string? ErrorMessage { get; set; }
    public int IdStatus { get; set; }
}   