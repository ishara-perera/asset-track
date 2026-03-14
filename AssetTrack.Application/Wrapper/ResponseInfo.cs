using System.Net;

namespace AssetTrack.Application.Wrapper;

public class ResponseInfo<T>
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static ResponseInfo<T> Success(T data, HttpStatusCode httpStatusCode, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
        }

        return new ResponseInfo<T>()
            { IsSuccess = true, Data = data, StatusCode = httpStatusCode };
    }

    public static ResponseInfo<T> Failure(string errorMessage, HttpStatusCode httpStatusCode) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = httpStatusCode };

}