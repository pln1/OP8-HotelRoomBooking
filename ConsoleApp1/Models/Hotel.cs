namespace OP8.Models;

public class Hotel
{
    public string? Name {get; set;}

    public string? Description {get; set;}

    public List<Room> Rooms {get; set;} = new List<Room>();

    public Hotel(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddRoom(Room newRoom)
    {
        Rooms.Add(newRoom);
    }

    public void RemoveRoom(Room removedRoom)
    {
        Rooms.Remove(removedRoom);
    }

    public List<Room> ReturnBookedRooms()
    {
        List<Room> result = new List<Room>();

        foreach (Room i in Rooms)
        {
            if (!i.IsAvailableNow())
            {
                result.Add(i);
            }
        }

        return result;
    }

    public List<Room> ReturnFreeRooms()
    {
        List<Room> result = new List<Room>();

        foreach (Room i in Rooms)
        {
            if (i.IsAvailableNow())
            {
                result.Add(i);
            }
        }

        return result;
    }
}