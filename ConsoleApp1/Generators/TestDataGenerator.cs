using System.Text;
using ClosedXML.Excel;
using DomainClass;

namespace Generators;

public static class TestDataGenerator
{
    public static List<AllDataDto> CreateDataReturnEntity(ErrorRate numberOfErroneousEntries, int quantity)
    {
        int percentError = (int)numberOfErroneousEntries;
        var data = new AllDataDto[quantity];
        for (int i = 0; i < quantity; i++)
        {
            ulong state = DeterministicRandom.Mix((ulong)GeneratorConsts.DefaultSeed, (ulong)i);  
            var row = DeterministicRandom.RowFactory(i, ref state);
            if (ErrorInjector.IsErrorRow(i, percentError))
                ErrorInjector.ApplyError(row, ErrorInjector.PickErrorType(i, percentError));
            data[i] = row;
        }
        return [..data];
    }
    
    public static string CreateDataReturnXslx(ErrorRate numberOfErroneousEntries, int quantity)
    {
        var allData = CreateDataReturnEntity(numberOfErroneousEntries, quantity);
        var directory = Path.Combine(Path.GetTempPath(), "benchmark_input");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, $"input_{quantity}_{(int)numberOfErroneousEntries}.xlsx");
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Input");

        for (int c = 0; c < GeneratorConsts.FileColumns.Length; c++)
        {
            sheet.Cell(1, c + 1).Value = GeneratorConsts.FileColumns[c];
        }

        for (int i = 0; i < allData.Count; i++)
        {
            XlsxWriter.WriteXslxRow(sheet, i + 2, allData[i]);
        }

        workbook.SaveAs(path);
        return path;
    }
    
    public static string CreateDataReturnCsv(ErrorRate numberOfErroneousEntries, int quantity)
    {
        var allData = CreateDataReturnEntity(numberOfErroneousEntries, quantity);

        var directory = Path.Combine(Path.GetTempPath(), "benchmark_input");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, $"input_{quantity}_{(int)numberOfErroneousEntries}.csv");

        using var writer = new StreamWriter(path, false, new UTF8Encoding(false));
        writer.WriteLine(string.Join(",", GeneratorConsts.FileColumns));

        foreach (var r in allData)
        {
            writer.WriteLine(string.Join(",",
                DeterministicRandom.Escape(r.TableOneId),
                DeterministicRandom.Escape(r.TableTwoId),
                DeterministicRandom.Escape(r.TableThreeId),
                DeterministicRandom.Escape(r.TableOneTestString),
                DeterministicRandom.Escape(r.TableOneTestIntWithoutConstraint),
                DeterministicRandom.Escape(r.TableOneTestIntWithConstraint),
                DeterministicRandom.Escape(r.TableTwoIdTableOneForeignKeyTest),
                DeterministicRandom.Escape(r.TableTwoTestData),
                DeterministicRandom.Escape(r.TableTwoTestIntUnique),
                DeterministicRandom.Escape(r.TableThreeIdTableOneForeignKeyTest),
                DeterministicRandom.Escape(r.TableThreeIdTableTwoForeignKeyTest),
                DeterministicRandom.Escape(r.TableThreeTestStringConstraint)));
        }

        return path;
    }
}