using System.Net;

namespace Notes.Data.Exceptions;

[Serializable]
internal class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}