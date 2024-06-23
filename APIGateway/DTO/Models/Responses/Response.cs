namespace APIGatewayControllers.DTO.Models.Responses
{
    public class Response<T>
    {
        public T? Result { get; set; }
        public string Message { get; set; } = "";
    }
}
