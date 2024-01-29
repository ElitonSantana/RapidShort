using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidShort.Domain.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        T GetById(object id);
        void Create(T entity);
        void Update(T entity);
        void Delete(object id);
        void Save();
    }
}
