namespace Generators;

internal static class GeneratorConsts
{
    internal const int DefaultSeed = 42;
    internal const int Block = 100;
    internal const long Prime = 1_000_000_007L;
    internal const long MultA = 2_654_435_761L;   
    internal const long MultB = 40_503L;          
    internal const ulong GoldenGamma = 0x9E3779B97F4A7C15UL;
    internal const ulong Splitmix64One = 0xBF58476D1CE4E5B9UL;
    internal const ulong Splitmix64Two = 0x94D049BB133111EBUL;
    internal const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789 ";
    internal const long ReservedUniqueAnchor = 2_000_000_000L;
    internal static readonly DateTime DateBase = new(2020, 1, 1);
    internal static readonly string[] FileColumns =
    {
        "TableOneId", "TableTwoId", "TableThreeId",
        "TableOneTestString", "TableOneTestIntWithoutConstraint", "TableOneTestIntWithConstraint",
        "TableTwoIdTableOneForeignKeyTest", "TableTwoTestData", "TableTwoTestIntUnique",
        "TableThreeIdTableOneForeignKeyTest", "TableThreeIdTableTwoForeignKeyTest",
        "TableThreeTestStringConstraint"
    };
}