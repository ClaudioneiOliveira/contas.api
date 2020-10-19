namespace contas.api.Domain.Models.Exceptions
{
    public enum Category
    {
        Undefined = -1,
        InvalidData = 0,
        NotFound = 1,
        Conflict = 2,
        ExternalAcess = 3,
        DataBase = 4,
        InternalError = 5,
        Duplicated = 6
    }
 }