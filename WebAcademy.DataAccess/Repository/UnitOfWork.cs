using Academy.DataAccess.Repository.IRepository;
using Academy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _context;

        public ICategoryRepository Category { get; private set; }
        public ICourseRepository Course { get; private set; }
        public ILectureRepository Lecture { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Course = new CourseRepository(_context);
            Lecture = new LectureRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
