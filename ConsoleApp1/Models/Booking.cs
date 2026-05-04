namespace OP8.Models;

public class Booking
{
    public Guid? GuidId {get; set;} = Guid.NewGuid();

    public string? ClientPhone {get; set;}

    public string? BookedHotelName {get; set;}

    public string? BookedRoomName {get; set;}

    public DateTime StartDate {get; set;}

    public DateTime EndDate {get; set;}

    public decimal TotalPrice {get; set;}

    public Booking(string clientPhone, string bhotel, string broom, DateTime start, DateTime end, decimal price)
    {
        ClientPhone = clientPhone;
        BookedHotelName = bhotel;
        BookedRoomName = broom;
        StartDate = start;
        EndDate = end;
        TotalPrice = price;
    }
}