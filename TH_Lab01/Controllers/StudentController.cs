using Microsoft.AspNetCore.Mvc;
using TH_Lab01.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TH_Lab01.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            if (!listStudents.Any())
            {
                listStudents.AddRange(new List<Student>()
                {
                    new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@g.com", DateOfBorth = new DateTime(2000, 1, 1) },
                    new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@g.com", DateOfBorth = new DateTime(2001, 2, 2) },
                    new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@g.com", DateOfBorth = new DateTime(2002, 3, 3) },
                    new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@g.com", DateOfBorth = new DateTime(2003, 4, 4) }
                });
            }
        }
        public ActionResult Index()
        {
            return View(listStudents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders = System.Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Student s,IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Lấy đường dẫn gốc của thư mục public (wwwroot)
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // Định nghĩa thư mục con để lưu ảnh sinh viên
                string uploadPath = Path.Combine(wwwRootPath, "images", "student");

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Tạo tên file duy nhất (dùng GUID) và giữ lại phần mở rộng (.jpg, .png)
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                // Lưu đường dẫn công khai (Public URL Path) vào Model
                s.ImagePath = "/images/student/" + fileName;

                // Copy file vào thư mục đích trên server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
            }
            s.Id = listStudents.LastOrDefault() != null ? listStudents.Last().Id + 1 : 101;
            listStudents.Add(s);
            return View("Index", listStudents);
        }
    }
}
