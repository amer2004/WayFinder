namespace GraduationProjectWebApi.Models.Offers;
public class OffersBooking:Entity
{
    public DateTime BookDate { get; set; }
    public decimal Price { get; set; }

    public int OfferId { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public Offer Offer { get; set; }
}
