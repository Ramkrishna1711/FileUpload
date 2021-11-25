using System;
using System.Collections.Generic;

namespace FileUpload.Model
{
    public class FileUploadResult
    {
        public Boolean FileValid { get; set; }
        public List<string> InvalidLines { get; set; }
        public Response Response { get; set; }

    }

    public class Response
    {
        public MessageStatusCode StatusCode { get; set; }
        public string MessageDescription { get; set; }

    }

    public enum MessageStatusCode
    {
        Error = 500,
        Success = 200,
        BadRequest = 400,
    };
}
