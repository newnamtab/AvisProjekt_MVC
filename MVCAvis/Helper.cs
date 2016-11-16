using MVCAvis.WcfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAvis
{
    public class Helper
    {
        public Reservation calcReservationPrize(Reservation res)
        {
            //Reservation tempres = new Reservation();
            //tempres = res;
            
            int days = (res.EndDate - res.StartDate).Days;

            switch (res.BilCat)
            {
                case "A":
                    res.TotalPrize = 100 * days;
                    break;
                case "B":
                    res.TotalPrize = 200 * days;
                    break;
                case "C":
                    res.TotalPrize = 300 * days;
                    break;
                case "I":
                    res.TotalPrize = 500 * days;
                    break;
                case "O":
                    res.TotalPrize = 250 * days;
                    break;
                default:
                    res.TotalPrize = 1 * days;
                    break;
            }
            return res;
        }
        //public Reservation opretRes(string cat, string des, DateTime start, DateTime slut, string fName, string lName, string address, int phone, string email)
        //{
        //    Reservation res = new Reservation();
        //    res.BilCat = cat;
        //    res.StartStation = des;
        //    res.StartDate = start;
        //    res.EndDate = slut;
        //    res.FirstName = fName;
        //    res.LastName = lName;
        //    res.Address = address;
        //    res.TelephoneNumber = phone;
        //    res.Email = email;
        //    return res;
        //}
        public bool Search(string s1, string s2)
        {
            if (CompareStrings(s1, s2) || CompareStrings(s2, s1))
            {
                return true;
            }
            return false;
        }

        private bool CompareStrings(string s1, string s2)
        {
            string s1up = s1.ToUpper();
            string s2up = s2.ToUpper();
            int count = 0;

            for (int i = 0; i < s1up.Length; i++)
            {
                if (s2up.Contains(s1up[i]))
                {
                    count++;
                    //char ch = s2up[s2up.IndexOf(s1up[i])+3];
                    //char kl = s1up[i + 3];
                    try
                    {
                        if (s2up[s2up.IndexOf(s1up[i])] == s1up[i] && s2up[s2up.IndexOf(s1up[i]) + 1] == s1up[i + 1] &&
                            s2up[s2up.IndexOf(s1up[i]) + 2] == s1up[i + 2] && s2up[s2up.IndexOf(s1up[i]) + 3] == s1up[i + 3])
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            //if (s2.Length - s1.Length == s2.Length - count - 1 && s1.Length - s2.Length != s1.Length - count + 1)
            //{
            //    return true;
            //}
            return false;
        }
    }
}
