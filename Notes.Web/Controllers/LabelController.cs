using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Labels.Commands.CreateLabel;
using Notes.Data.Features.Labels.Commands.DeleteLabel;
using Notes.Data.Features.Labels.Commands.UpdateLabel;
using Notes.Data.Features.Labels.Queries.GetLabel;
using Notes.Data.Features.Labels.Queries.GetLabels;

namespace Notes.Web.Controllers;

/// <summary>
/// Контроллер ярлыков
/// </summary>
[Route("api/labels")]
public sealed class LabelController : Controller
{
    /// <summary>
    /// Запрос на получение всех ярлыков
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Ярлыки</returns>
    [HttpGet]
    public async Task<IActionResult> GetLabelsAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetLabelsQuery(), cancellationToken));
    }

    /// <summary>
    /// Запрос на получение ярлыка по id
    /// </summary>
    /// <param name="labelId">Id ярлыка</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Ярлык</returns>
    [HttpGet("{labelId:int}", Name = nameof(GetLabelAsync))]
    public async Task<IActionResult> GetLabelAsync(
        [FromRoute] int labelId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetLabelQuery(labelId), cancellationToken));
    }
    
    /// <summary>
    /// Запрос на создание ярлыка
    /// </summary>
    /// <param name="dto">Данные ярлыка</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Id ярлыка</returns>
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
    
    /// <summary>
    /// Запрос на обновление ярлыка
    /// </summary>
    /// <param name="labelId">Id ярлыка</param>
    /// <param name="dto">Обновлённые данные</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpPut("{labelId:int}")]
    public async Task<IActionResult> UpdateLabelAsync(
        [FromRoute] int labelId,
        [FromBody] UpdateLabelDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateLabelCommand(labelId, dto), cancellationToken);

        return NoContent();
    }
    
    /// <summary>
    /// Запрос на удаление ярлыка
    /// </summary>
    /// <param name="labelId">Id ярлыка</param>
    /// <param name="cancellationToken">Токен отмкены запроса</param>
    [HttpDelete("{labelId:int}")]
    public async Task<IActionResult> DeleteLabelAsync(
        [FromRoute] int labelId,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteLabelCommand(labelId), cancellationToken);

        return NoContent();
    }
}