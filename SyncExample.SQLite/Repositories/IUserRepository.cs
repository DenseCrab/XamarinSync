using SyncExample.SQLite.DTOs;
using System;

namespace SyncExample.SQLite.Repositories
{
    public interface IUserRepository
    {
        DateTime GetUser { get; }
    }
}
