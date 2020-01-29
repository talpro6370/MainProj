using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPart.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string AirlineCompanyId { get; set; }
        public string OriginCountryCode { get; set; }
        public string DestinationCountryCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }
        public Flight()
        {

        }
        public Flight(int id, string airlineCompanyId, string originCountryCode, string destinationCountryCode, DateTime departureTime, DateTime landingTime, int remainingTickets)
        {
            Id = id;
            AirlineCompanyId = airlineCompanyId;
            OriginCountryCode = originCountryCode;
            DestinationCountryCode = destinationCountryCode;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTickets = remainingTickets;
        }
        public override string ToString()
        {
            return $"Id {Id} AirlineCompanyId {AirlineCompanyId} OriginCountryCode {OriginCountryCode} " +
                $"DestinationCountryCode {DestinationCountryCode} DepartureTime {DepartureTime} " +
                $"LandingTime {LandingTime} RemainingTickets {RemainingTickets}";
        }
    }
}