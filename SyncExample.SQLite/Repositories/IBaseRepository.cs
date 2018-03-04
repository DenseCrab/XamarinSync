using SyncExample.SQLite.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.Repositories
{
    public interface IBaseRepository<T> where T : IBaseDTO
    {
        IEnumerable<T> Items { get; }
        bool Insert(T item);
        bool Save(T item);
        bool Remove(T item);
        IBaseDTO GetByAPIPrimaryKey(int apiId);
        IBaseDTO GetByClientId(int clientId);
        IEnumerable<T> GetUnsyncedItems { get; }
    }
}
