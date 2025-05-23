namespace GraduationProjectWebApi.Models.Hotels;
public class RoomType : Entity
{
    public string Name { get; set; }
    public decimal Size { get; set; }
    [JsonIgnore]
    public List<Room> Rooms { get; set; } = [];
}
