
namespace AuthorizationServiceAPI.Models.Responses
{
    internal class ResponseModel<T> //TODO: Adapt
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}
