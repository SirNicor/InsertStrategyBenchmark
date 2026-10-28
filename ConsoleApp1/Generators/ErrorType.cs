namespace Generators;

public enum ErrorType
{
    ForeignKeyViolation = 0,
    UniqueViolation = 1,
    CheckViolation = 2,
    LengthViolation = 3,
    TypeError = 4,
    NullViolation = 5
}