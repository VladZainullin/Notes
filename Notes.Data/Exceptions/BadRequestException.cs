using System.Net;

namespace Notes.Data.Exceptions;

[Serializable]
internal class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadGateway;
}