using APIGatewayRouting.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentMetadataServiceMock.Persistance.Data
{
    public class LicenseRulesData
    {
        //TODO: Where should be FK >>>??? 
        
        [Key]
        public Guid Uuid { set; get; }
        public int Prize { set; get; }
        public LicenseType Type { set; get; }
        public LicenseDuration Duration { set; get; }

        [ForeignKey("content")]
        public Guid ContentId { set; get; }
        public ContentData Content { set; get; }
    }
}
