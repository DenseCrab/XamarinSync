using System;

namespace SyncExample.API.Requests.Implementations
{
    public class GetUnsyncedMessagesRequest : IGetUnsyncedMessagesRequest
    {
        public DateTime StartDate { get; set; }
    }
}
