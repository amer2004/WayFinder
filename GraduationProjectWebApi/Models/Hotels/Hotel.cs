namespace GraduationProjectWebApi.Models.Hotels;
public class Hotel:Entity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    public decimal Ratings { get; set; }

    public int AdminId { get; set; }

    public Admin Admin { get; set; }
    public List<Room> Rooms { get; set; }
}
