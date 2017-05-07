using RefactorMe.DontRefactor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorMe.Services
{
    public interface IService
    {
        IList<Product> List();
        IList<Product> ListInUSDollars();
        IList<Product> ListInEuro();
    }
}
