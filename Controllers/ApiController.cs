using Microsoft.AspNetCore.Mvc;
using Msit147Site.Models;

namespace Msit147Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;
        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public IActionResult Index(string name, int age = 20)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Guest";
            }

            return Content($"Hello {name}, You are {age} years old.");
            // return Content("<h2>Hello World!!</h2>","text/html");
            //return Content("<h2>Ajax 您好!!</h2>", "text/html", System.Text.Encoding.UTF8);

           // return Content("Hello World!!", "application/msword");          
                
        }

        public IActionResult AjaxEvent(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                userName = "Guest";
            }
            System.Threading.Thread.Sleep(10000);

            return Content($"Hello {userName}!!");
        }

        [HttpPost]
        public IActionResult Register(Members member, IFormFile Photo) {
            // string photoInfo = $"{Photo.FileName} - {Photo.Length} - {Photo.ContentType}";
            //檔案上傳
            string rootPath = Path.Combine(_host.WebRootPath, "uploads", Photo.FileName); //C:\Users\User\Documents\Ajax\Msit147Site\wwwroot
            using(var fileStream = new FileStream(rootPath, FileMode.Create))
            {
                Photo.CopyTo(fileStream);
            }

            //寫進資料庫
            member.FileName = Photo.FileName;
            byte[]? photoByte = null;
            using (var memoryStream = new MemoryStream())
            {
                Photo.CopyTo(memoryStream);
                photoByte = memoryStream.ToArray();
            }
                member.FileData = photoByte;

            _context.Members.Add(member);
            _context.SaveChanges();


            return Content(rootPath);
        }

       public IActionResult GetImageByte(int id=0)
        {
            Members? _member = _context.Members.Find(id);
            byte[]? img = _member.FileData;
            return File(img, "image/jpeg");



        }
        
        //載入縣市
        public IActionResult Cities() {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        
        }
        //根據縣市載入鄉鎮區
        public IActionResult Districts(string city) { 
           var districts = _context.Address.Where(a=>a.City == city).Select(a=>a.SiteId).Distinct();
            return Json(districts);

        }
        //根據鄉鎮區載入路名
        public IActionResult Roads(string SiteId) {
            var roads = _context.Address.Where(a => a.SiteId == SiteId).Select(a => a.Road);
            return Json(roads);
        }
    }
}
