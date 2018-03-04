using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Models
{
    public interface IResponseMessageModel
    {
        int ClientId { get; set; }
        int ApiId { get; set; }
    }
}
