using SyncExample.SQLite.Repositories.Implementations;

namespace SyncExample.SQLite
{
    public interface ILocalDatabaseContext
    {
        MessageRepository MessageRepository { get; }
        UserRepository UserRepository { get; }
        bool ClearDatabase();
    }
}
