using decor.Models;
using DecorVista.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace decor.Controllers
{
    public class HomeController : Controller
    {
        private Mycontext _mycon;
        private IWebHostEnvironment _env;

        public HomeController(Mycontext mycon,  IWebHostEnvironment env)
        {
            _mycon = mycon;
            _env = env;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(User users, IFormFile user_image)
        {
            string ImagePath = Path.Combine(_env.WebRootPath, "Adminassests", "Userimages", user_image.FileName);
            using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
            {
                user_image.CopyTo(fs);
            }
            users.user_image = user_image.FileName;
            _mycon.tbl_users.Add(users);

            _mycon.SaveChanges();
            
            return View();
        }

        [HttpPost]

        public IActionResult Userlogin(string username , string password)
        {
            var row = _mycon.tbl_users.FirstOrDefault(a => a.user_name == username);
            if (row != null && row.user_password == password)
            {
                HttpContext.Session.SetString("mysession", row.user_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return RedirectToAction("Register");

            }
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Shop(Product pr)
        {
            var red = _mycon.tbl_product.ToList();
            return View(red);
        }

        public IActionResult UserAccount()
        {
            var id = HttpContext.Session.GetString("mysession");
            var data = _mycon.tbl_users.Where(u => u.user_id == int.Parse(id)).ToList();
            return View(data);
        }

        public IActionResult Productdetail(int id)
        {
            var row = _mycon.tbl_product.FirstOrDefault(c => c.product_id == id);
            return View(row);
        }

        public IActionResult Addtocart(int id)
        {
            var data = HttpContext.Session.GetString("mysession");
            List<User> users = _mycon.tbl_users.Where(c => c.user_id == int.Parse(data)).ToList();
            ViewData["users"] = users;
            var thor = _mycon.tbl_product.FirstOrDefault(c => c.product_id == id);
            
            return View(thor);
        }

        [HttpPost]

        public IActionResult Addtocart(Cart car)
        {
            _mycon.tbl_cart.Add(car);
            _mycon.SaveChanges();
            return RedirectToAction("cart");
            
        }
        public IActionResult cart(Cart cars)
        {
            var data = HttpContext.Session.GetString("mysession");
            List<User> users = _mycon.tbl_users.Where(c => c.user_id == int.Parse(data)).ToList();
            ViewData["users"] = users;
            var car = _mycon.tbl_cart.ToList();
            return View(car);
        }

        public IActionResult Checkout(int id)
        {
            var data = HttpContext.Session.GetString("mysession");
            List<User> users = _mycon.tbl_users.Where(c => c.user_id == int.Parse(data)).ToList();
            ViewData["users"] = users;
            var thor = _mycon.tbl_cart.FirstOrDefault(c => c.Cart_id == id);
            return View(thor);
        }

        [HttpPost]

        public IActionResult Checkout(Checkout check)
        {
            _mycon.tbl_checkout.Add(check);
            _mycon.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult User_email()
        {
            var std = HttpContext.Session.GetString("mysession");
            var tt = _mycon.tbl_users.Where(c => c.user_id == int.Parse(std)).ToList();
            return View(tt);
        }

        public IActionResult DesignerRegister()
        {
            return View();
        }

        [HttpPost]

        public IActionResult DesignerRegister(Designer designer, IFormFile designer_image)
        {
            //  string ImagePath = Path.Combine(_env.WebRootPath, "/Adminassests/AdminImages", admin_image.FileName);
            string ImagePath = Path.Combine(_env.WebRootPath, "Adminassests", "DesignerImages", designer_image.FileName);
            string directoryPath = Path.Combine(_env.WebRootPath, "Adminassests", "DesignerImages");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            try
            {
                using (FileStream fs = new FileStream(ImagePath, FileMode.Create))
                {
                    designer_image.CopyTo(fs);
                }
                designer.designer_image = designer_image.FileName;
                _mycon.tbl_designer.Add(designer);
                _mycon.SaveChanges();
                return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                // Log or handle the error
                Console.WriteLine($"Error saving image: {ex.Message}");
            }
            return RedirectToAction("Register");
        }

        [HttpPost]

        public IActionResult Designerlogin(string username, string password)
        {
            var row = _mycon.tbl_designer.FirstOrDefault(a => a.designer_first_name == username);
            if (row != null && row.designer_password == password)
            {
                HttpContext.Session.SetString("mysession", row.designer_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return RedirectToAction("Register");

            }
        }

    
        public IActionResult FetchBlog(Blogs blog)
    {
            var data = _mycon.tbl_bolg.ToList();
            return View(data);
    }

        public IActionResult Servic()
        {
            
            return View();
        }

    }
}

  
