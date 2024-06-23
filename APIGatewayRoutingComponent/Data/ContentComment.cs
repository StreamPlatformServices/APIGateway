namespace APIGatewayRouting.Data
{
    public class ContentComment
    {
        public Guid Uuid { set; get; }
        public string Body { set; get; }
        public DateTime CreationTime { set; get; }
        public Guid AuthorId { set; get; } //TODO: Is it needed???
        public string UserName { set; get; }
    }
}