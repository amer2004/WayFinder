namespace GraduationProjectWebApi.DTOS;
public class GuideDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Image { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public int AdminId { get; set; }
    public Nationality? Nationality { get; set; }
}
