using System;

namespace SyncExample.SQLite.DTOs
{
    public interface IMessageDTO : IBaseDTO
    {
        string Text { get; set; }
        string Description { get; set; }
    }
}
