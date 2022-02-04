using DDAC_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DDAC_MVC_.Controllers
{
    public class ManageStaffController : Controller
    {
        public async Task<IActionResult> Index() //
        {
            IEnumerable<User> staff = null;
            List<User> staffList = new List<User>();

            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("managestaff");

            if (response.IsSuccessStatusCode)
            {
                staff = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

                foreach (User item in staff)  //loop through the collection of data
                {
                    if ((int)item.Type == 1)
                    {
                        staffList.Add(item);    //add into the empty list
                    }
                }

                return View(staffList); //display the list
            }
            else
            {
                TempData["error"] = "Error - Unable to load Articles";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
