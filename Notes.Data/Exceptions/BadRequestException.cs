using System.Net;
using System.Runtime.Serialization;

namespace Notes.Data.Exceptions;

[Serializable]
internal class BadRequestException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadGateway;
    
    public BadRequestException(string message) : base(message)
    {
    }
}