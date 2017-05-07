using RefactorMe.DontRefactor.Data;
using RefactorMe.DontRefactor.Data.Implementation;
using RefactorMe.DontRefactor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorMe.Services.Implementation
{
    public abstract class BaseService<T> : IService where T : class
    {
        protected IReadOnlyRepository<T> _repository;
        public BaseService(IReadOnlyRepository<T> repository)
        {
            _repository = repository;
        }

        public IList<T> Get()
        {
            return _repository.GetAll().ToList();
        }

        public abstract IList<Product> List();

        public IList<Product> ListInUSDollars()
        {
            return List()
                .Select(item => new Product { Id = item.Id, Name = item.Name, Price = item.Price * 0.76, Type = item.Type })
                .ToList();
        }

        public IList<Product> ListInEuro()
        {
            return List()
                .Select(item => new Product { Id = item.Id, Name = item.Name, Price = item.Price * 0.67, Type = item.Type })
                .ToList();
        }
    }
}
