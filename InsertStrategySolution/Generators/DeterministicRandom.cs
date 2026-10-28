using System.Text;
using DomainClass;

namespace Generators;

internal static class DeterministicRandom
{
    
    internal static ulong Mix(ulong seed, ulong i)
    {
        ulong z = seed + GeneratorConsts.GoldenGamma * (i + 1);
        z = (z ^ (z >> 30)) * GeneratorConsts.Splitmix64One;
        z = (z ^ (z >> 27)) * GeneratorConsts.Splitmix64Two;
        return z ^ (z >> 31);
    }

    internal static ulong Next(ref ulong state)
    {
        state += GeneratorConsts.GoldenGamma;
        ulong z = state;
        z = (z ^ (z >> 30)) * GeneratorConsts.Splitmix64One;
        z = (z ^ (z >> 27)) * GeneratorConsts.Splitmix64Two;
        return z ^ (z >> 31);   
    }

    internal static long Scatter(int i, long multiplier)
        => (i * multiplier) % GeneratorConsts.Prime;
    
    internal static string RandomString(ref ulong state, int length)
    {
        var sb = new StringBuilder(length);
        for (int c = 0; c < length; c++)
            sb.Append(GeneratorConsts.Alphabet[(int)(Next(ref state) % (ulong)GeneratorConsts.Alphabet.Length)]);
        return sb.ToString();
    }

    internal static string FormatId(string prefix, int index)
        => $"{prefix}-{index:D7}";

    internal static string Escape(string? value)
    {
        if (value is null) return string.Empty;
        if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0) return value;
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }


    internal static AllDataDto RowFactory(int i, ref ulong state)
    {
        int parentOne = Math.Max(0, i - (int)(Next(ref state) % 4));
        int parentTwo = Math.Max(0, i - (int)(Next(ref state) % 4));
        int parentThree = Math.Max(0, i - (int)(Next(ref state) % 4));

        return new AllDataDto
        {
            TableOneId = FormatId("T1", i),
            TableTwoId = FormatId("T2", i),
            TableThreeId = FormatId("T3", i),

            TableOneTestString = RandomString(ref state, 10 + (int)(Next(ref state) % 40)),
            TableOneTestIntWithoutConstraint = i % 7 == 0
                ? null
                : ((Next(ref state) % 2001) - 1000).ToString(),TableOneTestIntWithConstraint = i == 0
                ? GeneratorConsts.ReservedUniqueAnchor.ToString()   
                : Scatter(i, GeneratorConsts.MultA).ToString(),

            TableTwoIdTableOneForeignKeyTest = FormatId("T1", parentOne),
            TableTwoTestData = i % 10 == 0
                ? null
                : GeneratorConsts.DateBase.AddDays(Next(ref state) % 2000).ToString("yyyy-MM-dd"),
            TableTwoTestIntUnique = Scatter(i, GeneratorConsts.MultB).ToString(),

            TableThreeIdTableOneForeignKeyTest = FormatId("T1", parentThree),
            TableThreeIdTableTwoForeignKeyTest = FormatId("T2", parentTwo),
            TableThreeTestStringConstraint = RandomString(ref state, 3 + (int)(Next(ref state) % 8)),

            ErrorMessage = null,   
            IdStatus = 1   
        };
    }
}