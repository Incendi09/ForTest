using Academy.DataAccess.Repository.IRepository;
using Academy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.Repository
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        private ApplicationDbContext _context;

        public LectureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Lecture obj)
        {
            _context.Lectures.Update(obj);
        }
    }
}
