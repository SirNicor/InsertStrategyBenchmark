using DomainClass.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainClass;

public class BenchmarkDbContext : DbContext
{
    public BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options)
        : base(options)
    {
    }

    public DbSet<TableOne> TableOnes => Set<TableOne>();
    public DbSet<TableTwo> TableTwos => Set<TableTwo>();
    public DbSet<TableThree> TableThrees => Set<TableThree>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<SubTable> SubTables => Set<SubTable>();

    public static BenchmarkDbContext Create(string connectionString)
        => new(new DbContextOptionsBuilder<BenchmarkDbContext>()
            .UseSqlServer(connectionString)
            .Options);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTableOne(modelBuilder.Entity<TableOne>());
        ConfigureTableTwo(modelBuilder.Entity<TableTwo>());
        ConfigureTableThree(modelBuilder.Entity<TableThree>());
        ConfigureStatus(modelBuilder.Entity<Status>());
        ConfigureSubTable(modelBuilder.Entity<SubTable>());
    }

    private static void ConfigureTableOne(EntityTypeBuilder<TableOne> b)
    {
        b.ToTable("TableOne");
        b.HasKey(x => x.Id)
            .HasName("PK_TableOne_Id");
        b.Property(x => x.Id).ValueGeneratedOnAdd();
        b.Property(x => x.TestString)
            .HasMaxLength(50)
            .IsRequired();
        b.Property(x => x.TestIntWithoutConstraint)
            .IsRequired(false);
        b.Property(x => x.TestIntWithConstraint)
            .IsRequired();
        b.HasIndex(x => x.TestIntWithConstraint)
            .IsUnique()
            .HasDatabaseName("UQ_TableOne_TestIntWithConstraint");
        b.HasCheckConstraint(
            "CK_TableOne_TestIntWithConstraint",
            "[TestIntWithConstraint] >= 0");
    }

    private static void ConfigureTableTwo(EntityTypeBuilder<TableTwo> b)
    {
        b.ToTable("TableTwo");
        b.HasKey(x => x.Id)
            .HasName("PK_TableTwo_Id");
        b.Property(x => x.Id).ValueGeneratedOnAdd();
        b.Property(x => x.TestData)
            .HasColumnType("date")
            .IsRequired(false);
        b.Property(x => x.TestIntUnique)
            .IsRequired();
        b.HasIndex(x => x.TestIntUnique)
            .IsUnique()
            .HasDatabaseName("UQ_TableTwo_TestIntUnique");
        b.HasIndex(x => x.IdTableOneForeignKeyTest)
            .HasDatabaseName("IX_TableTwo_IdTableOneForeignKeyTest");
        b.HasOne(x => x.TableOne)
            .WithMany(t => t.TableTwos)
            .HasForeignKey(x => x.IdTableOneForeignKeyTest)
            .HasConstraintName("FK_TableTwo_IdTableOne")
            .OnDelete(DeleteBehavior.Restrict);   
    }

    private static void ConfigureTableThree(EntityTypeBuilder<TableThree> b)
    {
        b.ToTable("TableThree");
        b.HasKey(x => x.Id)
            .HasName("PK_TableThree_Id");
        b.Property(x => x.Id).ValueGeneratedOnAdd();
        b.Property(x => x.TestStringConstraint)
            .HasMaxLength(10)
            .IsRequired();
        b.HasIndex(x => x.IdTableOneForeignKeyTest)
            .HasDatabaseName("IX_TableThree_IdTableOneForeignKeyTest");
        b.HasIndex(x => x.IdTableTwoForeignKeyTest)
            .HasDatabaseName("IX_TableThree_IdTableTwoForeignKeyTest");
        b.HasCheckConstraint(
            "CK_TableThree_TestStringConstraint",
            "LEN([TestStringConstraint]) >= 3");
        b.HasOne(x => x.TableOne)
            .WithMany(t => t.TableThrees)
            .HasForeignKey(x => x.IdTableOneForeignKeyTest)
            .HasConstraintName("FK_TableThree_IdTableOne")
            .OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.TableTwo)
            .WithMany(t => t.TableThrees)
            .HasForeignKey(x => x.IdTableTwoForeignKeyTest)
            .HasConstraintName("FK_TableThree_IdTableTwo")
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureStatus(EntityTypeBuilder<Status> b)
    {
        b.ToTable("Status");
        b.HasKey(x => x.Id)
            .HasName("PK_Status_Id");
        b.Property(x => x.Id).ValueGeneratedOnAdd();
        b.Property(x => x.StatusName)
            .HasColumnName("Status")      
            .HasMaxLength(10)
            .IsRequired();
    }

    private static void ConfigureSubTable(EntityTypeBuilder<SubTable> b)
    {
        b.ToTable("SubTable");
        b.HasNoKey();
        b.Property(x => x.IdStatus).IsRequired();
        foreach (var property in b.Metadata.GetProperties())
        {
            if (property.ClrType == typeof(string))
                property.SetColumnType("nvarchar(max)");
        }
    }
}