namespace GraduationProjectWebApi.DTOS;
public class HotelReservationDTO
{
    public int Days { get; set; }
    public decimal Price { get; set; }
    public DateTime BookDate { get; set; }
    public int? OfferId { get; set; }
    public int RoomId { get; set; }
}
