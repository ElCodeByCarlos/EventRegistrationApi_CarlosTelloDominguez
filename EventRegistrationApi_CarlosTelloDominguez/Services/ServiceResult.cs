namespace EventRegistrationApi_CarlosTelloDominguez.Services
{
    public enum ServiceResultStatus
    {
        Success,
        NotFound,
        BadRequest,
        Conflict
    }

    public class ServiceResult<T>
    {
        public ServiceResultStatus Status { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }

        public bool IsSuccess => Status == ServiceResultStatus.Success;
        public static ServiceResult<T> Success(T data) =>
            new() { Status = ServiceResultStatus.Success, Data = data };

        public static ServiceResult<T> NotFound(string message) =>
            new() { Status = ServiceResultStatus.NotFound, ErrorMessage = message };

        public static ServiceResult<T> BadRequest(string message) =>
            new() { Status = ServiceResultStatus.BadRequest, ErrorMessage = message };

        public static ServiceResult<T> Conflict(string message) =>
            new() { Status = ServiceResultStatus.Conflict, ErrorMessage = message };
    }
}
