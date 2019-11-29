using MainProject;
using MainProject.Business_Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace part2
{
    public class WorkWithDB
    {
        public LoggedInAirlineFacade alf = new LoggedInAirlineFacade();
        public LoggedInAdministratorFacade afc = new LoggedInAdministratorFacade();
        public LoggedInCustomerFacade cfc = new LoggedInCustomerFacade();
        public LoginToken<Administrator> adninToken = new LoginToken<Administrator>();
        public LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany>();
        public LoginToken<Customer> customerToken = new LoginToken<Customer>();
        public List<Country> countries;
        public List<Customer> customers;
        public List<AirlineCompany> airlineCompanies;
        public List<Tickets> ticketsPerCustomer;
        public List<Flight> flightsPerCompany;
        public int numberOfFlightsToCreate = 0;
        public WorkWithAPI restWorking;
        public event PropertyChangedEventHandler PropertyChanged;
        private BackgroundWorker _bgWorker;
        private int _progressStatus;
        public int ProgressStatus
        {
            get
            {
                return _progressStatus;
            }
            set
            {
                this._progressStatus = value;
                OnPropertyChanged("ProgressStatus");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public WorkWithDB()
        {

            _bgWorker = new BackgroundWorker();
        }
        public async Task InsertingDataBase(string AirlineNo, string CustomerNo, string FlightsPerCmpnyNo, string TicketsPerCustNo, string CountriesNo)
        {
            
            int numberOfAirLines = Int32.Parse(AirlineNo);
            int numberOfCustomers = Int32.Parse(CustomerNo);
            int numberOfFlightsPerCmp = Int32.Parse(FlightsPerCmpnyNo);
            int numberOfTicketsPerCust = Int32.Parse(TicketsPerCustNo);
            int numberOfCountries = Int32.Parse(CountriesNo);
            try
            {
                await Task.Run(() => 
                {
                    restWorking = new WorkWithAPI();
                    List<Country> countriesFromRest = restWorking.CountriesApiWebWork();
                    int countryNoParsed = Convert.ToInt32(CountriesNo);
                    int customerNoParsed = Convert.ToInt32(CustomerNo);
                    int flightNoParsed = Convert.ToInt32(FlightsPerCmpnyNo);
                    int airlineNoParsed = Convert.ToInt32(AirlineNo);
                    int ticketNoParsed = Convert.ToInt32(TicketsPerCustNo);
                    for (int i = 0; i <countryNoParsed; i++)
                    {
                        afc.CreateNewCountry(new LoginToken<Administrator>(), countriesFromRest[i]);
                    }

                    List<AirlineCompany> airlines = restWorking.AirlineCompnyApiWork();
                    for (int i = 0; i < airlineNoParsed; i++)
                    {
                        afc.CreateNewAirline(new LoginToken<Administrator>(), airlines[i]);
                    }

                    List<Customer> customersREST = restWorking.CustomerApiWork();
                    for (int i = 0; i < customerNoParsed; i++)
                    {
                        afc.CreateNewCustomer(new LoginToken<Administrator>(), customersREST[i]);
                    }

                    flightsPerCompany = new List<Flight>();
                    long airlineId = 0;
                    long countryCode = 0;
                    string airlineName = null;
                    long secondCountryCode = 0;
                    DateTime firstDate = new DateTime();
                    DateTime secondDate = new DateTime();
                    numberOfFlightsToCreate = RandomFlightToBeCreated();
                    for (int i = 0; i < flightNoParsed; i++)
                    {
                        airlineId = alf.GetRandomAirlineId();
                        airlineName = alf.GetAirlineNameByID((int)airlineId);
                        countryCode = alf.GetRandomCountryId();
                        secondCountryCode = alf.GetRandomCountryId();
                        firstDate = RandomDate();
                        secondDate = RandomDate();
                        flightsPerCompany.Add(new Flight(airlineId, countryCode, secondCountryCode, firstDate, secondDate, RandomTickets(), airlineName, RandomString()));
                    }
                    foreach (var flight in flightsPerCompany)
                    {
                        alf.CreateFlight(airlineToken, flight);
                    }
                    Random rnd = new Random();
                    int numberOfIds = 0,flightID=0;
                    List<long> customerIds = cfc.GetAllCustomerID();
                    LoginService customerLogin = new LoginService();

                    for (int i = 0; i < customerNoParsed; i++)
                    {
                        for (int j = 0; j < ticketNoParsed; j++)
                        {
                            numberOfIds = rnd.Next(flightNoParsed);
                            Tickets ticket = new Tickets();
                            customerLogin.TryCustomerLogin(restWorking.CustomerApiWork()[i].USER_NAME, restWorking.CustomerApiWork()[i].PASSWORD, out customerToken);
                            flightID = (int)alf.GetRandomFlightID();
                            ticket = cfc.PurchaseTicket(customerToken, afc.GetFlightById(flightID));
                            ticket.CUSTOMER_ID = customerIds[i];
                            ticket.FLIGHT_ID = flightID;
                            afc.AddTicket(ticket);
                        }
                    }
                    _bgWorker.DoWork += (s, e) =>
                    {
                        for (int i = 0; i < 101; i++)
                        {
                            Thread.Sleep(10);
                            ProgressStatus = i;
                        }
                    };
                });

            }
            catch (Exception )
            {
                throw new Exception("Something went wrong while trying to insert data...");
            }
        }
        public void RemoveDB()
        {
            FlightCenterConfig.DeleteDataBase();
        }
        public int RandomTickets()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 10);
            return randomNumber;
        }
        public int RandomFlightToBeCreated()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 10);
            return randomNumber;
        }
        public DateTime RandomDate()
        {
            Random rnd = new Random();
            int randomRangeOfDays = 4*365; //4 years...
            DateTime date = DateTime.Today.AddDays(rnd.Next(randomRangeOfDays)).AddHours(rnd.Next(0, 24)).AddMinutes(rnd.Next(0, 60)).AddSeconds(rnd.Next(0, 60));
            return date;
        }
        public string RandomString()
        {
            Random random = new Random();
            int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }

    }
}
