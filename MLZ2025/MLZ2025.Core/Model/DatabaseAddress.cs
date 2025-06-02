using System.ComponentModel.DataAnnotations.Schema;

namespace MLZ2025.Core.Model;

[Table("Address")]
public class DatabaseAddress
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";
}

public class PrimaryKeyAttribute : Attribute
{
}

public class AutoIncrementAttribute : Attribute
{
}
