using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnecoreTestWebApiRest.Wrappers
{
    public class Response<T>
    {
        public Response(List<Models.Clientes> clientes)
        {
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            //Data = new List<data>();
        }

        //public Response(List<T> data)
        //{
        //    Succeeded = true;
        //    Message = string.Empty;
        //    Errors = null;
        //    //Data = data;
        //    Items = new List<data>();
        //}

        public T Data { get; set; }
        public List<T> Items { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
