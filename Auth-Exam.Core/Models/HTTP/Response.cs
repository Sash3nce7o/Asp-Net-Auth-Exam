using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Auth_Exam.Core.Models.HTTP
{
    public class Response<T>
    {
        public T? Data {get;set;}
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        
    }
}