using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Web.Controllers;

/// <summary>
/// Базовый контроллер проекта
/// </summary>
[ApiController]
public class Controller : ControllerBase
{
    /// <summary>
    /// Объект вызова обработчиков запроса 
    /// </summary>
    /// <exception cref="NullReferenceException">Объект не найден</exception>
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()
                                    ??
                                    throw new NullReferenceException();
}