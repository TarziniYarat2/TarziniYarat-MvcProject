using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TarziniYarat.BusinessLogic.Abstract;
using TarziniYarat.Model;
using TarziniYarat.UI.MVC.Models;
//using TarziniYarat.UI.MVC.Views;

namespace TarziniYarat.UI.MVC.Controllers
{
    
    public class SitesController : Controller
    {
        
        IPersonService _personService;
        IRoleService _roleService;
        IProductService _productService;
        ICategoryService _categoryService;
        IBrandService _brandService;
        public SitesController(IPersonService personService, IRoleService roleService, IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            _personService = personService;
            _roleService = roleService;
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public ActionResult HomePage()
        {
            return View(_productService.GetAll());
        }
        public ActionResult Home(int? personID, Product model)
        {
            List<Product> models = new List<Product>();
            foreach (Product item in _productService.GetAll())
            {
                models.Add(new Product()
                {
                    Photo = item.Photo,
                    UnitPrice = item.UnitPrice,
                    ProductName = item.ProductName
                });
            }
            ViewBag.Product = models;
            return RedirectToAction("HomePage", "Sites", new { id = personID });
        }
       
        public ActionResult Shop(int catID=0, int brandID=0)
        {
            List<Category> category = _categoryService.GetAll();
            List<Brand> brands = _brandService.GetAll();
            Size[] sizes = (Size[])Enum.GetValues(typeof(Size));
            Color[] colors = (Color[])Enum.GetValues(typeof(Color));
            ViewBag.Color = colors;
            ViewBag.Size = sizes;
            ViewBag.Brand = brands;
            ViewBag.Category = category;
            if (catID!=0)
            {
                return View(_productService.GetAllByCategory(catID));
            }
            else if (brandID!=0)
            {
                return View(_productService.GetAllByBrandId(brandID));
            }
            else if (brandID!=0 && catID!=0)
            {
                return View(_productService.GetAllCatIdBrandId(catID, brandID));
            }
            return View(_productService.GetAll());
        }

        public ActionResult ProductDetail(int id)
        {
            //ViewBag.Product = _productService.GetByID(id);
            return View(_productService.GetByID(id));
        }
        public ActionResult Combine()
        {
            return View();
        }
        public ActionResult CreateCombine()
        {
            return View();
        }
        public ActionResult ViewCombine()
        {
            return View();
        }
        public ActionResult AllViewCombine()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogDetail()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Card()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            List<Person> persons = _personService.GetAll();
            foreach (var item in persons)
            {
                if (login.UserName == item.Username && login.Password == item.Password)
                {
                    if (login.UserName== "thelastdance@mail.com" && login.Password=="123456")
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin", id = item.PersonID });
                    }
                    else if (item.RoleID==2 && item.IsActive==true)
                    {
                        return RedirectToAction("HomePage", "Sites", new { id = item.PersonID });
                    }
                    else if (item.RoleID==3 && item.IsActive==true)
                    {
                        return RedirectToAction("Home", "Sites", new  { Username = item.Username, PersonID = item.PersonID });
                    }
                }
                else
                {
                    continue;
                }
            }
            ViewBag.Hata = "Kullanıcı Bilgilerinizi Kontrol Ediniz.";
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person();
                person.Name = model.Name;
                person.Surname = model.Surname;
                person.Username = model.UserName;
                person.Password = model.Password;
                person.BirthDate = model.BirthDate;
                person.TCKN = model.TCKN;
                person.RoleID = 4;
                try
                {
                    _personService.Add(person);
                    return RedirectToAction("WaitPage");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ViewBag.Hata = "Bilgilerinizi kontrol ediniz. " +
                    "Şifreniz en az 6 karakterli olmalı. En az 1 sayı ve 1 harf içermelidir. " +
                    "Kimlik numaranız 11 rakamdan az olamaz.";
                //ModelState.AddModelError("", "Girdiğiniz bilgileri kontrol ediniz");
            }
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult WaitPage()
        {
            return View();
        }
    }
}