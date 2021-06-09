using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsandTravelSystem.Bl.Repository;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Controllers
{
    public class AdminController : Controller
    {
        AdminRep obj = new AdminRep();
      
        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult Home()
        {

            return View();
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create(AdminVM A, HttpPostedFileBase Photo)
        {
            obj.AddAdmin(A, Photo);

            return RedirectToAction("Home", "Admin");
        }
        public ActionResult ProfilePage(int Id)
        {
           var data= obj.GetAdminById(Id);

            return View(data);
        }
        [HttpGet]
       
        public ActionResult Reset(int Id)
        {


            var data = obj.GetAdminById(Id);
          

            return View(data);
        }
        [HttpPost]
        public ActionResult Reset(AdminVM A)
        {

            
            obj.Reset(A);

            return RedirectToAction("Login", "Account");
        }

    


    }
}