using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsandTravelSystem.Bl.Repository;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Controllers
{
    public class AgencyController : Controller
    {
        AgencyRep obj = new AgencyRep();
        // GET: Agency
        public ActionResult Home()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Display()
        {

            var data=obj.GetAllAency();
            return View(data);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(AgencyVM agency, HttpPostedFileBase Photo)
        {
            obj.AddAgency(agency,Photo);
            return RedirectToAction("Home", "Admin");
        }
        [Authorize(Roles ="Agency")]
        public ActionResult AgencyProfile(int Id)
        {

            var agency=obj.GetAgencyById(Id);
            return View(agency);

        }
        [HttpGet]
        [Authorize(Roles ="Agency")]
        public ActionResult Edit(int Id)
        {

            var agency = obj.GetAgencyById(Id);
            return View(agency);

        }
        [HttpPost]
        [Authorize(Roles = "Agency")]
        public ActionResult Edit(AgencyVM A,HttpPostedFileBase Photo)
        {

            obj.Edit(A,Photo);
            return RedirectToAction("Home", "Agency");

        }
        [HttpGet]

        public ActionResult Reset(int Id)
        {


            var data = obj.GetAgencyById(Id);


            return View(data);
        }
        [HttpPost]
        public ActionResult Reset(AgencyVM A)
        {


            obj.Reset(A);

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditByAdmin(int Id)
        {


            var agency = obj.GetAgencyById(Id);
            return View(agency);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditByAdmin(AgencyVM A, HttpPostedFileBase Photo)
        {

            obj.Edit(A, Photo);
            return RedirectToAction("Display", "Agency");

        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int Id)
        {    
                  var data = obj.GetAgencyById(Id);
                 
                 return View(data);
        }
           [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            obj.Delete(Id);
            return RedirectToAction("Display", "Agency");
        }

    }
}