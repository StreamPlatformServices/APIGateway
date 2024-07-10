using APIGatewayEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentMetadataServiceMock.Persistance.Data
{
    public class ContentData
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public ICollection<ContentCommentData> Comments { get; set; } 
        public ICollection<LicenseRulesData> LicenseRules { set; get; }
        public Guid OwnerId { get; set; }

    }
}
