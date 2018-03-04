using Newtonsoft.Json;
using SyncExample.API.Models;
using SyncExample.API.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Responses.Implementations
{
    public class SaveMessageResponse : ISaveMessageResponse
    {
        public int ApiId { get; set; }
        public int ClientId { get; set; }

        public DateTime TimeStamp { get; set; }
        public bool Success { get; set; }
        public string Exception { get; set; }
        public string ExceptionCode { get; set; }
    }
}
