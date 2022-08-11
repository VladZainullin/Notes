using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.NoteLabels.Commands.CreateNoteLabel;
using Notes.Data.Features.NoteLabels.Commands.DeleteNoteLabel;
using Notes.Data.Features.NoteLabels.Queries.GetNoteLabels;

namespace Notes.Web.Controllers;

/// <summary>
///     Контроллер ярлыков заметки
/// </summary>
[Route("api/notes/{noteId:int}/labels")]
public sealed class NoteLabelController : Controller
{
    /// <summary>
    ///     Запрос на получение ярлыков заметки
    /// </summary>
    /// <param name="noteId">Id заметки</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Ярлыки заметки</returns>
    [HttpGet]
    public async Task<IActionResult> GetNoteLabelsAsync(
        [FromRoute] int noteId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(
            new GetNoteLabelsQuery(noteId),
            cancellationToken));
    }

    /// <summary>
    ///     Команда добавления ярлыка для заметки
    /// </summary>
    /// <param name="noteId">Id заметки</param>
    /// <param name="labelId">Id ярлыка</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpPost("{labelId:int}")]
    public async Task<IActionResult> CreateNoteLabelAsync(
        [FromRoute] int noteId,
        [FromRoute] int labelId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new CreateNoteLabelCommand(
                noteId,
                labelId),
            cancellationToken);

        return NoContent();
    }

    /// <summary>
    ///     Команда удаления ярлыка заметки
    /// </summary>
    /// <param name="noteId">Id заметки</param>
    /// <param name="labelId">Id ярлыка</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpDelete("{labelId:int}")]
    public async Task<IActionResult> DeleteNoteLabelAsync(
        [FromRoute] int noteId,
        [FromRoute] int labelId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeleteNoteLabelCommand(
                noteId,
                labelId),
            cancellationToken);

        return NoContent();
    }
}