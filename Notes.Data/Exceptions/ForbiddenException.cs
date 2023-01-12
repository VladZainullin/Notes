using System.Net;

namespace Notes.Data.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message)
    {
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
}