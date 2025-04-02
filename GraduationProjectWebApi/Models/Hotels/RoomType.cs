namespace GraduationProjectWebApi.Models.Hotels;
public class RoomType : Entity
{
    public decimal Size { get; set; }
    [JsonIgnore]
    public List<Room> Rooms { get; set; } = [];
}
