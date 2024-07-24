using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db) 
        { 
            _db = db; 
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description) 
            {
                ModelState.AddModelError("Name", "The Name and Description must not be same");
            }
            if (ModelState.IsValid) 
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            else { return View(obj); }

        }
        public IActionResult Update(int villaId)
        {
            Villa? Obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (Obj == null) { return RedirectToAction("Error", "Home"); }
            return View(Obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            else { return View(obj); }

        }

        public IActionResult Delete(int villaId)
        {
            Villa? Obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (Obj == null) { return RedirectToAction("Error", "Home"); }
            return View(Obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            else { return View(obj); }

        }

    }
}
