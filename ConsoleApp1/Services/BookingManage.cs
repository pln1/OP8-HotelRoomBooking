namespace OP8.Services;

using OP8.Models;

public class BookingManager
{
    public List<Booking> Bookings {get; set;} = new List<Booking>();

    public void AddBooking(Client tenant, Hotel bhotel, Room broom)
    {
        Bookings.Add(new Booking(tenant, bhotel, broom, ));
    }

    public void RemoveBooking(Booking removedBooking)
    {
        Bookings.Remove(removedBooking);
    }

}