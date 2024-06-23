using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.DTO.Models
{
    public class ContentCommentModel
    {
       // public Guid Uuid { set; get; }
        //[Required]
        public string Body { set; get; }
        public DateTime CreationTime { set; get; }

        //[Required]
        public string AuthorName { set; get; } //TODO: Change to UserName?
    }
}