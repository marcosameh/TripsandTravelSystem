using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TripsandTravelSystem.Bl.Interface;
using TripsandTravelSystem.Models;
using TripsandTravelSystem.DAL;
using System.Web.Security;

namespace TripsandTravelSystem.Bl.Repository
{
    public class AgencyRep : IAgencyRep
    {
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();
        public void AddAgency(AgencyVM agenc, HttpPostedFileBase Photo)
        {
            
            {
                if (Photo != null)
                {
                    string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");
                    string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
                    string fullpath = Path.Combine(Folderpath, photoname);
                    Photo.SaveAs(fullpath);
                    agenc.Photo = photoname;
                }
                var Agency = new Agency();
                Agency.UserName = agenc.UserName;
                Agency.Email = agenc.Email;
                Agency.Password = agenc.Password;
                Agency.ConfirmPassword = agenc.ConfirmPassword;
                Agency.Email = agenc.Email;
                Agency.Photo = agenc.Photo;
                
            
                Membership.CreateUser(Agency.UserName, Agency.Password, Agency.Email);

                Roles.AddUserToRole(Agency.UserName, "Agency");
                db.Agency.Add(Agency);
                db.SaveChanges();


            }
        }

       

       public AgencyVM GetAgencyById(int Id)
        {
            var data = db.Agency.Where(a => a.AgencyId == Id).Select(a => new AgencyVM
            {
                AgencyId = a.AgencyId,
                UserName = a.UserName,
                Password = a.Password,
                ConfirmPassword = a.ConfirmPassword,
                Email = a.Email,
                Photo = a.Photo
            }).FirstOrDefault();
            return data;
        }

       public void Reset(AgencyVM A)
        {
            var Agency = db.Agency.Find(A.AgencyId);
            var data = Membership.GetUser(Agency.UserName);


            //var x = data.ChangePassword(Agency.Password,adm.Password);
            data.ChangePassword(data.ResetPassword(), A.Password);

            Agency.UserName = A.UserName;
            Agency.Email = A.Email;
            Agency.Password = A.Password;
            Agency.ConfirmPassword = A.ConfirmPassword;
            Agency.Email = A.Email;
            Agency.Photo = A.Photo;
            


            db.SaveChanges();
        }
        public void Edit(AgencyVM A, HttpPostedFileBase Photo)
        {
            

           

            if (Photo != null)
            {

                File.Delete(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Img/"),A.Photo));

                string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");
               
                string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
                string fullpath = Path.Combine(Folderpath, photoname);
                Photo.SaveAs(fullpath);
                A.Photo = photoname;
            }
            var Agency = db.Agency.Find(A.AgencyId);
            Agency.UserName = A.UserName;
            Agency.Email = A.Email;
            Agency.Password = A.Password;
            Agency.ConfirmPassword = A.ConfirmPassword;
            Agency.Email = A.Email;
            Agency.Photo = A.Photo;
            var user = Membership.GetUser(Agency.UserName);
            user.Email = A.Email;
            user.ChangePassword(user.ResetPassword(), A.Password);


            Membership.UpdateUser(user);
            db.SaveChanges();
        }
        public IQueryable<AgencyVM> GetAllAency()
        {
            var agency = db.Agency.Select(a =>  new AgencyVM
            {
                AgencyId=a.AgencyId,
                Password=a.Password,
                ConfirmPassword=a.ConfirmPassword,
                Email=a.Email,
                Photo=a.Photo,
                UserName=a.UserName
                
            });
            return (agency);
        }

        
       public void Delete(int? Id)
        {
            var data = db.TripPost.Where(a => a.AgencyId == Id);

            foreach (var item in data)
            {
                var rate = db.RatePosts.Where(a => a.PostId==item.TripId);
                foreach (var item1 in rate)
                {
                    db.RatePosts.Remove(item1);
                }
                var Saved = db.Saved.Where(a => a.PostId == item.TripId);
                foreach (var item2 in Saved)
                {
                    db.Saved.Remove(item2);
                }
                db.TripPost.Remove(item);

            }
            var data1 = db.Agency.Find(Id);
            
            Roles.RemoveUserFromRole(data1.UserName, "Agency");
            Membership.DeleteUser(data1.UserName, true);
            db.Agency.Remove(data1);
            db.SaveChanges();

        }
        
    }
}