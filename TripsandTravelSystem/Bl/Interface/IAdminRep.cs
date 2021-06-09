using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripsandTravelSystem.Models;
using System.IO;
using System.Web;
using TripsandTravelSystem.DAL;

namespace TripsandTravelSystem.BAl.Interface
{
    interface IAdminRep
    {
         void AddAdmin(AdminVM adm,HttpPostedFileBase Photo);
          AdminVM GetAdminById(int Id);
        void Reset(AdminVM A);
       
    }
}
