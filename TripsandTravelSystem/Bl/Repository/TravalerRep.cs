using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using TripsandTravelSystem.Bl.Interface;
using TripsandTravelSystem.DAL;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Bl.Repository
{
    
    public class TravalerRep: ITravalerRep
    {
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();
        public IQueryable<TravalerVM> Display()
        {
            var travaler = db.Travaler.Select(a => new TravalerVM
            {
                TravalerId = a.TravalerId,
                Password = a.Password,
                ConfirmPassword = a.ConfirmPassword,
                Email = a.Email,
                Photo = a.Photo,
                UserName = a.UserName
                

            });
            return (travaler);
        }
        public void AddTravaler(TravalerVM travaler, HttpPostedFileBase Photo)
        {

            {
                if (Photo != null)
                {
                    string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");
                    string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
                    string fullpath = Path.Combine(Folderpath, photoname);
                    Photo.SaveAs(fullpath);
                  travaler.Photo = photoname;
                }
                var Travaler = new Travaler();
                Travaler.UserName = travaler.UserName;
                Travaler.Email = travaler.Email;
                Travaler.Password = travaler.Password;
                Travaler.ConfirmPassword = travaler.ConfirmPassword;
                Travaler.Email = travaler.Email;
                Travaler.Photo = travaler.Photo;


                Membership.CreateUser(Travaler.UserName, Travaler.Password, Travaler.Email);

                Roles.AddUserToRole(Travaler.UserName, "Traveler");
                db.Travaler.Add(Travaler);
                db.SaveChanges();


            }
        }
        public void Reset(TravalerVM A)
        {
            var travaler = db.Travaler.Find(A.TravalerId);
            var data = Membership.GetUser(travaler.UserName);


            //var x = data.ChangePassword(Agency.Password,adm.Password);
            data.ChangePassword(data.ResetPassword(), A.Password);

            travaler.UserName = A.UserName;
            travaler.Email = A.Email;
            travaler.Password = A.Password;
            travaler.ConfirmPassword = A.ConfirmPassword;
            travaler.Email = A.Email;
            travaler.Photo = A.Photo;



            db.SaveChanges();
        }
       public void Delete(int? Id)
        {
            var data = db.Travaler.Find(Id);
            var data1 = db.RatePosts.Where(a => a.TravalerId == Id);
            foreach (var item in data1)
            {
                db.RatePosts.Remove(item);
            }


            var data2 = db.Saved.Where(a => a.TravelerId == Id);
            foreach (var item1 in data2)
            {
                db.Saved.Remove(item1);
            }

            Roles.RemoveUserFromRole(data.UserName, "Traveler");
            Membership.DeleteUser(data.UserName, true);
            db.Travaler.Remove(data);
            db.SaveChanges();
        }
        public TravalerVM GetTravalerById(int Id)
        {
            var travaler = db.Travaler.Where(a=>a.TravalerId==Id).Select(a => new TravalerVM
            {
                TravalerId = a.TravalerId,
                Password = a.Password,
                ConfirmPassword = a.ConfirmPassword,
                Email = a.Email,
                Photo = a.Photo,
                UserName = a.UserName


            }).FirstOrDefault();
            return (travaler);
        }
        public void Edit(TravalerVM A, HttpPostedFileBase Photo)
        {




            if (Photo != null)
            {

                File.Delete(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Img/"), A.Photo));

                string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");

                string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
                string fullpath = Path.Combine(Folderpath, photoname);
                Photo.SaveAs(fullpath);
                A.Photo = photoname;
            }
            var Travaler = db.Travaler.Find(A.TravalerId);
            Travaler.UserName = A.UserName;
            Travaler.Email = A.Email;
            Travaler.Password = A.Password;
            Travaler.ConfirmPassword = A.ConfirmPassword;
            Travaler.Email = A.Email;
            Travaler.Photo = A.Photo;
            var user = Membership.GetUser(Travaler.UserName);
            user.Email = A.Email;
            user.ChangePassword(user.ResetPassword(), A.Password);


            Membership.UpdateUser(user);
            db.SaveChanges();
        }
    }
}