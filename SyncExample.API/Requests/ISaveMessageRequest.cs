using SyncExample.API.Models;
using System;
using System.Collections.Generic;

namespace SyncExample.API.Requests
{
    public interface ISaveMessageRequest
    {
        string Text { get; set; }
        string Description { get; set; }
        int? ClientId { get; set; }
    }
}
