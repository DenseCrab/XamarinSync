using SyncExample.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Responses
{
    public interface ISaveMessagesResponse : IBaseResponse
    {
        List<IResponseMessageModel> MessageList { get; set; }
    }
}
