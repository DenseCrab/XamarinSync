using SyncExample.SQLite.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.Repositories
{
    public interface IMessageRepository : IBaseRepository<IMessageDTO>
    {
    }
}
