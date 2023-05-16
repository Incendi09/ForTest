using Academy.DataAccess.Repository.IRepository;
using Academy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.Repository
{

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Category obj)
        {
            var objFromDb = _context.Categories.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.CategoryName = obj.CategoryName;
                objFromDb.Description = obj.Description;
                objFromDb.UpdateDateTime = obj.UpdateDateTime;
                if (obj.CategoryImageUrl != null)
                {
                    objFromDb.CategoryImageUrl = obj.CategoryImageUrl;
                }

            }
        }
    }
}
