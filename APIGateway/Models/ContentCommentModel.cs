using System.ComponentModel.DataAnnotations;

namespace APIGatewayController.Models
{
    public class ContentCommentModel
    {
        public Guid Uuid { set; get; }
        [Required]
        public string Body { set; get; }
        public DateTime CreationTime { set; get; }
        
        [Required]
        public string AuthorName { set; get; }
    }
}