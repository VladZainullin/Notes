using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Web.Controllers;

[ApiController]
public class Controller : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()
                                 ??
                                 throw new NullReferenceException();
}