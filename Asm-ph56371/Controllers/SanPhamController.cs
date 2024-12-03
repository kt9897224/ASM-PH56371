using Asm_ph56371.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asm_ph56371.Controllers
{
    [Authorize (Roles ="Admin")]
    public class SanPhamController : Controller
    {
        public AsmDbContext _db;
        public SanPhamController(AsmDbContext db)
        {
            _db = db;
        }
        // GET: SanPhamController
        public ActionResult Index(int page = 1)
        {
            int pageSize = 3;  // Số sản phẩm mỗi trang
            var skip = (page - 1) * pageSize;

            // Lấy sản phẩm cho trang hiện tại
            var spList = _db.SanPhams.Skip(skip).Take(pageSize).ToList();

            // Tính tổng số sản phẩm
            var totalItems = _db.SanPhams.Count();

            // Tính tổng số trang
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Truyền dữ liệu vào View
            ViewBag.SpList = spList;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View();
        }


        // GET: SanPhamController/Details/5
        public ActionResult Details(Guid id)
        {
            var spDetails = _db.SanPhams.Find(id);
            return View(spDetails);
        }

        // GET: SanPhamController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: SanPhamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sp, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    // Tạo tên file ảnh duy nhất để tránh trùng lặp
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    // Kiểm tra và tạo thư mục nếu chưa tồn tại
                    string directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Lưu ảnh vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    // Cập nhật đường dẫn ảnh vào thuộc tính IMGURL của sản phẩm
                    sp.IMGURL = fileName;
                }
                else
                {
                    sp.IMGURL = "default.jpg"; // Nếu không có ảnh, gán ảnh mặc định
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                _db.SanPhams.Add(sp);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sp);
        }


        // GET: SanPhamController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var spEdit = _db .SanPhams.Find(id);
            if (spEdit == null)
            {
                return NotFound();
            }
            return View(spEdit);
        }

        // POST: SanPhamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormFile image, SanPham sp )
        {
            var spEdit = _db.SanPhams.Find(id);
           if(spEdit == null)
            {
                return NotFound();
            }
           spEdit.TenSanPham = sp.TenSanPham;
            spEdit.Gia = sp.Gia;
            spEdit.SoLuongTonKho = sp.SoLuongTonKho;
            spEdit.MoTa = sp.MoTa;

            if (image != null && image.Length > 0)
            {
                //định nghĩa đường dẫn để lưu file 
                //Lấy cái dữ liệu ảnh ở trên web 

                string fileName = Path.GetFileName(image.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", image.FileName);
                //kiểm tra và tạo thưu mục nếu chưa tồn tại 
                string diretory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(diretory))// check tồn tại: Exists
                {
                    Directory.CreateDirectory(diretory);
                }
                //Lưu ảnh mới 
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                //Cập nhật 
                spEdit.IMGURL = fileName;
            }
            _db.Update(spEdit);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SanPhamController/Delete/5
        
    }
}
