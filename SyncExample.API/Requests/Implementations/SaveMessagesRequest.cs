using Newtonsoft.Json;
using SyncExample.API.Models;
using SyncExample.API.Models.Implementations;
using System;
using System.Collections.Generic;

namespace SyncExample.API.Requests.Implementations
{
    public class SaveMessagesRequest : ISaveMessagesRequest
    {
        [JsonConverter(typeof(ConcreteListTypeConverter<IMessageModel, MessageModel>))]
        public List<IMessageModel> MessageDtos { get; set; }
    }
}
