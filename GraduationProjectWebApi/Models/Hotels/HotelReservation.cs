namespace GraduationProjectWebApi.Models.Hotels;
public class HotelReservation:Entity
{
    public int Days { get; set; }
    public decimal Price { get; set; }

    public int? OfferId { get; set; }
    public int RoomId { get; set; }

    public Offer? Offer { get; set; }
    public Room Room { get; set; }
}