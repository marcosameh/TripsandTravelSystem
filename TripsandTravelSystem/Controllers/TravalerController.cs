using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsandTravelSystem.Bl.Repository;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Controllers
{
    public class TravalerController : Controller
    {
        TravalerRep obj = new TravalerRep();
        // GET: Travaler
        [Authorize(Roles ="Admin")]
        public ActionResult Display()
        {
            var data = obj.Display();
            return View(data);
        }
        [HttpGet]
        public ActionResult AddTravaler()
        {

            
            return View();
        }
        [HttpPost]
        public ActionResult AddTravaler(TravalerVM t,HttpPostedFileBase Photo)
        {
            obj.AddTravaler(t,Photo);
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Display");
            }
            else
            return RedirectToAction("Login","Account");
        }
        [HttpGet]
       [Authorize(Roles ="Admin")]
        public ActionResult DeleteTravaler(int Id)
        {

            var data = obj.GetTravalerById(Id);
            return View(data);
        }
        [HttpPost]
        public ActionResult DeleteTravaler(int?Id)
        {
            obj.Delete(Id);
            return RedirectToAction("Display");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditTravaler(int Id)
        {
            var data = obj.GetTravalerById(Id);
            return View(data);
        }
        [HttpPost]
        public ActionResult EditTravaler(TravalerVM t1,HttpPostedFileBase Photo)
        {
            obj.Edit(t1, Photo);
            return RedirectToAction("Display");
        }
        [HttpGet]

        public ActionResult Reset(int Id)
        {


            var data = obj.GetTravalerById(Id);


            return View(data);
        }
        [HttpPost]
        public ActionResult Reset(TravalerVM A)
        {


            obj.Reset(A);

            return RedirectToAction("Login", "Account");
        }
    }
}