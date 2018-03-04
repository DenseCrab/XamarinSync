using SyncExample.API.Requests;
using SyncExample.API.Responses;
using System.Threading.Tasks;

namespace SyncExample.API
{
    public interface IAPIContext
    {
        Task<IGetUnsyncedMessagesResponse> GetUnsyncedMessages(IGetUnsyncedMessagesRequest request);
        Task<ISaveMessagesResponse> SaveMessages(ISaveMessagesRequest request);
        Task<ISaveMessageResponse> SaveMessage(ISaveMessageRequest request);
    }
}
