using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Notes.Commands.CreateNote;
using Notes.Data.Features.Notes.Commands.DeleteNote;
using Notes.Data.Features.Notes.Queries.GetNote;
using Notes.Data.Features.Notes.Queries.GetNotes;

namespace Notes.Web.Controllers;

[Route("api/notes")]
public sealed class NotesController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetNotesAsync(
        [FromQuery] CancellationToken cancellationToken) =>
        Ok(await Mediator.Send(
            new GetNotesQuery(),
            cancellationToken));

    [HttpGet("{noteId:int}", Name = nameof(GetNoteAsync))]
    public async Task<IActionResult> GetNoteAsync(
        [FromRoute] int noteId,
        [FromQuery] CancellationToken cancellationToken) =>
        Ok(await Mediator.Send(
            new GetNoteQuery(noteId),
            cancellationToken));

    [HttpPost]
    public async Task<IActionResult> CreateNoteAsync(
        [FromBody] CreateNoteDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var noteId = await Mediator.Send(
            new CreateNoteCommand(dto),
                cancellationToken);

        // ReSharper disable once Mvc.ActionNotResolved
        return CreatedAtAction(
            nameof(GetNoteAsync),
            new
            {
                noteId
            }, 
            noteId);
    }

    [HttpPut("{noteId:int}")]
    public async Task<IActionResult> UpdateNoteAsync(
        [FromRoute] int noteId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new GetNoteQuery(noteId),
            cancellationToken);

        return NoContent();
    }

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