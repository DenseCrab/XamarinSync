using Newtonsoft.Json;
using SyncExample.API.Models;
using SyncExample.API.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Responses.Implementations
{
    public class GetUnsyncedMessagesResponse : IGetUnsyncedMessagesResponse
    {
        [JsonConverter(typeof(ConcreteListTypeConverter<IMessageModel, MessageModel>))]
        public List<IMessageModel> MessageList { get; set; }

        public DateTime TimeStamp { get; set; }
        public bool Success { get; set; }
        public string Exception { get; set; }
        public string ExceptionCode { get; set; }
    }
}
