namespace GraduationProjectWebApi.DTOS;
public class OffersBookingDTO
{
    public DateTime BookDate { get; set; }
    public decimal Price { get; set; }
    public int OfferId { get; set; }
    public int UserId { get; set; }
}
