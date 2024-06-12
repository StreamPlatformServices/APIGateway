using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServiceAPI.Models
{
    internal class ResponseModel<T> //TODO: Adapt
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        
    }
}
