using Newtonsoft.Json;
using SyncExample.API.Models;
using SyncExample.API.Models.Implementations;
using System;
using System.Collections.Generic;

namespace SyncExample.API.Requests.Implementations
{
    public class SaveMessageRequest : ISaveMessageRequest
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public int? ClientId { get; set; }
    }
}
