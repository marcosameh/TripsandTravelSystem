using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Bl.Interface
{
    interface IPostRep
    {
        void AddNewPost(TripPostVM p, HttpPostedFileBase Photo);

        void Approve(int Id);
        IQueryable<TripPostVM> WaitingPosts();
        void ApproveAll();
        void Remove(int id);
        TripPostVM GetPostById(int Id, int? TravalerId);
       IQueryable <TripPostVM> GetPostsByAgencyId(int AgencyId);

        void EditPost(TripPostVM p, HttpPostedFileBase Photo);

        IQueryable<TripPostVM> WallPage(int?Id);
        IQueryable<TripPostVM> SearchPrice(int Price, int? Id);
        IQueryable<TripPostVM> SearchAgencyName(String AgencyName, int? Id);
        IQueryable<TripPostVM> SearchTripDate(String TripDate, int? Id);

        IQueryable<TripPostVM> FavouritePosts(int TravalerId);


    }
}
