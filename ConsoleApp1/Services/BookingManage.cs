namespace OP8.Services;

using System.Runtime.InteropServices;
using OP8.Models;

public class BookingManager
{
    public List<Booking> Bookings {get; set;} = new List<Booking>();

    public void AddBooking(string clientPhone, string bhotel, string broom, DateTime start, DateTime end, decimal total)
    {
        Bookings.Add(new Booking(clientPhone, bhotel, broom, start, end, total));
    }

    public void RemoveBooking(Booking removedBooking)
    {
        Bookings.Remove(removedBooking);
    }

    public void UpdatePhone(string oldPhone, string newPhone)
    {
        foreach (Booking booking in Bookings)
        {
            if (booking.ClientPhone == oldPhone)
            {
                booking.ClientPhone = newPhone;
                break;
            }
        }
    }

    public List<string> FindBookedRoomsInHotel(string hotelName)
    {
        List<string> BookedRooms = new List<string>();
        foreach (Booking booking in Bookings)
        {
            if (booking.BookedHotelName == hotelName && booking.StartDate >= DateTime.Now && booking.EndDate <= DateTime.Now)
            {
                BookedRooms.Add(booking.BookedRoomName);
            }
        }
        return BookedRooms;
    }

    public List<string> FindFreeRoomsInHotel(string hotelName, List<Room> rooms)
    {
        List<string> FreeRooms = new List<string>();
        List<string> BookedRooms = FindBookedRoomsInHotel(hotelName);
        foreach (Room room in rooms)
        {
            bool isNotBooked = true;
            foreach (string bookedName in BookedRooms)
            {
                if (room.Name == bookedName)
                {
                    isNotBooked = false;
                    break;
                }
            }
            if (isNotBooked)
            {
                FreeRooms.Add(room.Name);
            }
        }
        return FreeRooms;
    }

    public List<Booking> FindBookingsByPhone(string phone)
    {
        List<Booking> BookingsByPhone = new List<Booking>();
        foreach (Booking booking in Bookings)
        {
            if (booking.ClientPhone == phone)
            {
                BookingsByPhone.Add(booking);
            }
        }
        return BookingsByPhone;
    }

    public List<Booking> FindBookingsInPeriod(string hotelName, DateTime start, DateTime end)
    {
        List<Booking> BookingsInPeriod = new List<Booking>();
        foreach (Booking booking in Bookings)
        {
            if (booking.BookedHotelName == hotelName && booking.StartDate <= start && booking.EndDate >= end)
            {
                BookingsInPeriod.Add(booking);
            }
        }
        return BookingsInPeriod;
    }

    public List<string> GetTenant(string hotelName)
    {
        List<string> TenantList = new List<string>();
        foreach (Booking booking in Bookings)
        {
            if (booking.BookedHotelName == hotelName && DateTime.Now < booking.EndDate)
            {
                TenantList.Add(booking.ClientPhone);
            }
        }
        return TenantList;
    }
}