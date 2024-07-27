using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServiceAPI.Models.Requests
{
    public class ContentCreatorRequestDto : UserRequestDto
    {
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string NIP { get; set; }

    }
}

