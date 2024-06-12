
using System.ComponentModel.DataAnnotations;

namespace APIGatewayController.Models
{
    public class ContentModel
    {
        [Required]
        public string Name { set; get; }
        public DateTime UploadTime { set; get; }
        public string Description { set; get; }
        public IEnumerable<ContentCommentModel> ContentComments { set; get; }

    }
}
