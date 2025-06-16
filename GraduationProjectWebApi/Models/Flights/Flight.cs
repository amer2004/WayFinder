namespace GraduationProjectWebApi.Models.Flights;
public class Flight : Entity
{
    public int Number { get; set; }
    public DateTime Departure { get; set; }
    public DateTime ArrivalTime { get; set; }

    public int AirLineId { get; set; }
    public int DepartureLocationId { get; set; }
    public int DestinationLocationId { get; set; }
    public int AdminId { get; set; }

    [JsonIgnore]
    public Admin Admin { get; set; }
    [JsonIgnore]
    public AirLine? AirLine { get; set; } = null;
    [JsonIgnore]
    public Location? DepartureLocation { get; set; } = null;
    [JsonIgnore]
    public Location? DestinationLocation { get; set; } = null;
}