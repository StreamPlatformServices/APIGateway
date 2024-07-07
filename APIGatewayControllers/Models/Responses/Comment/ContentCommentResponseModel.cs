namespace APIGatewayControllers.Models.Responses.Comment
{
    public class ContentCommentResponseModel
    {
        public Guid Uuid { set; get; }
        public string Body { set; get; }
        public DateTime CreationTime { set; get; }
        public string UserName { set; get; } 
    }
}