namespace GraduationProjectWebApi.DTOS;
public class RoomDTO
{
    public int Number { get; set; } 
    public bool Status { get; set; }
    public decimal Price { get; set; }

    public int TypeId { get; set; }
    public int HotelId { get; set; }
}
