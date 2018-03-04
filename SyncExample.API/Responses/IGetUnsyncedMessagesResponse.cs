using SyncExample.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Responses
{
    public interface IGetUnsyncedMessagesResponse : IBaseResponse
    {
        List<IMessageModel> MessageList { get; set; }
    }
}
