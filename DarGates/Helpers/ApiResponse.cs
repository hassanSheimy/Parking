using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
        where T : new()
    {
        public T Response { get; set; }
    }

}
