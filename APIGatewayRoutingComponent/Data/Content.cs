using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayRouting.Data
{
    public class Content
    {
        public Guid Uuid { set; get; }
        public string Name { set; get; }
        public DateTime UploadTime { set; get; }
        //TODO: public DateTime UpdateTime { set; get; }
        public string Description { set; get; }
        public IEnumerable<ContentComment> ContentComments { set; get; }
    }
}
