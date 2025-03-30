namespace GraduationProjectWebApi.Models;
public class Guide:Entity
{
    public string Name { get; set; }
    public int AdminId { get; set; }

    public Admin Admin { get; set; }
}
