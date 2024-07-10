namespace APIGatewayControllers.Models.Responses.Content
{
    public class ContentsResponseModel
    {
        public Guid Uuid { set; get; }
        public string Title { set; get; }
        public int Duration { set; get; }
        public string ImageUrl { set; get; }
    }
}
