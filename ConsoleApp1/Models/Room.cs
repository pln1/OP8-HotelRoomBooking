namespace OP8.Models;

public class Room
{
    public string? Name {get; set;}

    public decimal PricePerNight {get; set;}

    public string? Description {get; set;}

    public Room(string name, decimal price, string description)
    {
        Name = name;
        PricePerNight = price;
        Description = description;
    }

    public List<Booking> Bookings { get; set; } = new List<Booking>();

    public bool IsAvailable(DateTime checkIn, DateTime checkOut)
    {
        foreach (Booking booking in Bookings)
        {
            if (checkIn < booking.EndDate && checkOut > booking.StartDate)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsAvailableNow()
    {
        DateTime now = DateTime.Now;

        foreach (Booking booking in Bookings)
        {
            if (now < booking.EndDate && now > booking.StartDate)
            {
                return false;
            }
        }
        return true;
    }
}