using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorMe.DontRefactor.Models;
using RefactorMe.DontRefactor.Data.Implementation;

namespace RefactorMe
{
    /// <summary>
    /// Demo of adding extra features 
    /// </summary>
    public class ProductDataConsolidatorExtra : ProductDataConsolidator
    {
        protected IQueryable<TShirt> t2;
        public ProductDataConsolidatorExtra() : base()
        {
            t2 = new TShirtRepository().GetAll();
        }

        /// <summary>
        /// extend extra API
        /// </summary>
        public List<Product> GetInNZDollars()
        {
            return GetByRate(0.36);
        }

        /// <summary>
        /// open for adding or removing items
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        protected override List<Product> GetByRate(double rate)
        {
            ps.Clear();

            AddProduct(l, "Lawnmower", rate);
            AddProduct(p, "Phone Case", rate);
            AddProduct(t, "T-Shirt", rate);
            AddProduct(t2, "T-Shirt2", rate);   //add t2

            return ps;
        }
    }
}
