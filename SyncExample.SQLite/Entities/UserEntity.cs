using SQLite;
using SyncExample.SQLite.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.Entities
{
    public class UserEntity : IUserDTO
    {
        public DateTime? LastSyncedOn { get; set; }
    }
}
