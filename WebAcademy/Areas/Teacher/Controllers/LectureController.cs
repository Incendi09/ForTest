using Academy.DataAccess.Repository.IRepository;
using Academy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAcademy.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class LectureController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LectureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Lecture> objLectureList = _unitOfWork.Lecture.GetAll();
            return View(objLectureList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lecture obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Lecture.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Lecture created successfully";
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
            var lectureFromDbFirst = _unitOfWork.Lecture.GetFirstOrDefault(u => u.Id == id);

            if (lectureFromDbFirst == null)
            {
                return NotFound();
            }

            return View(lectureFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Lecture obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Lecture.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Lecture updated successfully";
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
            var LectureFromDbFirst = _unitOfWork.Lecture.GetFirstOrDefault(u => u.Id == id);

            if (LectureFromDbFirst == null)
            {
                return NotFound();
            }

            return View(LectureFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var LectureFromDbFirst = _unitOfWork.Lecture.GetFirstOrDefault(u => u.Id == id);
            if (LectureFromDbFirst == null)
            {
                return NotFound();
            }
            _unitOfWork.Lecture.Remove(LectureFromDbFirst);
            _unitOfWork.Save();
            TempData["success"] = "Lecture deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
