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

    public bool IsRoomExist(string roomName)
    {
        foreach (Room room in Rooms)
        {
            if (room.Name == roomName)
            {
                return true;
            }
        }
        return false;
    }

    public void AddRoom(string roomName, decimal price, string description)
    {
        Rooms.Add(new Room(roomName, price, description));
    }

    public void RemoveRoom(string roomName)
    {
        Rooms.Remove(FindRoomByName(roomName));
    }

    public void ChangeRoomName(string oldRoomName, string newRoomName)
    {
        foreach (Room room in Rooms)
        {
            if (room.Name == oldRoomName)
            {
                room.Name = newRoomName;
                break;
            }
        }
    }

    public void ChangeRoomPrice(string roomName, decimal price)
    {
        foreach (Room room in Rooms)
        {
            if (room.Name == roomName)
            {
                room.PricePerNight = price;
                break;
            }
        }
    }

    public void ChangeRoomDescription(string roomName, string description)
    {
        foreach (Room room in Rooms)
        {
            if (room.Name == roomName)
            {
                room.Description = description;
                break;
            }
        }
    }

    public Room FindRoomByName(string roomName)
    {
        foreach (Room room in Rooms)
        {
            if (room.Name == roomName)
            {
                return room;
            }
        }
        return new Room("RoomNotFound", 0, "RoomBotFound");
    }

    public List<Room> GetAllRoomsInHotel()
    {
        return Rooms;
    }

    public decimal CountTotalPrice(string roomName, DateTime start, DateTime end)
    {
        return ((end - start).Days * FindRoomByName(roomName).PricePerNight);
    }
}