using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public T data { get; set; }

        public IEnumerable<string> errorList { get; set; }

        public static Result<T> success(T objData)
        {
            return new Result<T>
            {
                IsSuccess = true,
                data = objData
            };
        }

        public static Result<T> failed(IEnumerable<string> errors)
        {
            return new Result<T> { 
                IsSuccess = false,
                errorList = errors
            };
        }
    }
}
