using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Responses
{
    public interface IBaseResponse
    {
        DateTime TimeStamp { get; set; }
        bool Success { get; set; }
        string ExceptionCode { get; set; }
        string Exception { get; set; }
    }
}
