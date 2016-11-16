using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WCF_AVIS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AVISservice : IAVISservice
    {
        private IDB DB;

        public AVISservice()
        {
            SetDB();
        }

        public void SetDB()
        {
            if (HttpContext.Current.Application["DB"] != null)
                DB = (IDB)HttpContext.Current.Application["DB"];
            else
                DB = new DB.FakeDB();

            HttpContext.Current.Application["DB"] = DB;
        }
        public void SaveResevation(Reservation res)
        {
            //Gem i (falsk)database
            DB.Insert(res);
        }

        public Reservation SearchReservation(string searchVariable)
        {
            //Søg i (falsk)database og returner det man finder
            return new Reservation("ford focus", "Odense", DateTime.Today, DateTime.Today);
        }

        public List<Reservation> Search(Reservation searchVariable)
        {

            //Søg i (falsk)database og returner det man finder
            return DB.Search(searchVariable); 
        }


        public void Update(Reservation res)
        {
            //opdater reservation i (falsk)DB
        }

        public void Delete(Reservation res)
        {
            //sæt "flag" i database så ordren står som afvist.
        }

        public double CalcPrice(string cat, string dest, DateTime dstart, DateTime dslut)
        {
            double bilCatMultiplier;
            switch (cat)
            {
                case "Kategori A":
                    bilCatMultiplier = 100;
                    break;
                case "Kategori B":
                    bilCatMultiplier = 200;
                    break;
                case "Kategori C":
                    bilCatMultiplier = 300;
                    break;
                case "Kategori I":
                    bilCatMultiplier = 500;
                    break;
                case "Kategori O":
                    bilCatMultiplier = 250;
                    break;
                default:
                    bilCatMultiplier = 0;
                    break;
            }
            if (bilCatMultiplier==0) throw new Exception("Der skete en fejl i udregningen. Prøv igen");
           
            return bilCatMultiplier;
        }

        public List<Reservation> GetReservations()
        {
            return DB.GetReservations();
        }
        public RentalStation MatchStation(string stationcode)
        {
            return DB.MatchStation(stationcode);
        }
        public string CreateSalt()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå";
            char[] reSalt = new char[5];
            Random rand = new Random();
            for (int i = 0; i < reSalt.Length; i++)
            {
                reSalt[i] = chars[rand.Next(chars.Length)];
            }
            return new string(reSalt);
        }
        public string Hasher(string pass, string salt)
        {
            StringBuilder sb = new StringBuilder();
            HashAlgorithm hasher = MD5.Create();
            foreach (byte b in hasher.ComputeHash(Encoding.UTF8.GetBytes(pass + salt)))
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        public bool validateUser(string n, string p)
        {
            return DB.Login(n, p);
        }

        public int validateAcess(string user)
        {
            return DB.loginAccess(user);
        }
    }
}
