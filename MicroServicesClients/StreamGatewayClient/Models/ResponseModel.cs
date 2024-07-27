
namespace StreamGatewayAPI.Models.Responses
{
    internal class ResponseModel<T> //TODO: Make one standard for all services??? Maybe not
    {
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
