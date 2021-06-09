using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TripsandTravelSystem.DAL;

namespace TripsandTravelSystem.Bl
{
    public class TravalerServiceController : Controller
    {
        // GET: TravalerService
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();
        public JsonResult SendMail(String Email, string Name, string Massage,int PostId,int AgencyId)
        {
            try
            {
                var PostUrl = "https://localhost:44379/Post/WallPage/" + PostId;

                string Title = "You Recieved Question From "+ Name +"On your Post : " + PostUrl;
                var AgencyEmail = db.Agency.Where(a => a.AgencyId == AgencyId).Select(a => a.Email).FirstOrDefault();


                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("marcosameh678@gmail.com", "marco284633");

                smtp.Send("marcosameh678@gmail.com", AgencyEmail, Title, Massage +".  You Can Replay in This Question By Send Mail TO : " +Email);
                ViewBag.x = 1;
                return Json(ViewBag.x);
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }
    }
}