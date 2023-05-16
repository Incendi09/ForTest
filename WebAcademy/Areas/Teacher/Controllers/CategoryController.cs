using Academy.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Academy.Models;
using Academy.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Hosting;
using Academy.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAcademy.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            //return View(objCategoryList);
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
            CategoryVM categoryVM = new()
            {
                Category = new()
            };

            if (id == null || id == 0)
            {
                //create 
                return View(categoryVM);
            }
            else
            {
                //update

                var CategoryFromDbFirst = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == id);

                if (CategoryFromDbFirst == null)
                {
                    return NotFound();
                }

                return View(CategoryFromDbFirst);

            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj, IFormFile? file)
        {
            //checks validation of [Required] and so on
            if (obj.CategoryName == obj.Description)
            {
                ModelState.AddModelError("description", "The Description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();  //generates new file name
                    var uploads = Path.Combine(wwwRootPath, @"images\categories"); //combining paths to make full one
                    var extention = Path.GetExtension(file.FileName); // to keep same extention, so just rename a name, not extention

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.CategoryImageUrl = @"\image\categories\" + fileName + extention;
                }
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category createed successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _context.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _context.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbFirst = _context.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _context.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitOfWork.Category.GetAll();
            return Json(new { data = categoryList });
        }
        #endregion
    }
}
