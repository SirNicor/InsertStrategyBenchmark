using ClosedXML.Excel;
using DomainClass;

namespace Generators;

internal static class XlsxWriter
{
    internal static void WriteXslxRow(IXLWorksheet sheet, int excelRow, AllDataDto r)
    {
        sheet.Cell(excelRow, 1).Value = r.TableOneId ?? string.Empty;
        sheet.Cell(excelRow, 2).Value = r.TableTwoId ?? string.Empty;
        sheet.Cell(excelRow, 3).Value = r.TableThreeId ?? string.Empty;
        sheet.Cell(excelRow, 4).Value = r.TableOneTestString ?? string.Empty;
        sheet.Cell(excelRow, 5).Value = r.TableOneTestIntWithoutConstraint ?? string.Empty;
        sheet.Cell(excelRow, 6).Value = r.TableOneTestIntWithConstraint ?? string.Empty;
        sheet.Cell(excelRow, 7).Value = r.TableTwoIdTableOneForeignKeyTest ?? string.Empty;
        sheet.Cell(excelRow, 8).Value = r.TableTwoTestData ?? string.Empty;
        sheet.Cell(excelRow, 9).Value = r.TableTwoTestIntUnique ?? string.Empty;
        sheet.Cell(excelRow, 10).Value = r.TableThreeIdTableOneForeignKeyTest ?? string.Empty;
        sheet.Cell(excelRow, 11).Value = r.TableThreeIdTableTwoForeignKeyTest ?? string.Empty;
        sheet.Cell(excelRow, 12).Value = r.TableThreeTestStringConstraint ?? string.Empty;
    }
}