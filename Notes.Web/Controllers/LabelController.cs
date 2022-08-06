using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Labels.Commands.CreateLabel;
using Notes.Data.Features.Labels.Commands.DeleteLabel;
using Notes.Data.Features.Labels.Commands.UpdateLabel;
using Notes.Data.Features.Labels.Queries.GetLabel;
using Notes.Data.Features.Labels.Queries.GetLabels;

namespace Notes.Web.Controllers;

[Route("api/labels")]
public sealed class LabelController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetLabelsAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetLabelsQuery(), cancellationToken));
    }

    [HttpGet("{labelId:int}", Name = nameof(GetLabelAsync))]
    public async Task<IActionResult> GetLabelAsync(
        [FromRoute] int labelId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetLabelQuery(labelId), cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLabelAsync(
        [FromBody] CreateLabelDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var labelId = await Mediator.Send(
            new CreateLabelCommand(dto),
            cancellationToken);
        
        return CreatedAtAction(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetLabelAsync),
            new
            {
                labelId
            },
            labelId);
    }
    
    [HttpPut("{labelId:int}")]
    public async Task<IActionResult> UpdateLabelAsync(
        [FromRoute] int labelId,
        [FromBody] UpdateLabelDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateLabelCommand(labelId, dto), cancellationToken);

        return NoContent();
    }
    
    [HttpPut("{labelId:int}")]
    public async Task<IActionResult> DeleteLabelAsync(
        [FromRoute] int labelId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteLabelCommand(labelId), cancellationToken);

        return NoContent();
    }
}