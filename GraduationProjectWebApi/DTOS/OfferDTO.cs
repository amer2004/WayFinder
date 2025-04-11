namespace GraduationProjectWebApi.DTOS;
public class OfferDTO
{
    public string Name { get; set; }
    public string Details { get; set; }
    public int Days { get; set; }
    public decimal Price { get; set; }

    public int? FlightId { get; set; }
    public int? HotelId { get; set; }
    public int? GuideId { get; set; }
}