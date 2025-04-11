namespace GraduationProjectWebApi.Models.Flights;
public class Flight : Entity
{
    //public decimal Price { get; set; } Moved to FlightBooking
    public int Number { get; set; }//Added instead of Name
    public DateTime Departure { get; set; }
    public DateTime ArrivalTime { get; set; }

    public int AirLineId { get; set; }
    public int DepartureLocationId { get; set; }
    public int DestinationLocationId { get; set; }

    [JsonIgnore]
    public AirLine? AirLine { get; set; } = null;
    [JsonIgnore]
    public Location? DepartureLocation { get; set; } = null;
    [JsonIgnore]
    public Location? DestinationLocation { get; set; } = null;
}