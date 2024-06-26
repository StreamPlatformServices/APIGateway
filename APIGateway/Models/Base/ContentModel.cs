
using System.ComponentModel.DataAnnotations;

namespace APIGatewayControllers.Models.Base
{
    public class ContentModel
    {
        [Required]
        public string Title { set; get; }
        public int Duration { set; get; }
        //TODO: imageUrl will be constructed from baseUrl which will be provided by StreamGateWay or by appsettings, and image id will be provided from content metadata database 
    }
}
