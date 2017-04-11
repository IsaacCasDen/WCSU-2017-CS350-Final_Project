using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Response
    {
        bool IsSuccess = false;
        String Message;
        object ResponseData;

        public Response(bool status, String message, Object data)
        {
            this.IsSuccess = status;
            this.Message = message;
            this.ResponseData = data;
        }
    }
}