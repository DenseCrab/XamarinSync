using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.DTOs.Implementations
{
    public class UserDTO : IUserDTO
    {
        public DateTime? LastSyncedOn { get; set; }
    }
}
