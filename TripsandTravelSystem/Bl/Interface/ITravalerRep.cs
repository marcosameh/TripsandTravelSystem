using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Bl.Interface
{
    interface ITravalerRep
    {
       IQueryable< TravalerVM> Display();
        void AddTravaler(TravalerVM t, HttpPostedFileBase Photo);
        void Reset(TravalerVM A);
        void Delete(int? Id);
        TravalerVM GetTravalerById(int Id);
        void Edit(TravalerVM A, HttpPostedFileBase Photo);

    }
}
