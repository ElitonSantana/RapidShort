using Microsoft.EntityFrameworkCore;
using RapidShort.Domain.Services.Interfaces;
using RapidShort.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidShort.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _Context;
        private readonly DbSet<T> _DbSet;
        public Repository(ApplicationDbContext Context)
        {
            _Context = Context;
            _DbSet = Context.Set<T>();
        }

        public List<T> Get()
        {
            return _DbSet.AsQueryable().ToList();
        }
        public T GetById(object id)
        {
            return _DbSet.Find(id);
        }

        public void Create(T entity)
        {
            _DbSet.Add(entity);
            _Context.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            _DbSet.Update(entity);
            Save();
        }

        public void Delete(object id)
        {
            T entityToDelete = _DbSet.Find(id);
            if (entityToDelete != null)
                _DbSet.Remove(entityToDelete);
            Save();
        }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
