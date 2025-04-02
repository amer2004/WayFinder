namespace GraduationProjectWebApi.Models;
public class Admin : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AdminType Type { get; set; }

    [JsonIgnore]
    public List<Hotel> Hotels { get; set; } = [];
    [JsonIgnore]
    public List<AirLine> AirLines { get; set; } = [];
}
