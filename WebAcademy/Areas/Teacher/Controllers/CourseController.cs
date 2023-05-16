using Academy.DataAccess.Repository;
using Academy.DataAccess.Repository.IRepository;
using Academy.Models;
using Academy.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAcademy.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CourseController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            CourseVM courseVM = new()
            {
                Course = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString(),
                })
            };

            if (id == null || id == 0)
            {
                //create 
                return View(courseVM);
            }
            else
            {
                //update

                var CouseFromDbFirst = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == id);

                if (CouseFromDbFirst == null)
                {
                    return NotFound();
                }

                return View(CouseFromDbFirst);

            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CourseVM obj, IFormFile? file)
        {
            //checks validation of[required] and so on
            if (obj.Course.CourseName == obj.Course.ShortDescription)
            {
                ModelState.AddModelError("description", "the description cannot exactly match the name.");
            }
            if (obj.Course.Semester < 1 || obj.Course.Semester > 7)
            {
                ModelState.AddModelError("description", "semester should be between 1 and 7");
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();  //generates new file name
                    var uploads = Path.Combine(wwwRootPath, @"images\courses"); //combining paths to make full one
                    var extention = Path.GetExtension(file.FileName); // to keep same extention, so just rename a name, not extention

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Course.CourseImageUrl = @"\image\courses\" + fileName + extention;
                }
                _unitOfWork.Course.Add(obj.Course);
                _unitOfWork.Save();
                TempData["success"] = "Course createed successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var courseFromDb = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == id);

            if (courseFromDb == null)
            {
                return NotFound();
            }

            return View(courseFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var courseFromDb = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == id);
            if (courseFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Course.Remove(courseFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Course deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var courseList = _unitOfWork.Course.GetAll();
            return Json(new { data = courseList });
        }
        #endregion

    }
}
