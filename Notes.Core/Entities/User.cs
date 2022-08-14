namespace Notes.Core.Entities;

/// <summary>
///     Пользователь
/// </summary>
public sealed class User
{
    /// <summary>
    ///     Id пользователя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Имя пользователя
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Фамилия пользователя
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    ///     Отчество пользователя
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    ///     Полное имя пользователя
    /// </summary>
    public string? FullName => $"{Surname} {Name} {Patronymic}";

    /// <summary>
    ///     Сокращённое имя пользователя
    /// </summary>
    public string? FullNameShort => $"{Surname} {Name?[0]}. {Patronymic?[0]}.";

    /// <summary>
    ///     Дата рождения пользователя
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    ///     Возраст пользователя
    /// </summary>
    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    /// <summary>
    ///     Адрес электронной почты пользователя
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///     Пароль пользователя
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    ///     Заметки пользователя
    /// </summary>
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    /// <summary>
    ///     Ярлыки пользователя
    /// </summary>
    public ICollection<Label> Labels { get; set; } = new List<Label>();
}