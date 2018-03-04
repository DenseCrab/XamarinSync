using SyncExample.API.Models;
using System;
using System.Collections.Generic;

namespace SyncExample.API.Requests
{
    public interface ISaveMessagesRequest
    {
        List<IMessageModel> MessageDtos { get; set; }
    }
}
