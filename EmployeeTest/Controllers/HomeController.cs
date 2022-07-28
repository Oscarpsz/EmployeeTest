using EmployeeTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmployeeTest.Data;

namespace EmployeeTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEmployees()
        {
            IEnumerable<Employee> employees = _db.t_employees;
            return Json(new {data = employees });
        }
        
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee obj)
        {
            var findRfc = _db.t_employees.Find(obj.rfc);
            if (findRfc != null)
            {
                ModelState.AddModelError("rfc", "The RFC already exists in the database");
            }
            if (ModelState.IsValid)
            {
                _db.t_employees.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The employee was added correctly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}