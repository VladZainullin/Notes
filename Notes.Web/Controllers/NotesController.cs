using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Notes.Commands.CreateNote;
using Notes.Data.Features.Notes.Commands.DeleteNote;
using Notes.Data.Features.Notes.Commands.UpdateNote;
using Notes.Data.Features.Notes.Queries.GetNote;
using Notes.Data.Features.Notes.Queries.GetNotes;

namespace Notes.Web.Controllers;

/// <summary>
/// Контроллер заметок
/// </summary>
[Route("api/notes")]
public sealed class NotesController : Controller
{
    /// <summary>
    /// Запрос на получение всех заметок
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Заметки</returns>
    [HttpGet]
    public async Task<IActionResult> GetNotesAsync(
        [FromQuery] CancellationToken cancellationToken) =>
        Ok(await Mediator.Send(
            new GetNotesQuery(),
            cancellationToken));

    /// <summary>
    /// Запрос на получение заметки по Id
    /// </summary>
    /// <param name="noteId">Id заметки</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpGet("{noteId:int}", Name = nameof(GetNoteAsync))]
    public async Task<IActionResult> GetNoteAsync(
        [FromRoute] int noteId,
        [FromQuery] CancellationToken cancellationToken) =>
        Ok(await Mediator.Send(
            new GetNoteQuery(noteId),
            cancellationToken));

    /// <summary>
    /// Запрос на создание заметки
    /// </summary>
    /// <param name="dto">Данные заметки</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Id заметки</returns>
    [HttpPost]
    public async Task<IActionResult> CreateNoteAsync(
        [FromBody] CreateNoteDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var noteId = await Mediator.Send(
            new CreateNoteCommand(dto),
            cancellationToken);

        // ReSharper disable once Mvc.ActionNotResolved
        return CreatedAtRoute(
            nameof(GetNoteAsync),
            new
            {
                noteId
            },
            noteId);
    }

    /// <summary>
    /// Запрос на обновление заметки
    /// </summary>
    /// <param name="noteId">Id заметки</param>
    /// <param name="dto">Обновлённые данные</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpPut("{noteId:int}")]
    public async Task<IActionResult> UpdateNoteAsync(
        [FromRoute] int noteId,
        [FromBody] UpdateNoteDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdateNoteCommand(noteId, dto),
            cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Запрос на удаление заметки
    /// </summary>
    /// <param name="noteId">Id заметки</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpDelete("{noteId:int}")]
    public async Task<IActionResult> DeleteNoteAsync(
        [FromRoute] int noteId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeleteNoteCommand(noteId),
            cancellationToken);

        return NoContent();
    }
}