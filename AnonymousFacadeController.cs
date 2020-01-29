using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MainProject;
using MainProject.Business_Logic;

namespace WebApiPart.Controllers
{
    public class AnonymousFacadeController : ApiController
    {
        private AnonymousUserFacade anonymousUser = new AnonymousUserFacade();

        [ResponseType(typeof(List<Flight>))]
        [Route("api/AnonymousFacade/GetAllFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            if (anonymousUser.GetAllFlights() == null)
                return NotFound();
            return Ok(anonymousUser.GetAllFlights());
        }

        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/AnonymousFacade/GetAllAirlines")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            if (anonymousUser.GetAllAirlineCompanies() == null)
                return NotFound();
            return Ok(anonymousUser.GetAllAirlineCompanies());
        }
        [ResponseType (typeof(Dictionary<Flight, int>))]
        [Route ("api/AnonymousFacade/GetAllFlightVacancy")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            if (anonymousUser.GetAllFlightsVacancy() == null)
                return NotFound();
            return Ok(anonymousUser.GetAllFlightsVacancy());
        }
        [ResponseType (typeof(Flight))]
        [Route("api/AnonymousFacade/{id}",Name ="GetFlightById")]
        [HttpGet]
        public IHttpActionResult GetFlightById(int id)
        {
            if (anonymousUser.GetFlightById(id) == null)
                return NotFound();
            return Ok(anonymousUser.GetFlightById(id));
        }
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/GetFlightsByOrigCountry")]
        [HttpGet]
        public IHttpActionResult GetFlightsByOriginCountry(int countryCode)
        {
            if (anonymousUser.GetFlightsByOriginCountry(countryCode) == null)
                return NotFound();
            return Ok((anonymousUser.GetFlightsByOriginCountry(countryCode)));
        }

        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/GetFlightsByDesCountry")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDestinationCountry(int countryCode)
        {
            if (anonymousUser.GetFlightsByDestinationCountry(countryCode) == null)
                return NotFound();
            return Ok((anonymousUser.GetFlightsByDestinationCountry(countryCode)));
        }
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/GetFlightsByDepDate")]
        [HttpGet]
        IHttpActionResult GetFlightsByDepatrureDate(DateTime departureDate)
        {
            if (anonymousUser.GetFlightsByDepatrureDate(departureDate) == null)
                return NotFound();
            return Ok((anonymousUser.GetFlightsByDepatrureDate(departureDate)));
        }
        [ResponseType(typeof(Flight))]
        [Route("api/AnonymousFacade/GetFlightsByLandDate")]
        [HttpGet]
        public IHttpActionResult GetFlightsByLandingDate(DateTime landingDate)
        {
            if (anonymousUser.GetFlightsByLandingDate(landingDate) == null)
                return NotFound();
            return Ok((anonymousUser.GetFlightsByLandingDate(landingDate)));
        }

    }
}
