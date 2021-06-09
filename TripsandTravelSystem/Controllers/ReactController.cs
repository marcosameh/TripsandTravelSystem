using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsandTravelSystem.Bl.Repository;

namespace TripsandTravelSystem.Controllers
{
    public class ReactController : Controller
    {
        // GET: React
        ReactRep obj = new ReactRep();
        public ActionResult Like(int PostId, int TravalerId)
        {
            obj.Like(PostId, TravalerId);
            return Redirect(Request.UrlReferrer.ToString());

        }
        public ActionResult DisLike(int PostId, int TravalerId)
        {
            obj.DisLike(PostId, TravalerId);
            return Redirect(Request.UrlReferrer.ToString());

        }
        public ActionResult AddToFavourite(int PostId, int TravalerId)
        {
            obj.AddToFavourite(PostId, TravalerId);
            return Redirect(Request.UrlReferrer.ToString());

        }
        public ActionResult DeleteFromFavourite(int PostId, int TravalerId)
        {
            obj.DeleteFromFavourite(PostId, TravalerId);
            return Redirect(Request.UrlReferrer.ToString());

        }
    }
}