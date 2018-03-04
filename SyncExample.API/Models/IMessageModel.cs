using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API.Models
{
    public interface IMessageModel
    {
        int Id { get; set; }
        string Text { get; set; }
        string Description { get; set; }
        int? ClientId { get; set; }
    }
}
