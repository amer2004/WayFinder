namespace GraduationProjectWebApi.Models;
public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Nationality Nationality { get; set; }

    [JsonIgnore]
    public List<OffersBooking> OffersBookings { get; set; } = [];

    [JsonIgnore]
    public List<FlightBooking> FlightBookings { get; set; } = [];
}
