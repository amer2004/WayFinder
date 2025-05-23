namespace GraduationProjectWebApi.DTOS;
public class OfferDTO
{
    public string Name { get; set; }
    public string Details { get; set; }
    public int Days { get; set; }
    public decimal Price { get; set; }
    public string? Image1 { get; set; }
    public string? Image2 { get; set; }
    public string? Image3 { get; set; }
    public string? Image4 { get; set; }

    public int? FlightId { get; set; }
    public int? HotelId { get; set; }
    public int? GuideId { get; set; }
}