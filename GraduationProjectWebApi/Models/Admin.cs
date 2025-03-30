namespace GraduationProjectWebApi.Models;
public class Admin : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public List<Hotel> Hotels { get; set; }
    public List<AirLine> AirLines { get; set; }
}
