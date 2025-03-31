namespace GraduationProjectWebApi.Models.Flights;
public class FlightBooking:Entity
{
    public decimal Price { get; set; }
    public DateTime BookDate { get; set; }

    public int UserId { get; set; }
    public int FlightId { get; set; }
    public int? OfferId { get; set; }

    public User User { get; set; }
    public Flight Flight { get; set; }
    public Offer? Offer { get; set; }
}
