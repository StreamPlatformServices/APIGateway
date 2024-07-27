using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentMetadataServiceMock.Persistance.Data
{
    public class ContentCommentData
    {

        //TODO: Now! migration and mappers
        
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContentCommentId { get; set; }

        public string UserName { get; set; } //TODO: ???
        public DateTime CreationTime { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Body { get; set; }

        [ForeignKey("ContentId")]
        public Guid ContentId { get; set; }
        public ContentData Content { get; set; }
        


    }
}
