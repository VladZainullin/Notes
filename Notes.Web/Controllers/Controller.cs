using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Web.Controllers;

[ApiController]
public class Controller : ControllerBase
{
    public IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>() 
                                 ?? 
                                 throw new NullReferenceException();
}