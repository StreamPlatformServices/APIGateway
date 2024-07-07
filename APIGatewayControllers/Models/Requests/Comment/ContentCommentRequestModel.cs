using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Requests.Comment
{
    public class ContentCommentRequestModel
    {
        [Required]
        public Guid ContentId { set; get; }
        [Required]
        public string Body { set; get; }   
    }
}