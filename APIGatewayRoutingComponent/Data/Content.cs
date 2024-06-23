using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayRouting.Data
{
    public class Content
    {
        public Guid Uuid { set; get; } = Guid.Empty; //TODO: It is empty by default???
        public string Title { set; get; }
        public DateTime UploadTime { set; get; }
        //TODO: public DateTime UpdateTime { set; get; }
        public int Duration { set; get; }
        public string ImageUrl { set; get; }
        public string Description { set; get; }
        public IEnumerable<LicenseRules> LicenseRules { set; get; }
        public IEnumerable<ContentComment> ContentComments { set; get; }
    }
}
