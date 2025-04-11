namespace GraduationProjectWebApi.DTOS;
public class FlightBookingDTO
{
    public decimal Price { get; set; }
    public DateTime BookDate { get; set; }

    public int UserId { get; set; }
    public int FlightId { get; set; }
    public int? OfferId { get; set; }
}
