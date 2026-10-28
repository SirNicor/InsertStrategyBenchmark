using DomainClass;

namespace Generators;

internal static class ErrorInjector
{
    internal static bool IsErrorRow(int i, int percent)  => ((i + GeneratorConsts.DefaultSeed) % GeneratorConsts.Block) < percent;

    internal static ErrorType PickErrorType(int i, int percent)
    {
        var type = (ErrorType)((((i + GeneratorConsts.DefaultSeed) % GeneratorConsts.Block) * 6) / percent);
        if (i == 0 && type == ErrorType.UniqueViolation)
            type = ErrorType.CheckViolation;
        return type;
    }
    
    internal static void ApplyError(AllDataDto row, ErrorType type)
    {
        switch (type)
        {
            case ErrorType.ForeignKeyViolation:
                row.TableTwoIdTableOneForeignKeyTest = DeterministicRandom.FormatId("T1", int.MaxValue);
                break;

            case ErrorType.UniqueViolation:
                row.TableOneTestIntWithConstraint = GeneratorConsts.ReservedUniqueAnchor.ToString();
                break;

            case ErrorType.CheckViolation:
                row.TableOneTestIntWithConstraint = "-1";
                break;

            case ErrorType.LengthViolation:
                row.TableOneTestString = new string('X', 60);   
                break;

            case ErrorType.TypeError:
                row.TableOneTestIntWithConstraint = "not_a_number";
                break;

            case ErrorType.NullViolation:
                row.TableOneTestString = null;                  
                break;
        }
    }
}