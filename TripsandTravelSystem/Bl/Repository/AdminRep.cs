using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripsandTravelSystem.BAl.Interface;
using TripsandTravelSystem.Models;
using TripsandTravelSystem.DAL;
using System.Web.Security;
using System.IO;
using System.Data.Entity;

namespace TripsandTravelSystem.Bl.Repository
{
    
    public class AdminRep : IAdminRep
    {
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();
      public void AddAdmin(AdminVM adm, HttpPostedFileBase Photo)
            
        {
            
            if (Photo != null) { 
            string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");
            string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
            string fullpath = Path.Combine(Folderpath, photoname);
            Photo.SaveAs(fullpath);
            adm.Photo = photoname;
                }
            var admin = new Admin();
            admin.UserName = adm.UserName;
            admin.Email = adm.Email;
            admin.Password = adm.Password;
            admin.ConfirmPassword = adm.ConfirmPassword;
            admin.Email = adm.Email;
            admin.Photo = adm.Photo;
            admin.PhoneNumber = adm.PhoneNumber;
            db.Admin.Add(admin);
            Membership.CreateUser(admin.UserName, admin.Password, admin.Email);

            Roles.AddUserToRole(admin.UserName, "Admin");
            db.Admin.Add(admin);
            db.SaveChanges();
            
            
        }

        public AdminVM GetAdminById(int Id)
        {
            var data = db.Admin.Where(a => a.Id == Id).Select(a => new AdminVM
            {
                Id=a.Id,
                UserName=a.UserName,
                Password=a.Password,
                ConfirmPassword=a.ConfirmPassword,
                Email=a.Email,
                PhoneNumber=a.PhoneNumber,
                Photo=a.Photo
            }).FirstOrDefault();
            return data;

        }

        public void Reset(AdminVM adm)
        {
            var admin = db.Admin.Find(adm.Id);
            var data = Membership.GetUser(admin.UserName);


            //var x = data.ChangePassword(admin.Password,adm.Password);
            data.ChangePassword(data.ResetPassword(), adm.Password);

            admin.UserName = adm.UserName;
            admin.Email = adm.Email;
            admin.Password = adm.Password;
            admin.ConfirmPassword = adm.ConfirmPassword;
            admin.Email = adm.Email;
            admin.Photo = adm.Photo;
            admin.PhoneNumber = adm.PhoneNumber;
         
           
            db.SaveChanges();
            
        }
     
    }
}