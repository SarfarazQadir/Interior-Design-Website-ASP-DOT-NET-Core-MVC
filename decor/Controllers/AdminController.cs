
using decor.Models;
using DecorVista.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace decor.Controllers
{
    public class AdminController : Controller
    {
        private Mycontext _mycon;
        private IWebHostEnvironment _env;

        public AdminController(Mycontext mycon , IWebHostEnvironment env)
        {
            _mycon = mycon;
            _env = env;
        }

        public IActionResult Index1()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string adminEmail, string adminPassword)
        {
            var row = _mycon.tbl_admin.FirstOrDefault(a => a.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword)
            {
                HttpContext.Session.SetString("admin_session", row.admin_id.ToString());
                return View("Index1");
            }
            else
            {
                ViewBag.message = "Incorrect Username Or Password";
                return View();
            }
        }

        public IActionResult Profile()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                var adm = HttpContext.Session.GetString("admin_session");
                var data = _mycon.tbl_admin.Where(a => a.admin_id == int.Parse(adm)).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("login");
            }

        }
        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            _mycon.tbl_admin.Update(admin);
            _mycon.SaveChanges();
            return RedirectToAction("profile");
        }
        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile admin_image, Admin admin)
        {
            //  string ImagePath = Path.Combine(_env.WebRootPath, "/Adminassests/AdminImages", admin_image.FileName);
            string ImagePath = Path.Combine(_env.WebRootPath, "Adminassests", "AdminImages", admin_image.FileName);
            string directoryPath = Path.Combine(_env.WebRootPath, "Adminassests", "AdminImages");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            try
            {
                using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
                {
                    admin_image.CopyTo(fs);
                }
                admin.admin_image = admin_image.FileName;
                _mycon.tbl_admin.Update(admin);
                _mycon.SaveChanges();
                return RedirectToAction("profile");
            }
            catch (Exception ex)
            {
                // Log or handle the error
                Console.WriteLine($"Error saving image: {ex.Message}");
            }
            return RedirectToAction("profile");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("login");
        }

        public IActionResult Index(User use)
        {
           var std = _mycon.tbl_users.ToList();
            return View(std);
            
        }

        public IActionResult DetailUser(int id)
        {
            return View(_mycon.tbl_users.FirstOrDefault(c => c.user_id == id));
        }


        public IActionResult DeleteUser(int id)
        {
            var user = _mycon.tbl_users.Find(id);
            _mycon.tbl_users.Remove(user);
            _mycon.SaveChanges();
            return RedirectToAction("FetchUser");
        }

        public IActionResult FetchDesigner()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycon.tbl_designer.ToList());

            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult DetailDesigner(int id)
        {
            return View(_mycon.tbl_designer.FirstOrDefault(c => c.designer_id == id));
        }
        public IActionResult UpdateDesigner(int id)
        {
            return View(_mycon.tbl_designer.Find(id));
        }

        [HttpPost]
        public IActionResult UpdateDesigner(Designer designer, IFormFile designer_image)
        {
            if (designer_image == null)
            {
                /*                HttpContext.Session.SetString("product_session", row.product_id.ToString());
                */
                designer.designer_image = "ddd";

                _mycon.tbl_designer.Update(designer);
                _mycon.SaveChanges();

                return RedirectToAction("FetchDesigner");
            }
            else
            {
                string ImagePath = Path.Combine(_env.WebRootPath, "Adminassests", "DesignerImages", designer_image.FileName);

                using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
                {
                    designer_image.CopyTo(fs);
                }
                designer.designer_image = designer_image.FileName;
                _mycon.tbl_designer.Update(designer);
                _mycon.SaveChanges();
                return RedirectToAction("FetchDesigner");
            }
        }

        public IActionResult DeleteDesigner(int id)
        {
            var designer = _mycon.tbl_designer.Find(id);
            _mycon.tbl_designer.Remove(designer);
            _mycon.SaveChanges();
            return RedirectToAction("FetchDesigner");
        }

        public IActionResult FetchCategory()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycon.tbl_category.ToList());
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult AddCategory()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]

        public IActionResult AddCategory(Category category)
        {
            _mycon.tbl_category.Add(category);
            _mycon.SaveChanges();
            return RedirectToAction("FetchCategory");

        }

        public IActionResult UpdateCategory(int id)
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycon.tbl_category.Find(id));
            }
            else
            {
                return RedirectToAction("login");
            }
            
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category cat)
        {
            _mycon.tbl_category.Update(cat);
            _mycon.SaveChanges();
            return RedirectToAction("FetchCategory");
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _mycon.tbl_category.Find(id);
            _mycon.tbl_category.Remove(category);
            _mycon.SaveChanges();
            return RedirectToAction("FetchCategory");
        }

        public IActionResult FetchProduct()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycon.tbl_product.ToList());
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult DetailProduct(int id)
        {
            return View(_mycon.tbl_product.Include(p => p.Category).FirstOrDefault
                (p => p.product_id == id));
        }
        public IActionResult AddProduct()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                List<Category> categories = _mycon.tbl_category.ToList();
                ViewData["category"] = categories;
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        public IActionResult AddProduct(Product prd, IFormFile product_image)
        {
            string ImageName = Path.GetFileName(product_image.FileName);
            string ImagePath = Path.Combine(_env.WebRootPath, "Adminassests","ProductImages", ImageName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                product_image.CopyTo(fs);
            }
            prd.product_image = ImageName;
            _mycon.tbl_product.Add(prd);
            _mycon.SaveChanges();
            return RedirectToAction("FetchProduct");
        }

        public IActionResult UpdateProduct(int id)
        {
            List<Category> categories = _mycon.tbl_category.ToList();
            ViewData["category"] = categories;

            var product = _mycon.tbl_product.Find(id);

            ViewBag.SelectedCategoryId = product.category_id;
            return View(product);
        }
        [HttpPost]
        
        public IActionResult UpdateProduct(IFormFile product_image, Product prd)
        {
            /*            var row = _mycon.tbl_product.FirstOrDefault(p => p.product_image == product_image);
            */
            if (product_image ==null)
            {
                /*                HttpContext.Session.SetString("product_session", row.product_id.ToString());
                */
                prd.product_image = "ddd";

                _mycon.tbl_product.Update(prd);
                _mycon.SaveChanges();

                return RedirectToAction("FetchProduct");
            }
            else
            {
                string ImagePath = Path.Combine(_env.WebRootPath, "Adminassests","ProductImages", product_image.FileName);

                using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
                {
                    product_image.CopyTo(fs);
                }

                prd.product_image = product_image.FileName;

                _mycon.tbl_product.Update(prd);
                _mycon.SaveChanges();

                return RedirectToAction("FetchProduct");
            }
        }
        public IActionResult DeleteProduct(int id)
        {
            var product = _mycon.tbl_product.Find(id);
            _mycon.tbl_product.Remove(product);
            _mycon.SaveChanges();
            return RedirectToAction("FetchProduct");
        }

        public IActionResult FetchBlog()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View(_mycon.tbl_bolg.ToList());
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public IActionResult DetailBlog(int id)
        {
            return View(_mycon.tbl_bolg.FirstOrDefault(b => b.blog_id == id));
        }
        public IActionResult AddBlog()
        {
            var admin = HttpContext.Session.GetString("admin_session");
            if (admin != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        public IActionResult AddBlog(Blogs blog, IFormFile blog_article_image)
        {
            string ImageName = Path.GetFileName(blog_article_image.FileName);
            string ImagePath = Path.Combine(_env.WebRootPath, "BlogImages", ImageName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                blog_article_image.CopyTo(fs);
            }
            blog.blog_article_image = blog_article_image.FileName;
            _mycon.tbl_bolg.Add(blog);
            _mycon.SaveChanges();
            return RedirectToAction("FetchBlog");
        }
        public IActionResult UpdateBlog(int id)
        {
            var blog = _mycon.tbl_bolg.Find(id);
            return View(blog);
        }
        [HttpPost]
        public IActionResult UpdateBlog(IFormFile blog_article_image, Blogs blog)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "BlogImages", blog_article_image.FileName);

            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                blog_article_image.CopyTo(fs);
            }

            blog.blog_article_image = blog_article_image.FileName;

            _mycon.tbl_bolg.Update(blog);
            _mycon.SaveChanges();

            return RedirectToAction("FetchBlog");
        }
        public IActionResult DeleteBlog(int id)
        {
            var blog = _mycon.tbl_bolg.Find(id);
            _mycon.tbl_bolg.Remove(blog);
            _mycon.SaveChanges();
            return RedirectToAction("FetchBlog");
        }



    }
}
