class Flight {
    constructor(id, airlineCompanyId, originCountryCode, destinationCountryCode, departureTime, landingTime, remainingTickets, status) {
        this.Id = id
        this.AirlineCompanyId = airlineCompanyId
        this.OriginCountryCode = originCountryCode
        this.DestinationCountryCode = destinationCountryCode
        this.DepartureTime = departureTime
        this.LandingTime = landingTime
        this.RemainingTickets = remainingTickets
        if (status == undefined) {
            this.status = "Landing..."
        }

    }
    
}
class David {
    name;
    age;


}