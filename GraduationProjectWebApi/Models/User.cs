namespace GraduationProjectWebApi.Models;
public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public List<OffersBooking> OffersBookings { get; set; }

    [JsonIgnore]
    public List<FlightBooking> FlightBookings { get; set; }

}
