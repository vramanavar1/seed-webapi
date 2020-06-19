using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeedWebApi.Exceptions
{
    public class ApiError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }

        public ApiError(string message, string fieldName = "", string detail = "")
        {
            this.Message = message;
            this.FieldName = fieldName;
            this.Detail = detail;
        }
    }

}
