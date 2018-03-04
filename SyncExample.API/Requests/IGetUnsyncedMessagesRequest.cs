using System;

namespace SyncExample.API.Requests
{
    public interface IGetUnsyncedMessagesRequest
    {
        DateTime StartDate { get; set; }
    }
}
