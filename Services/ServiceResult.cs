namespace tp_a_rendre.Services;

public class ServiceResult<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }

    public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };

    public static ServiceResult<T> Fail(string message) => new() { Success = false, ErrorMessage = message };
}
