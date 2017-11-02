using ExpenseTrackerDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login(string username, string password)
        {

            bool loggedIn = await ValidateLogin(username, password);

            if (loggedIn)
            {
                Session.Add("UserLoggedIn", true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["MessageText"] = "Username/password is invalid";
                TempData["MessageType"] = EnumMessageType.ERROR;
                return View("Index");
            }
        }

        public ActionResult Logoff()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            return View("Index");
        }




        private async Task<bool> ValidateLogin(string username, string password)
        {

            List<User> users = await base.GetItemListAsync<User>("Users");

            
            var user = users.Find(u => u.UserName == username && u.Password == password);
            
            if (user != null)
            {
                Session.Add("UserName", user.UserName);                
            }

            return user != null;
        }
    }
}