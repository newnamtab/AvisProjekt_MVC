using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace WCF_AVIS
{
    [DataContract]
    [Serializable]
    public class Reservation
    {
        private string _ReservationNumber { get; set; }
        private Customer _Customer { get; set; }
        private CarCategory _BookedCategory { get; set; }
        private DateTime _StartDate { get; set; }
        private DateTime _ReturnDate { get; set; }
        private RentalStation _RentalStation { get; set; }
        private RentalStation _ReturnStation { get; set; }
        private double _DailyPrice { get; set; }
        private double _TotalPrice { get; set; }

        [DataMember]
        public string Reservationsnummer { get { return this._ReservationNumber; } set { this._ReservationNumber = value; } }
        [DataMember]
        public Customer Customer { get {return this._Customer; } set {this._Customer = value; } }
        [DataMember]
        public DateTime StartDate { get { return this._StartDate; } set { this._StartDate = value; } }
        [DataMember]
        public DateTime EndDate { get { return this._ReturnDate; } set { this._ReturnDate = value; } }
        [DataMember]
        public string BilCat { get { return this._BookedCategory.ID.ToString(); } set { this._BookedCategory = new CarCategory(); this._BookedCategory.ID = char.Parse(value); } }
        [DataMember]
        public RentalStation StartStation { get { return this._RentalStation; } set { this._RentalStation = value; } }
        [DataMember]
        public RentalStation EndStation { get { return this._ReturnStation; } set { this._ReturnStation = value; } }
        //[DataMember]
        //public string FirstName { get { return this._Customer.FirstName; } set { this._Customer.LastName = value; } }
        //[DataMember]
        //public string LastName { get { return this._Customer.LastName; } set { this._Customer.LastName = value; } }
        //[DataMember]
        //public string Address { get { return this._Customer.Street; } set { this._Customer.Street = value; } }
        //[DataMember]
        //public string Postalcode { get { return this._Customer.PostalCode; } set { this._Customer.PostalCode = value; } }
        //[DataMember]
        //public string City { get { return this._Customer.City; } set { this._Customer.City = value; } }
        //[DataMember]
        //public int TelephoneNumber { get { return int.Parse(this._Customer.TelephoneNumber); } set { this._Customer.TelephoneNumber = value.ToString(); } }
        //[DataMember]
        //public string Email { get { return this._Customer.Email; } set { this._Customer.Email = value; } }
        [DataMember]
        public double TotalPrize { get { return this._TotalPrice; } set { this._TotalPrice = value; } }
        [DataMember]
        public int Insurance { get; set; }




        public Reservation(DateTime startDate, DateTime endDate, string bilCat, string startStation, string endStation, string firstName, string lastName, string address, int telephoneNumber, string email)
        {
            this._Customer = new Customer();
            this._BookedCategory = new CarCategory();
            this._RentalStation = new RentalStation();
            this._ReturnStation = new RentalStation();
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BilCat = bilCat;
            this.StartStation = new DB.FakeDB().MatchStation(startStation) ;
            this.EndStation = new DB.FakeDB().MatchStation(endStation);
            this.Customer.FirstName = firstName;
            this.Customer.LastName = lastName;
            this.Customer.Street = address;
            this.Customer.TelephoneNumber = telephoneNumber.ToString();
            this.Customer.Email = email;

        }
        //PRIVATE CONSTRUCTOR FOR INTERNAL USE
        private Reservation(string reservationsnummer, DateTime startDate, DateTime endDate, string bilCat, RentalStation startStation, RentalStation endStation, string firstName, string lastName, string address, int telephoneNumber, string email, double totalPrize)
        {
            // NOT IMPLEMENTED OR USED YET
            this.Reservationsnummer = reservationsnummer;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BilCat = bilCat;
            this.StartStation = startStation;
            this.EndStation = endStation;
            this.Customer.FirstName = firstName;
            this.Customer.LastName = lastName;
            this.Customer.Street = address;
            this.Customer.TelephoneNumber = telephoneNumber.ToString();
            this.Customer.Email = email;
            this.TotalPrize = totalPrize;
        }

        public Reservation(string bilcat, string startstation, DateTime start, DateTime end)
        {
            this.Customer = new Customer();
            this.BilCat = new CarCategory().ID.ToString();
            this.StartStation = new RentalStation();
            this.EndStation = new RentalStation();
            this.StartDate = start;
            this.EndDate = end;
            this.BilCat = bilcat;
            this.StartStation = new DB.FakeDB().MatchStation(startstation);
            this.TotalPrize = 0;
            this.Reservationsnummer = "UNASSIGNED";
        }
        public Reservation()
        {

            this.Customer = new Customer();
            this._BookedCategory = new CarCategory();
            this.StartStation = new RentalStation();
            this.EndStation = new RentalStation();
            this._DailyPrice = 0;
            this.Reservationsnummer = "UNASSIGNED";
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            this.BilCat = "A";
            this.Customer.FirstName = "NONE";
            this.Customer.LastName = "UNKNOWN";
            this.Customer.Street = "NONE";
            this.Customer.PostalCode = "NONE";
            this.Customer.City = "NOWHERE";
            this.Customer.TelephoneNumber = "0";
            this.Customer.Email = "NONE@NONE.COM";
            this.TotalPrize = 0;
            this.Insurance = 0;

        }
    }
}
