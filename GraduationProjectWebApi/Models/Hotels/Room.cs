namespace GraduationProjectWebApi.Models.Hotels;
public class Room : Entity
{
    public int Number { get; set; } //Added
    public bool Status { get; set; }
    public decimal Price { get; set; }

    public int TypeId { get; set; }
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; }
    public RoomType Type { get; set; }
}
