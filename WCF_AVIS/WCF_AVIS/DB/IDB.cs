using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_AVIS
{
    interface IDB
    {
        void Insert(Reservation res);
        List<Reservation> Search(Reservation res);
        List<Reservation> GetReservations();
        RentalStation MatchStation(string stationcode);
        bool Login(string user, string password);
        int loginAccess(string user);
        string TestMetode();
    }
    
}
