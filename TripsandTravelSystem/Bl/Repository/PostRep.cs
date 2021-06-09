using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripsandTravelSystem.Bl.Interface;
using TripsandTravelSystem.Models;
using TripsandTravelSystem. DAL;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace TripsandTravelSystem.Bl.Repository
{
    public class PostRep : IPostRep
    {
      
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();
       
       public void AddNewPost(TripPostVM p, HttpPostedFileBase Photo)
        {
            try
            {
                if (Photo != null)
                {
                    string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");
                    string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
                    string fullpath = Path.Combine(Folderpath, photoname);
                    Photo.SaveAs(fullpath);
                    p.TripImage = photoname;
                }
                var TripPost = new TripPost();
                TripPost.TripTitle = p.TripTitle;
                TripPost.Status = "Waiting";
                TripPost.TripImage = p.TripImage;
                TripPost.AgencyId = p.AgencyId;
                TripPost.TripDestination = p.TripDestination;
                TripPost.TripDetails = p.TripDetails;
                TripPost.PostDate = DateTime.Now.ToString("dd/MM/yyyy");
                TripPost.TripDate = p.TripDate.ToString();
                TripPost.Price = p.Price;
                db.TripPost.Add(TripPost);
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        public void Approve(int id)
        {
            var data = db.TripPost.Find(id);
            data.Status = "Approved";
            db.SaveChanges();

        }
        public IQueryable<TripPostVM> WaitingPosts()
        {
            var data = db.TripPost.Where(a => a.Status == "Waiting").Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,
                AgencyId = a.AgencyId,
                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.PostDate,
                Price = a.Price

        });
            return data;
        }
        public void ApproveAll()
        {
            var data = db.TripPost.Where(a => a.Status == "Waiting");
            foreach (var item in data)
            {
                item.Status = "Approved";
            }
            db.SaveChanges();

        }
        public void Remove(int id)
        {
            var data = db.RatePosts.Where(a => a.PostId == id);
            foreach (var item in data)
            {
                db.RatePosts.Remove(item);
            }
            var data1 = db.Saved.Where(a => a.PostId == id);
            foreach (var item in data1)
            {
                db.Saved.Remove(item);
            }

            var data2 = db.TripPost.Find(id);
            db.TripPost.Remove(data2);
            db.SaveChanges();
        }
        public TripPostVM GetPostById(int Id,int? TravalerId)
        {
            var data = db.TripPost.Where(a => a.TripId == Id).Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,
                AgencyId = a.AgencyId,
                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.PostDate,
                Status = a.Status,
                Price = a.Price,
                NumOfLikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.Like == true).Count(),
                NumOfDislikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.DisLike == true).Count(),
                Favourite = a.Saved.Where(b => b.TravelerId == TravalerId).Where(b => b.PostId == a.TripId).Count()
            }).FirstOrDefault();
            return data;
        }
       public IQueryable<TripPostVM> GetPostsByAgencyId(int AgencyId)
        {
            var data = db.TripPost.Where(a => a.AgencyId == AgencyId).Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,
                AgencyId = a.AgencyId,
                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.PostDate,
                Status = a.Status,
                Price = a.Price
               

            }) ;
            return data;

        }
        public void EditPost(TripPostVM p,HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {
                string Folderpath = System.Web.HttpContext.Current.Server.MapPath("~/Img/");
                string photoname = Guid.NewGuid() + Path.GetFileName(Photo.FileName);
                string fullpath = Path.Combine(Folderpath, photoname);
                Photo.SaveAs(fullpath);
                p.TripImage = photoname;
            }
            var data = db.TripPost.Find(p.TripId);
            data.TripTitle = p.TripTitle;
            data.TripDetails = p.TripDetails;
            data.TripDate = p.TripDate.ToString();
            data.TripDestination = p.TripDestination;
            data.TripImage = p.TripImage;
            data.Status = "Waiting";
            data.Price = p.Price;
            
              db.SaveChanges();
        }
        public IQueryable<TripPostVM> WallPage(int?Id)
        {
           

            var data = db.TripPost.Where(a => a.Status == "Approved").Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,

                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.TripDate,
                Price = a.Price,
                NumOfLikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.Like == true).Count(),
                NumOfDislikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.DisLike == true).Count(),
                
                Like = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.Like).FirstOrDefault(),
                DisLike = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.DisLike).FirstOrDefault()


            }) ;
            return data;

        }
        public IQueryable<TripPostVM> SearchPrice(int Price,int?Id)
        {
            var data = db.TripPost.Where(a => a.Status == "Approved").Where(a=>a.Price==Price).Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,

                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.TripDate,

                Price = a.Price,
                NumOfLikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.Like == true).Count(),
                NumOfDislikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.DisLike == true).Count(),
               
                  Like = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.Like).FirstOrDefault(),
                DisLike = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.DisLike).FirstOrDefault()


            });
            return data;

        }
        
        public IQueryable<TripPostVM> SearchAgencyName(String AgencyName, int? Id)

        {
            var data = db.TripPost.Where(a => a.Status == "Approved").Where(a => a.Agency.UserName.Contains(AgencyName)).Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,

                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.TripDate,

                Price = a.Price,
                NumOfLikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.Like == true).Count(),
                NumOfDislikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.DisLike == true).Count(),
                Stars = a.RatePosts.Where(b => b.PostId == a.TripId).Sum(b => b.Stars) / a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.Stars != null).Count(),

                  Like = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.Like).FirstOrDefault(),
                DisLike = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.DisLike).FirstOrDefault(),
                
            });
            return data;
        }
        public IQueryable<TripPostVM> SearchTripDate(String TripDate, int? Id)
        {
            var data = db.TripPost.Where(a => a.Status == "Approved").Where(a => a.TripDate.Contains(TripDate)).Select(a => new TripPostVM
            {
                TripId = a.TripId,
                AgencyName = a.Agency.UserName,

                TripTitle = a.TripTitle,
                TripDate = a.TripDate,
                TripDestination = a.TripDestination,
                TripDetails = a.TripDetails,
                TripImage = a.TripImage,
                PostDate = a.TripDate,

                Price = a.Price,
                NumOfLikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.Like == true).Count(),
                NumOfDislikes = a.RatePosts.Where(b => b.PostId == a.TripId).Where(b => b.DisLike == true).Count(),
               

                  Like = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.Like).FirstOrDefault(),
                DisLike = a.RatePosts.Where(b => b.TravalerId == Id).Where(b => b.PostId == a.TripId).Select(b => b.DisLike).FirstOrDefault()

            });
            return data;

        }
        public IQueryable<TripPostVM> FavouritePosts(int TravalerId)
        {
            var data = db.Saved.Where(a=>a.TravelerId==TravalerId).Select(a => new TripPostVM
            {
                TripId = a.TripPost.TripId,
                TripDate=a.TripPost.TripDate,
                AgencyName=a.TripPost.Agency.UserName,
                TripDestination=a.TripPost.TripDestination,
                TripImage=a.TripPost.TripImage,
                TripDetails=a.TripPost.TripDetails,
                Price=a.TripPost.Price,
                NumOfLikes=a.TripPost.RatePosts.Where(b => b.PostId == a.PostId).Where(b => b.Like == true).Count(),
                NumOfDislikes = a.TripPost.RatePosts.Where(b => b.PostId == a.PostId).Where(b => b.DisLike == true).Count(),


                Like = a.TripPost.RatePosts.Where(b => b.TravalerId == a.TravelerId).Where(b => b.PostId == a.PostId).Select(b => b.Like).FirstOrDefault(),
                DisLike = a.TripPost.RatePosts.Where(b => b.TravalerId == a.TravelerId).Where(b => b.PostId == a.PostId).Select(b => b.DisLike).FirstOrDefault()


            }) ;
            return data;
        }

        
    }
}