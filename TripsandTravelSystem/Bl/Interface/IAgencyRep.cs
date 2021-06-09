using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TripsandTravelSystem.Models;

namespace TripsandTravelSystem.Bl.Interface
{
    interface IAgencyRep
    {
        void AddAgency(AgencyVM agenc,HttpPostedFileBase Photo);
        AgencyVM GetAgencyById(int Id);
        void Reset(AgencyVM A);
        void Delete(int? Id);
        void Edit(AgencyVM A, HttpPostedFileBase Photo);
        IQueryable<AgencyVM> GetAllAency();
        
    }
}
