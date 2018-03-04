using System;

namespace SyncExample.SQLite.DTOs
{
    public interface IUserDTO
    {
        DateTime? LastSyncedOn { get; set; }
    }
}
