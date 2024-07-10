namespace APIGatewayControllers.Models.Responses.Content
{
    public class GetContentsByUserResponseModel
    {
        public IEnumerable<ContentCreatorContentsResponseModel> ContentCreatorContents { get; set; }
        public int Count { get; set; }
    }
}
