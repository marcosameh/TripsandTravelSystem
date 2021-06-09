using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripsandTravelSystem.Bl.Interface
{
    interface IReactRep
    {
        void Like(int PostId, int TravalerId);
        void DisLike(int PostId, int TravalerId);
        void AddToFavourite(int PostId, int TravalerId);
        void DeleteFromFavourite(int PostId, int TravalerId);



    }
}
