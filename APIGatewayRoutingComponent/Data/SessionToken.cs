using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayRouting.Data
{
    public class SessionToken
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
