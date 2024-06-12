namespace APIGatewayRouting.Data
{
    public class ContentComment
    {
        public Guid Uuid { set; get; }
        public string Body { set; get; }
        public DateTime CreationTime { set; get; }
        public string AuthorName { set; get; }
    }
}