using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        //++ after adding Ninject Factory
        void Save();
    }
}