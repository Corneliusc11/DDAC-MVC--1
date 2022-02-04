using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDAC_MVC_.Models;
using System.Web;
namespace DDAC_MVC_.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Login()
        //{
        //    return RedirectToAction("Index", "Login");
        //}

        public async Task<IActionResult> Login(string username,string password) //
        {
           

            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("user/login?username="+username+"&password="+password);

            if (response.IsSuccessStatusCode)
            {
              
               var user = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
                foreach(var item in user)
                {
                    TempData["id"] = item.Id;
                    TempData["username"] = item.Username;
                    TempData["role"] = item.Type;
                }
                HttpResponseMessage result = await GlobalVariables.WebApiClient.GetAsync("useraccesslog?userid="+ TempData["id"]);
                TempData["sessionid"] = result.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Index", "Home");



            }
            else
            {
                TempData["error"] = "Error - Unable to load Articles";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
