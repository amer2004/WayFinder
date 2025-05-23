namespace GraduationProjectWebApi.DTOS;

public class HotelDTO
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    public decimal Ratings { get; set; }
    public int AdminId { get; set; }
    public string? Image1 { get; set; }
    public string? Image2 { get; set; }
}
