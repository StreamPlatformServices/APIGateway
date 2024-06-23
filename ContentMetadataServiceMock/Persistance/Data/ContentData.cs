﻿using APIGatewayRouting.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentMetadataServiceMock.Persistance.Data
{
    //TODO: Opcjonalne pola i wymagane dla wszystkich danych (nullable)
    public class ContentData
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContentId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int Duration { get; set; }
        public IEnumerable<ContentCommentData> Comments { get; set; } 
        public IEnumerable<LicenseRulesData> LicenseRules { set; get; }


    }
}
