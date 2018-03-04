using AutoMapper;
using SQLite;
using SyncExample.SQLite.DTOs;
using SyncExample.SQLite.DTOs.Implementations;
using SyncExample.SQLite.Entities;
using System;

namespace SyncExample.SQLite.Repositories.Implementations
{
    public class UserRepository
    {
        private readonly SQLiteConnection _context;
        private IMapper _mapper;

        public UserRepository(SQLiteConnection context)
        {
            _context = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserEntity>();
                cfg.CreateMap<UserEntity, UserDTO>();
            });
            _mapper = config.CreateMapper();
        }

        public DateTime? GetLastSynced()
        {
            return _context.Table<UserEntity>().First().LastSyncedOn;
        }

        public bool UpdateLastSyncedOn(DateTime lastSync)
        {
            bool success = false;
            try
            {
                var user = _context.Table<UserEntity>().First();
                user.LastSyncedOn = lastSync;
                _context.Update(user);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
    }
}
