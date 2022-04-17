using CodeFirstApp.Context;
using CodeFirstApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodeFirstApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserDbContext _data;

        public HomeController(UserDbContext context)
        {
            _data = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Table()
        {
            List<ModelApp> model = new List<ModelApp>();
            var re = _data.Ulogins.ToList();
            foreach (var item in re)
            {
                model.Add(new ModelApp
                {
                    Id= item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    password=item.password,
                    Mobile = item.Mobile

                });

            }

            return View(model);
        }
        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNew(ModelApp user)
        {
            UserLogin table = new UserLogin();
            table.Id = user.Id;
            table.Name = user.Name;
            table.Email = user.Email;
            table.password = user.password;
            table.Mobile = user.Mobile;
            if (user.Id == 0)
            {
                _data.Ulogins.Add(table);
                _data.SaveChanges();
            }
            else
            {
                _data.Ulogins.Update(table);
                _data.SaveChanges();
            }

            return RedirectToAction("Table");
        }

        public IActionResult Edit(int id)
        {
            ModelApp model = new ModelApp();
            var edit = _data.Ulogins.Where(m => m.Id == id).First();
            model.Id = edit.Id;
            model.Name = edit.Name;
            model.Email = edit.Email;
            model.password = edit.password;
            model.Mobile = edit.Mobile;
            return View("AddNew",model);

        }
        public IActionResult Delete(int id)
        {
            var del = _data.Ulogins.Where(m => m.Id == id).FirstOrDefault();
            _data.Ulogins.Remove(del);
            _data.SaveChanges();

            return RedirectToAction("Table");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LoginPage(ModelApp model)
        {
            
            var table=_data.Ulogins.Where(m => m.Email == model.Email).FirstOrDefault();
            if (table == null)
            {
                TempData["email"] = "Invalid E-mail or Phone Number!";
            }
            else
            {
                if(table.password==model.password)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, table.Name), new Claim(ClaimTypes.Email, table.Email) };
                    var identity = new ClaimsIdentity(new[] { new (ClaimTypes.Name, table.Name),new Claim(ClaimTypes.Email, table.Email) },CookieAuthenticationDefaults.AuthenticationScheme);
                    var authpro = new AuthenticationProperties { IsPersistent = true };
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    HttpContext.Session.SetString("Name", table.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pass"] = "Invalid Password!";
                }
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginPage");
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
