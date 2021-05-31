using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel, string retunrUrl)
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                var userDetails = db.Users.
                    Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).
                    FirstOrDefault();
              
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong name or password";
                    return View("LoginIndex", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                   
                    return RedirectToAction("Index", "Administrator");  

                }
                
            }


        }
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("LoginIndex", "Login");
        }
    }
}