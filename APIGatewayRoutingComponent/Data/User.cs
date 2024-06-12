using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayRouting.Data
{
    public enum UserLevel
    {
        Unknown,
        EndUser,
        ContentCreator,
        Administrator
    }
    public class User
    {
        public Guid Uuid { set; get; }
        public string Name { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string Mail { set; get; }
        public UserLevel UserLevel { set; get; }

        public DateTime CreationTime { set; get; }
        //TODO: public DateTime UpdateTime { set; get; }
    }
}
