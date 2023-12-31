﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }

        public IProductRepository Product { get; }
    }
}
