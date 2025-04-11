namespace GraduationProjectWebApi.Models.Flights;
public class AirLine:Entity
{
    public string Name { get; set; }
    public string Location { get; set; }

    public int AdminId { get; set; }
    [JsonIgnore]
    public Admin? Admin { get; set; }
    [JsonIgnore]
    public List<Flight> Flights { get; set; } = [];
}
