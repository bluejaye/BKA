﻿using RefactorMe.DontRefactor.Data;
using RefactorMe.DontRefactor.Data.Implementation;
using RefactorMe.DontRefactor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorMe.Services.Implementation
{
    public class PhoneCaseService : BaseService<PhoneCase>
    {
        public PhoneCaseService(IReadOnlyRepository<PhoneCase> repository) : base(repository)
        {
        }

        public override IList<Product> List()
        {
            return Get()
                .Select(item => new Product { Id = item.Id, Name = item.Name, Price = item.Price, Type = "Phone Case" })
                .ToList();
        }
    }
}
