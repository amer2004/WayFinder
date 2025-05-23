namespace GraduationProjectWebApi.DTOS;
public class RoomDTO
{
    public int Number { get; set; } 
    public bool Status { get; set; }
    public decimal Price { get; set; }
    public string? Image1 { get; set; }
    public string? Image2 { get; set; }
    public string? Image3 { get; set; }

    public int TypeId { get; set; }
    public int HotelId { get; set; }
}
