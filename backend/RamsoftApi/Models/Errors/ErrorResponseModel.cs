using System.Net;

namespace Models.Errors;

public class ErrorResponseModel
{

    public ErrorResponseModel(HttpStatusCode _code, string _message)
    {
        this.Code = _code;
        this.Message = _message;
    }

    public HttpStatusCode Code { get; set; }
    public string Message { get; set; }
}
