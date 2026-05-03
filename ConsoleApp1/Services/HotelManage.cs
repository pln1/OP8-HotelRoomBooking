namespace OP8.Services;

using OP8.Models;

public class HotelManager
{
    public List<Hotel> Hotels {get; set;} = new List<Hotel>();

    public void AddHotel(string newName, string newDescription)
    {
        Hotels.Add(new Hotel(newName, newDescription));
    }

    public bool RemoveHotel(string removedName)
    {
        foreach (Hotel hotel in Hotels)
        {
            if(hotel.Name == removedName)
            {
                Hotels.Remove(hotel);
                return true;
            }
        }
        return false;
    }

    public bool ChangeHotelData(string oldname, string newname, string newdescription)
    {
        foreach (Hotel hotel1 in Hotels)
        {
            if (hotel1.Name == oldname)
            {
                foreach (Hotel hotel2 in Hotels)
                {
                    if (hotel2.Name == newname)
                    {
                        return false;
                    }
                }

                hotel1.Name = newname;
                hotel1.Description = newdescription;
            }
        }
        return false;
    }

    public Hotel GetHotelByName(string name)
    {
        return Hotels.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}