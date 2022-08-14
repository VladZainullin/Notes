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

    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public ICollection<Note> Notes { get; set; } = new List<Note>();

    public ICollection<Label> Labels { get; set; } = new List<Label>();
}