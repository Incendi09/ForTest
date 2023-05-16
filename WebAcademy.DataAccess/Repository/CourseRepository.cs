using Academy.DataAccess.Repository.IRepository;
using Academy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Course obj)
        {
            var objFromDb = _context.Courses.FirstOrDefault(u=>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.CourseName = obj.CourseName;
                objFromDb.ShortDescription = obj.ShortDescription;
                objFromDb.Semester = obj.Semester;
                objFromDb.UpdateDateTime = obj.UpdateDateTime;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Category = obj.Category;
                if (obj.CourseImageUrl !=null)
                {
                    objFromDb.CourseImageUrl = obj.CourseImageUrl;
                }

            }
            //_context.Courses.Update(obj);
        }
    }
}
