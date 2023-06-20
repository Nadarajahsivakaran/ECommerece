using ECommerce.Models;
using ECommerce.Repositories.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repositories.Repository
{
    public class CategoryRepopsitory : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepopsitory(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
