using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayEntities.Entities
{
    
    public class ContentCreatorUser : User
    {
        public string PhoneNumber { set; get; }
        public string NIP { set; get; }
       
    }
}
