namespace GraduationProjectWebApi.DTOS;

public class FlightDTO
{
    public int Number { get; set; }
    public DateTime Departure { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int AirLineId { get; set; }
    public int AdminId { get; set; }
    public int DepartureLocationId { get; set; }
    public int DestinationLocationId { get; set; }
}
