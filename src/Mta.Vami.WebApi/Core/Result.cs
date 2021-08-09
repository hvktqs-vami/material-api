using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public class Result
    {
        public const string OK_CODE = "00";
        public const string EXCEPTION_CODE = "99";

        public string Code { get; set; }

        public string Message { get; set; }

        public bool IsOk()
        {
            return Code == OK_CODE;
        }

        public bool IsError()
        {
            return !IsOk();
        }

        public bool IsException()
        {
            return Code == EXCEPTION_CODE;
        }

        public static Result Ok()
        {
            return new Result
            {
                Code = OK_CODE,
                Message = ""
            };
        }

        public static Result Error(string message)
        {
            return new Result
            {
                Code = "01",
                Message = message
            };
        }

        public static Result<TData> Ok<TData>(TData data)
        {
            return new Result<TData>
            {
                Code = OK_CODE,
                Message = "",
                Data = data
            };
        }    


        public static Result<TData> Error<TData>(string message, TData data = default)
        {
            return new Result<TData>
            {
                Code = "01",
                Message = message,
                Data = data
            };
        }    

        public static Result Exception(string message, Exception ex)
        {
            return new Result
            {
                Code = EXCEPTION_CODE,
                Message = string.Format("{0}:{1}", message, ex.ToString())
            };
        }

        public static Result<TData> Exception<TData>(string message, Exception ex)
        {
            return new Result<TData>
            {
                Code = EXCEPTION_CODE,
                Message = string.Format("{0}:{1}", message, ex.ToString())
            };
        }

        public Result<TData> To<TData>()
        {
            var rs = new Result<TData>
            {
                Code = Code,
                Message = Message
            };
            return rs;
        }

    }


    public class Result<TData>: Result
    {
        public TData Data { get; set; }
    }    
}
