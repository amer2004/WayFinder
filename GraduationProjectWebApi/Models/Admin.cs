namespace GraduationProjectWebApi.Models;
public class Admin : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AdminType Type { get; set; }

    [JsonIgnore]
    public List<Hotel> Hotels { get; set; } = [];
    [JsonIgnore]
    public List<AirLine> AirLines { get; set; } = [];
    [JsonIgnore]
    public List<Flight> Flights { get; set; } = [];
}
