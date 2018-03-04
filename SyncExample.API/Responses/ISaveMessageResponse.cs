using SyncExample.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Responses
{
    public interface ISaveMessageResponse : IBaseResponse
    {
        int ApiId { get; set; }
        int ClientId { get; set; }
    }
}
