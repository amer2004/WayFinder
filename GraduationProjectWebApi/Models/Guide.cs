namespace GraduationProjectWebApi.Models;
public class Guide:Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AdminId { get; set; }
    public string? Image { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Nationality? Nationality { get; set; }

    public Admin Admin { get; set; }
    [JsonIgnore]
    public List<Offer> Offers { get; set; } = [];
}
