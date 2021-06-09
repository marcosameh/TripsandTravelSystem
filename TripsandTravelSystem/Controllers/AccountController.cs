using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TripsandTravelSystem.DAL;

namespace TripsandTravelSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();
        public ActionResult Login()
        {

           

            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            try
            {


                if (Membership.ValidateUser(admin.UserName, admin.Password))


                {

                    FormsAuthentication.SetAuthCookie(admin.UserName, false);
                    if (Roles.IsUserInRole(admin.UserName, "Admin"))
                    {
                        var data = db.Admin.FirstOrDefault(x => x.UserName.Equals(admin.UserName));

                        Session["photo"] = data.Photo;
                        Session["Name"] = data.UserName;
                        Session["Id"] = data.Id;
                        return RedirectToAction("Home", "Admin");
                    }
                    else if (Roles.IsUserInRole(admin.UserName,"Agency"))
                    { 

                     var data = db.Agency.FirstOrDefault(x => x.UserName.Equals(admin.UserName));
                    Session["photo"] = data.Photo;
                    Session["Name"] = data.UserName;
                    Session["Id"] = data.AgencyId;
                    
                        return RedirectToAction("Home","Agency");
                    }
                    else if (Roles.IsUserInRole(admin.UserName, "Traveler"))
                    {
                        var data = db.Travaler.FirstOrDefault(x => x.UserName.Equals(admin.UserName));

                        Session["photo"] = data.Photo;
                        Session["Name"] = data.UserName;
                        Session["Id"] = data.TravalerId;
                        
                        return RedirectToAction("WallPage", "Post",new { id=data.TravalerId});
                        
                    }
                    else
                    {
                        TempData["x"] = 1;
                        return View();
                    }



                }
                else
                {
                    TempData["x"] = 1;
                   
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            catch (Exception) {
                return View(ViewBag.x=1);
            }

        }
   

        public ActionResult SignOut()
        {
            Session["UserInfo"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("WallPage", "Post");

        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {


            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            var user = Membership.FindUsersByEmail(Email);
            if (user.Count!=0)


            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int code = _rdm.Next(_min, _max);

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("marcosameh678@gmail.com", "marco284633");
                
                smtp.Send("marcosameh678@gmail.com",Email, "Your Code For Reset Password", code.ToString());



                Session["Email"] = Email;
                Session["code"] = code;
                return RedirectToAction("Code");
            }




            else
            {
                ViewBag.x = 1;
                return View(ViewBag.x);
            }
        }
        [HttpGet]
        public ActionResult Code()
        {



            return View();
        }
        [HttpPost]
        public ActionResult Code(string Email,string UserCode, string RealCode)
        {
            if (UserCode == RealCode)
            {
                var Admin = db.Admin.FirstOrDefault(a => a.Email == Email);
                var Agency = db.Agency.FirstOrDefault(a => a.Email == Email);
                var Traveler = db.Travaler.FirstOrDefault(a => a.Email == Email);
                if (Admin != null)
                {



                    return RedirectToAction("Reset", "Admin", new { Id = Admin.Id });

                }
                else if (Agency != null)
                {


                    return RedirectToAction("Reset", "Agency", new { Id = Agency.AgencyId });
                }
                else if (Traveler != null)
                {


                    return RedirectToAction("Reset", "Travaler", new { Id = Traveler.TravalerId });
                }



                else
                {
                    return View();
                }
            }

            else
            {
                ViewBag.x = 1;
                return View(ViewBag.x);
            }


           
        }


    }
}