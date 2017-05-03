using RefactorMe.DontRefactor.Data.Implementation;
using RefactorMe.DontRefactor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorMe
{
    public class ProductDataConsolidator
    {
        protected IQueryable<Lawnmower> l;
        protected IQueryable<PhoneCase> p;
        protected IQueryable<TShirt> t;

        protected List<Product> ps = new List<Product>();

        protected ProductDataConsolidator()
        {
            l = new LawnmowerRepository().GetAll();
            p = new PhoneCaseRepository().GetAll();
            t = new TShirtRepository().GetAll();
        }

        /// <summary>
        /// keep original API, open for extention
        /// </summary>
        public List<Product> Get()
        {
            return GetByRate(1);
        }

        /// <summary>
        /// keep original API, open for extention
        /// </summary>
        public List<Product> GetInUSDollars()
        {
            return GetByRate(0.76);
        }

        /// <summary>
        /// keep original API, open for extention
        /// </summary>
        public List<Product> GetInEuros()
        {
            return GetByRate(0.67);
        }

        /// <summary>
        /// Open for a new implementation
        /// </summary>
        protected virtual List<Product> GetByRate(double rate)
        {
            ps.Clear();

            AddProduct(l, "Lawnmower", rate);
            AddProduct(p, "Phone Case", rate);
            AddProduct(t, "T-Shirt", rate);

            return ps;
        }

        /// <summary>
        /// abstract add products from methods
        /// </summary>
        /// <param name="items"></param>
        /// <param name="type"></param>
        /// <param name="rate"></param>
        protected void AddProduct(IQueryable<object> items, string type, double rate = 1)
        {
            foreach (var i in items)
            {
                var item = i as Product;  //unbox
                ps.Add(new Product()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price * rate,
                    Type = type
                });
            }
        }
    }
}
