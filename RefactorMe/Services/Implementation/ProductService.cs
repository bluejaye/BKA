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
    public class ProductService : IService
    {
        BaseService<Lawnmower> _lawnmower;
        BaseService<PhoneCase> _phonecase;
        BaseService<TShirt> _tshirt;
        public ProductService(BaseService<Lawnmower> lawnmower, BaseService<PhoneCase> phonecase, BaseService<TShirt> tshirt)
        {
            _lawnmower = lawnmower;
            _phonecase = phonecase;
            _tshirt = tshirt;
        }

        public IList<Product> ListInEuro()
        {
            return _lawnmower.ListInEuro()
                .Union(_phonecase.ListInEuro())
                .Union(_tshirt.ListInEuro())
                .ToList();
        }

        public IList<Product> ListInUSDollars()
        {
            return _lawnmower.ListInUSDollars()
                .Union(_phonecase.ListInUSDollars())
                .Union(_tshirt.ListInUSDollars())
                .ToList();
        }

        public IList<Product> List()
        {
            return _lawnmower.List()
                .Union(_phonecase.List())
                .Union(_tshirt.List())
                .ToList();
        }
    }
}
