using SQLite;
using SyncExample.SQLite.Entities;
using SyncExample.SQLite.Repositories.Implementations;
using System;

namespace SyncExample.SQLite
{
    public class LocalDatabaseContext: ILocalDatabaseContext
    {
        private readonly SQLiteConnection _context;
        private MessageRepository _messageRepository;
        private UserRepository _userRepository;

        public LocalDatabaseContext()
        {

        }

        public LocalDatabaseContext(string dbPath)
        {
            _context = new SQLiteConnection(dbPath);
            _context.CreateTable<MessageEntity>();
            _context.CreateTable<UserEntity>();
        }

        public MessageRepository MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                    _messageRepository = new MessageRepository(_context);

                
                return _messageRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);


                return _userRepository;
            }
        }


        public bool ClearDatabase()
        {
            bool success = false;
            try
            {
                _context.DeleteAll<MessageEntity>();
                success = true;
            }
            catch (Exception e)
            {
                var xx = e.Message;

            }

            return success;

        }
    }
}
