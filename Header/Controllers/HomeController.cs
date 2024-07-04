using Header.Data;
using Header.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Timers;


namespace Header.Controllers
{
	public class HomeController : Controller
	{
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }




   //     [HttpGet]
   //     public JsonResult GetHeaders()
   //     {
			//var headers = Request.Headers
   //                             .Select(header => new { Key = header.Key, Value = header.Value.ToString() })
   //                         .ToList();
   //       //  _logger.LogInformation("Headers: {@Headers}", headers);
   //         return Json(headers);
   //     }



        public IActionResult Index()
		{

            return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}



        [HttpPost]
        public IActionResult SaveBook([FromBody] Book obj)
        {
            if (obj != null) 
            {
                _db.books.Add(obj);
                _db.SaveChanges();
                return Json(new { success = true, message = "Book added successfully" });
            }
            var abc = Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }


        [HttpPost]
        public IActionResult SaveBookFromForm( Book obj)
        {
            if (!string.IsNullOrEmpty(obj.Title))
            {
                _db.books.Add(obj);
                _db.SaveChanges();
                return Json(new { success = true, message = "Book added successfully" });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }



        [HttpGet]
        public IActionResult GetBookById(Book obj)
        {
            
            var getbooks = _db.books.ToList();
          
            return Json(getbooks);

        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var AllBooks = _db.books.ToList();
            return Json(AllBooks);
        }



        [HttpGet]
        public IActionResult getBookByAuthorId([FromQuery] int AuthorId)
        {

            var getbooks = _db.books.Where( a=> a.AuthorId == AuthorId ).ToList();

            return Json(getbooks);

        }

        [HttpDelete]
        public IActionResult DeleteBookByAuthorId([FromRoute] int id)
        {

            var getbooks = _db.books.FirstOrDefault(a => a.BookId == id);
            if(getbooks != null)
            {
                _db.books.Remove(getbooks);
                _db.SaveChanges();
                return Json(new { success = true, message = "Book deleted successfully" });
            }

            else
            {
                return Json(new { success = false, message = "Failed to delete the book OR No book Found with ID" });
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

