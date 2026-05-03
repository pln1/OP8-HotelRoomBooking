namespace OP8.Models;

public class Booking
{
    public Client? Tenant {get; set;}

    public Hotel? BookedHotel {get; set;}

    public Room? BookedRoom {get; set;}

    public DateTime StartDate {get; set;}

    public DateTime EndDate {get; set;}

    public decimal TotalPrice {get; set;}

    public string? Description {get; set;}

    public Booking(Client tenant, Hotel bhotel, Room broom, DateTime start, DateTime end, decimal price, string description)
    {
        Tenant = tenant;
        BookedHotel = bhotel;
        BookedRoom = broom;
        StartDate = start;
        EndDate = end;
        TotalPrice = price;
        Description = description;
    }
}