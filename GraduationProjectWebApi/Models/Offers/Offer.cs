namespace GraduationProjectWebApi.Models.Offers;
public class Offer : Entity
{
    public string Name { get; set; }
    public string Details { get; set; }
    public int Days { get; set; }
    public decimal Price { get; set; }

    public int? FlightId { get; set; }
    public int? HotelId { get; set; }
    public int? GuideId { get; set; }

    public Flight? Flight { get; set; }
    public Hotel? Hotel { get; set; }
    public Guide? Guide { get; set; }
    public List<OffersBooking> Bookings { get; set; }
}
