using AspStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspStudy.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private CustomMemberShipProvider member = new CustomMemberShipProvider();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,Password")] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (member.ValidateUser(login.UserName, login.Password))
                {
                    ViewBag.Message = "";
                    FormsAuthentication.SetAuthCookie(login.UserName, false);
                    return RedirectToAction("Index", "Todoes");
                }
            }

            ViewBag.Message = "ログインできませんでした。";
            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            ViewBag.Message = "";
            return RedirectToAction("Index");
        }
    }
}