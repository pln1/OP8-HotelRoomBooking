namespace OP8.Services;

using OP8.Models;

public class BookingManager
{
    public List<Booking> Bookings {get; set;} = new List<Booking>();

    public void AddBooking(string clientPhone, string bhotel, string broom, DateTime start, DateTime end, decimal total, string description)
    {
        Bookings.Add(new Booking(clientPhone, bhotel, broom, start, end, total, description));
    }

    public void RemoveBooking(Booking removedBooking)
    {
        Bookings.Remove(removedBooking);
    }

}