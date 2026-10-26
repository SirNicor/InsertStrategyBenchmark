using FluentMigrator;
using Microsoft.Extensions.Configuration;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace DomainClass;

public class MigrationAllForTest(string connectionString)
{
    public void Start()
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()          
                .WithGlobalConnectionString(connectionString)   
                .ScanIn(typeof(InitialMigrationAllForTest).Assembly).For.Migrations())
            .BuildServiceProvider(false);
        var scope = serviceProvider.CreateAsyncScope();     
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}

[Migration(1, "Initial migration: create test tables for benchmark")]
public class InitialMigrationAllForTest : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE TABLE TableOne(
                Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_TableOne_Id PRIMARY KEY,
                TestString NVARCHAR(50) NOT NULL,
                TestIntWithoutConstraint INT NULL,
                TestIntWithConstraint INT NOT NULL 
                    CONSTRAINT UQ_TableOne_TestIntWithConstraint UNIQUE 
                    CONSTRAINT CK_TableOne_TestIntWithConstraint CHECK (TestIntWithConstraint >= 0)
            );

            CREATE TABLE TableTwo(
                Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_TableTwo_Id PRIMARY KEY,
                IdTableOneForeignKeyTest INT NOT NULL 
                    CONSTRAINT FK_TableTwo_IdTableOne FOREIGN KEY (IdTableOneForeignKeyTest) 
                    REFERENCES TableOne(Id),
                TestData DATE NULL,
                TestIntUnique INT NOT NULL 
                    CONSTRAINT UQ_TableTwo_TestIntUnique UNIQUE
            );

            CREATE NONCLUSTERED INDEX IX_TableTwo_IdTableOneForeignKeyTest 
            ON TableTwo(IdTableOneForeignKeyTest);

            CREATE TABLE TableThree(
                Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_TableThree_Id PRIMARY KEY,
                IdTableOneForeignKeyTest INT NOT NULL 
                    CONSTRAINT FK_TableThree_IdTableOne FOREIGN KEY (IdTableOneForeignKeyTest) 
                    REFERENCES TableOne(Id),
                IdTableTwoForeignKeyTest INT NOT NULL 
                    CONSTRAINT FK_TableThree_IdTableTwo FOREIGN KEY (IdTableTwoForeignKeyTest) 
                    REFERENCES TableTwo(Id),
                TestStringConstraint NVARCHAR(10) NOT NULL 
                    CONSTRAINT CK_TableThree_TestStringConstraint CHECK (LEN(TestStringConstraint) >= 3)
            );

            CREATE NONCLUSTERED INDEX IX_TableThree_IdTableOneForeignKeyTest 
            ON TableThree(IdTableOneForeignKeyTest);

            CREATE NONCLUSTERED INDEX IX_TableThree_IdTableTwoForeignKeyTest 
            ON TableThree(IdTableTwoForeignKeyTest);

            CREATE TABLE Status(
                Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Status_Id PRIMARY KEY,
                Status NVARCHAR(10) NOT NULL
            );

            CREATE TABLE SubTable(
                TableOneId NVARCHAR(MAX) NULL,
                TableTwoId NVARCHAR(MAX) NULL,
                TableThreeId NVARCHAR(MAX) NULL,
                TableOneTestString NVARCHAR(MAX) NULL,
                TableOneTestIntWithoutConstraint NVARCHAR(MAX) NULL,
                TableOneTestIntWithConstraint NVARCHAR(MAX) NULL,
                TableTwoIdTableOneForeignKeyTest NVARCHAR(MAX) NULL,
                TableTwoTestData NVARCHAR(MAX) NULL,
                TableTwoTestIntUnique NVARCHAR(MAX) NULL,
                TableThreeIdTableOneForeignKeyTest NVARCHAR(MAX) NULL,
                TableThreeIdTableTwoForeignKeyTest NVARCHAR(MAX) NULL,
                TableThreeTestStringConstraint NVARCHAR(MAX) NULL,
                ErrorMessage NVARCHAR(MAX) NULL,
                IdStatus INT NOT NULL 
                    CONSTRAINT FK_SubTable_Status FOREIGN KEY (IdStatus) 
                    REFERENCES Status(Id)
            );
        ");

        Insert.IntoTable("Status").Row(new { Status = "New" })
                                  .Row(new { Status = "Updated" })
                                  .Row(new { Status = "Inserted" })
                                  .Row(new { Status = "Error" });
    }

    public override void Down()
    {
        Execute.Sql(@"
            DROP TABLE IF EXISTS SubTable;
            DROP TABLE IF EXISTS TableThree;
            DROP TABLE IF EXISTS TableTwo;
            DROP TABLE IF EXISTS TableOne;
            DROP TABLE IF EXISTS Status;
        ");
    }
}