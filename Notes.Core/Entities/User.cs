namespace Notes.Core.Entities;

public sealed class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public string? FullName => $"{Surname} {Name} {Patronymic}";

    public string? FullNameShort => $"{Surname} {Name?[0]}. {Patronymic?[0]}.";

    public DateTime DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}