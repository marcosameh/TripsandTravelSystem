using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripsandTravelSystem.DAL;

namespace TripsandTravelSystem.Bl.Repository
{
    public class ReactRep
    {
        TripsandTravelesSystemEntities db = new TripsandTravelesSystemEntities();

        public void Like(int PostId, int TravalerId)
        {
            var data = db.RatePosts.Where(a => a.TravalerId == TravalerId).Where(a => a.PostId == PostId).FirstOrDefault();
            if (data != null)
            {
                db.RatePosts.Remove(data);
                db.SaveChanges();

            }
            var RatePost = new RatePosts();
            RatePost.PostId = PostId;
            RatePost.DisLike = false;
            RatePost.Like = true;
            RatePost.TravalerId = TravalerId;
            db.RatePosts.Add(RatePost);
            db.SaveChanges();

        }
        public void DisLike(int PostId, int TravalerId)
        {
            var data = db.RatePosts.Where(a => a.TravalerId == TravalerId).Where(a => a.PostId == PostId).FirstOrDefault();
            if (data != null)
            {
                db.RatePosts.Remove(data);
                db.SaveChanges();

            }
            var RatePost = new RatePosts();
            RatePost.PostId = PostId;
            RatePost.DisLike = true;
            RatePost.Like = false;
            RatePost.TravalerId = TravalerId;
            db.RatePosts.Add(RatePost);
            db.SaveChanges();

        }
        public void AddToFavourite(int PostId, int TravalerId)
        {
            var SavedPost = new Saved();
            SavedPost.PostId = PostId;
            SavedPost.TravelerId = TravalerId;
            db.Saved.Add(SavedPost);
            db.SaveChanges();
         }
        public void DeleteFromFavourite(int PostId, int TravalerId)
        {
            var data = db.Saved.Where(a => a.PostId == PostId).Where(a => a.TravelerId == TravalerId).FirstOrDefault();
            if (data != null)
            {
                db.Saved.Remove(data);

                db.SaveChanges();
            }
          

        }
    }

}