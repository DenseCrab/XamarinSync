using AutoMapper;
using SQLite;
using SyncExample.SQLite.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.SQLite.Repositories.Implementations
{
    public class BaseRepository<TObject, TDTO, TEntity> : IBaseRepository<TObject> where TObject : IBaseDTO where TDTO : TObject, new() where TEntity : TObject, new()
    {
        private IMapper _mapper;
        private readonly SQLiteConnection _context;

        public BaseRepository(SQLiteConnection context)
        {
            _context = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDTO, TEntity>();
                cfg.CreateMap<TEntity, TDTO>();
            });

            _mapper = config.CreateMapper();
        }

        public IEnumerable<TObject> Items
        {
            get
            {
                IEnumerable<TEntity> list = _context.Table<TEntity>();
                List<TObject> outList = new List<TObject>();
                foreach (var item in list)
                {
                    TDTO poco = _mapper.Map<TDTO>(item);
                    outList.Add(poco);
                }

                return outList;
            }
        }

        public bool Insert(TObject item)
        {
            bool success = false;

            try
            {
                DateTime current_timestamp = DateTime.UtcNow;
                TEntity entity = _mapper.Map<TEntity>(item);
                entity.CreatedOn = current_timestamp;
                entity.UpdatedOn  = current_timestamp;
                _context.Insert(entity);
                success = true;
            }
            catch (Exception ex)
            {
                //TODO: log error to hockey app
                success = false;
            }

            return success;
        }

        public bool Save(TObject item)
        {
            bool success = false;

            try
            {
                DateTime current_timestamp = DateTime.UtcNow;
                TEntity entity = _mapper.Map<TEntity>(item);
                entity.UpdatedOn = current_timestamp;
                _context.Update(entity);
                success = true;
            }
            catch (Exception ex)
            {
                //TODO: log error to hockey app
                success = false;
            }

            return success;
        }

        public bool Remove(TObject item)
        {
            bool success = false;

            try
            {
                var entity = _context.Table<TEntity>().Where(e => e.Id == item.Id).FirstOrDefault();
                if (entity != null)
                {
                    _context.Delete(entity);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                //TODO: log error to hockey app
                success = false;
            }

            return success;
        }

        public IBaseDTO GetByAPIPrimaryKey(int apiId)
        {
            TDTO poco = default(TDTO);

            try
            {
                var entityResult = _context.Table<TEntity>().Where(i => i.ApiId == apiId).FirstOrDefault();
                poco = _mapper.Map<TDTO>(entityResult);
            }
            catch (Exception ex)
            {
                //TODO: log error to hockey app
                poco = default(TDTO);
            }

            return poco;
        }

        public IBaseDTO GetByClientId(int clientiD)
        {
            TDTO poco = default(TDTO);

            try
            {
                var entityResult = _context.Table<TEntity>().Where(i => i.Id == clientiD).FirstOrDefault();
                poco = _mapper.Map<TDTO>(entityResult);
            }
            catch (Exception ex)
            {
                //TODO: log error to hockey app
                poco = default(TDTO);
            }

            return poco;

        }

        public IEnumerable<TObject> GetUnsyncedItems
        {
            get
            {
                IEnumerable<TEntity> list = _context.Table<TEntity>().Where(i => i.SyncedOn == null);
                List<TObject> outList = new List<TObject>();
                foreach (var item in list)
                {
                    TDTO poco = _mapper.Map<TDTO>(item);
                    outList.Add(poco);
                }

                return outList;
            }
        }
    }
}
