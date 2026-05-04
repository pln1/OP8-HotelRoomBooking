namespace OP8.Services;

using OP8.Models;

public class HotelManager
{
    public List<Hotel> Hotels {get; set;} = new List<Hotel>();

    public bool IsHotelExist(string name)
    {
        foreach (Hotel hotel in Hotels)
        {
            if (hotel.Name == name)
            {
                return true;
            }
        }
        return false;
    }

    public void AddHotel(string newName, string newDescription)
    {
        Hotels.Add(new Hotel(newName, newDescription));
    }

    public void RemoveHotel(string removedName)
    {
        foreach (Hotel hotel in Hotels)
        {
            if(hotel.Name == removedName)
            {
                Hotels.Remove(hotel);
                break;
            }
        }
    }

    public void ChangeHotelName(string oldName, string newName)
    {
        foreach (Hotel hotel1 in Hotels)
        {
            if (hotel1.Name == oldName)
            {
                hotel1.Name = newName;
                break;
            }
        }
    }

    public void ChangeHotelDescription(string hotelName, string newDescription)
    {
        foreach (Hotel hotel1 in Hotels)
        {
            if (hotel1.Name == hotelName)
            {
                hotel1.Description = newDescription;
                break;
            }
        }
    }

    public Hotel FindHotelByName(string name)
    {
        foreach (Hotel hotel in Hotels)
        {
            if (hotel.Name == name)
            {
                return hotel;
            }
        }
        return new Hotel("HotelNoTFound", "HotelNotFound");
    }

    public List<Hotel> GetAllHotels()
    {
        return Hotels;
    }
}