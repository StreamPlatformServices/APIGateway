using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServiceAPI.Models.Requests
{
    public class EndUserRequestDto : UserRequestDto
    {
        public string UserName { get; set; }

    }
}

