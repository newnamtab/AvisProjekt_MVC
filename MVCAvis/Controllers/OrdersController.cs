using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAvis.Models;
using MVCAvis.WcfService;

namespace MVCAvis.Controllers
{
    public class OrdersController : Controller
    {
        private AVISserviceClient Refe = new AVISserviceClient();
        private Helper hilfer = new Helper();

        [HttpGet]
        public ActionResult createOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createOrder(string confirm, FormCollection form)
        {
            Reservation tempRes = new Reservation();

            switch (confirm)
            {
                case "Calculate prize":
                    tempRes.BilCat = form["cat"];
                    tempRes.StartStation= Refe.MatchStation(form["destination"]);
                    tempRes.EndStation = Refe.MatchStation(form["Slutdestination"]);
                    tempRes.Insurance = int.Parse(form["Insurance"]);
                    tempRes.StartDate = DateTime.Parse(form["datostart"]);
                    tempRes.EndDate = DateTime.Parse(form["datoslut"]);

                    tempRes = hilfer.calcReservationPrize(tempRes);

                    ViewData["pris"] = tempRes.TotalPrize;
                    ViewData["desti"] = tempRes.StartStation.StationCode;
                    ViewData["Slutdestination"] = tempRes.EndStation.StationCode;
                    ViewData["datostart"] = tempRes.StartDate;
                    ViewData["datoslut"] = tempRes.EndDate;
                    ViewData["dage"] = (tempRes.EndDate - tempRes.StartDate).Days;
                    ViewData["forsikring"] = tempRes.Insurance;
                    break;
                case "Create order":
                    
                    
                    tempRes.BilCat = form["cat"];
                    tempRes.StartStation =Refe.MatchStation( form["destination"]);
                    tempRes.EndStation =Refe.MatchStation( form["Slutdestination"]);
                    tempRes.StartDate = DateTime.Parse(form["datostart"]);
                    tempRes.EndDate = DateTime.Parse(form["datoslut"]);
                    tempRes.Insurance = int.Parse(form["Insurance"]);
                    tempRes.Customer = new Customer();
                    tempRes.Customer.FirstName = form["firstname"];
                    tempRes.Customer.LastName = form["lastname"];
                    tempRes.Customer.Street = form["address"];
                    tempRes.Customer.PostalCode = form["postal"];
                    tempRes.Customer.City = form["city"];
                    tempRes.Customer.TelephoneNumber = form["phonenumber"];
                    tempRes.Customer.Email = form["email"];

                    Refe.SaveResevation(tempRes);
                    break;
                default:
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotImplemented, "Something went wrong. Try again" + confirm);
            }
            return View();
        }

        [HttpGet]
        public ActionResult searchOrder()
        {
            List<Reservation> resList = new List<Reservation>();
            resList = Refe.GetReservations().ToList();

            return View(resList);
        }
        [HttpPost]
        public ActionResult searchOrder(string search, FormCollection form)
        {
            List<Reservation> resList = new List<Reservation>();
            Reservation tempRes = new Reservation();
            DateTime tempDate;
            
            switch (search)
            {
                case "Search":
                    tempRes.Reservationsnummer = form["searchreservationsnummer"];
                    tempRes.StartStation = Refe.MatchStation( form["searchstation"]);
                    
                    if (DateTime.TryParse(form["searchdatostart"], out tempDate))
                    {
                        tempRes.StartDate = tempDate;
                    }
                    else tempRes.StartDate = DateTime.Now - new TimeSpan(30,0,0,0);

                    
                    if (DateTime.TryParse(form["searchdatoslut"], out tempDate))
                    {
                        tempRes.EndDate = tempDate;
                    }
                    else tempRes.EndDate = DateTime.Now + new TimeSpan(30, 0, 0, 0);

                    tempRes.Customer = new Customer();
                    tempRes.Customer.FirstName = form["searchfirstname"];
                    tempRes.Customer.LastName = form["searchlastname"];
                    tempRes.Customer.Street = form["searchaddress"];
                    tempRes.BilCat = "A";

                    int temp;
                    if (int.TryParse(form["searchphonenumber"], out temp))
                    {
                        tempRes.Customer.TelephoneNumber = temp.ToString();
                    }
                    else tempRes.Customer.TelephoneNumber = "0";
                    
                    tempRes.Customer.Email = form["searchemail"];

                    resList = Refe.Search(tempRes).ToList();
                    break;

                //ViewData["reservations"] = resList;
                //ViewData["startdato"] = 
                //ViewData["firstname"] = 
                //ViewData["lastname"] = 
                //ViewData["phonenumber"] = 
                //ViewData["email"] = 
                //ViewData["rentalstation"] = 



                case "Reset":
                    Reservation ts = new Reservation();
                    ts.BilCat = form["cat"];
                    ts.StartStation =Refe.MatchStation( form["destination"]);
                    ts.EndStation = Refe.MatchStation(form["destination"]);
                    ts.Customer = new Customer();
                    ts.Customer.FirstName = form["firstname"];
                    ts.Customer.LastName = form["lastname"];
                    ts.Customer.Street = form["address"];
                    ts.Customer.TelephoneNumber = form["phonenumber"];
                    ts.Customer.Email = form["email"];
                    ts.StartDate = DateTime.Parse(form["datostart"]);
                    ts.EndDate = DateTime.Parse(form["datoslut"]);
                    
                    Refe.SaveResevation(ts);
                    break;
                default:
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotImplemented, "Something went wrong. Try again" + search);
            }
            return View(resList);
        }

        //[HttpGet]
        //public ActionResult editOrder()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult editOrder(string reservationnumber)
        {
            Reservation res = Refe.SearchReservation(reservationnumber);
            return View(res);
        }

        [HttpPost]
        public ActionResult editOrder(string modify,FormCollection form)
        {

            return View();
        }
    }
}