using SQLite;
using SyncExample.SQLite.DTOs;
using SyncExample.SQLite.DTOs.Implementations;
using SyncExample.SQLite.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.Repositories.Implementations
{
    public class MessageRepository : BaseRepository<IMessageDTO, MessageDTO, MessageEntity>, IMessageRepository
    {
        public MessageRepository(SQLiteConnection context) : base(context)
        {
        }
    }
}
