using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsandTravelSystem.Bl.Repository;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Controllers
{
    public class PostController : Controller
    {
        PostRep obj = new PostRep();
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles ="Agency")]
        public ActionResult CreatePost()
        {

            
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Agency")]
        public ActionResult CreatePost(TripPostVM p,HttpPostedFileBase Photo)
        {
            

            obj.AddNewPost(p, Photo);
            return RedirectToAction("Home", "Agency");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult WaitingPosts()
        {
            
            var data = obj.WaitingPosts();
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Approve(int Id)
        {
            obj.Approve(Id);
            return RedirectToAction("WaitingPosts");
        }
        [Authorize(Roles = "Admin,Agency")]

        public ActionResult Remove(int id)
        {
            obj.Remove(id);
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("WaitingPosts");
            }else
            {
                return RedirectToAction("AgencyHistory", "Post");
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ApproveALL()
        {
            obj.ApproveAll();
            return RedirectToAction("WaitingPosts");
        }
        public ActionResult ShowDetials(int Id, int? TravalerId)
        {

           var data= obj.GetPostById(Id,TravalerId);
            return View(data);
        }
        [Authorize(Roles ="Agency")]
        public ActionResult AgencyHistory(int Id)
        {

            var data = obj.GetPostsByAgencyId(Id);
            return View(data);
        }
        [HttpGet]
        public ActionResult EditPost(int Id, int? TravalerId)
        {


            var data = obj.GetPostById(Id, TravalerId);
            return View(data);
        }
        [HttpPost]
        public ActionResult EditPost(TripPostVM p,HttpPostedFileBase Photo)
        {

             obj.EditPost(p,Photo);
            return RedirectToAction("ShowDetials", "Post", new { Id = p.TripId });
        }
        [HttpGet]
        public ActionResult WallPage(int?Id)
        {

            var data = obj.WallPage(Id);

            return View(data);
        }
        [HttpGet]
        public ActionResult SearchPrice(int Price, int? Id)
        {

            var data = obj.SearchPrice(Price,Id);
            return View(data);
        }
        public ActionResult SearchAgencyName(string AgencyName, int? Id)
        {

            var data = obj.SearchAgencyName(AgencyName,Id);
            return View(data);
        }
        public ActionResult SearchTripDate(string TripDate, int? Id)
        {

            var data = obj.SearchTripDate(TripDate,Id);
            return View(data);
        }
        public ActionResult FavouritePosts(int Id)
        {

            var data = obj.FavouritePosts(Id);
            return View(data);
        }
    }
}