namespace OP8.Services;

using OP8.Models;

public class RoomManager
{
    public Dictionary<string, Dictionary<string, Room>> Rooms {get; set;} = new Dictionary<string, Dictionary<string, Room>>();

    public bool IsRoomExist(string hotelName, string roomName)
    {
        if (Rooms[hotelName].ContainsKey(roomName))
        {
            return true;
        }
        return false;
    }

    public void RemoveHotel(string hotelName)
    {
        Rooms.Remove(hotelName);
    }

    public void ChangeHotelName(string oldname, string newname)
    {
        Rooms[newname] = Rooms[oldname];
        RemoveHotel(oldname);
    }

    public void AddRoom(string hotelName, string roomName, decimal price, string description)
    {
        Rooms[hotelName][roomName] = new Room(roomName, price, description);
    }

    public void RemoveRoom(string hotelName, string roomName)
    {
        Rooms[hotelName].Remove(roomName);
    }

    public void ChangeRoomName(string hotelName, string oldRoomName, string newRoomName)
    {
        Rooms[hotelName][newRoomName] = Rooms[hotelName][oldRoomName];
        RemoveRoom(hotelName, oldRoomName);
    }

    public void ChangeRoomPrice(string hotelname, string roomName, decimal price)
    {
        Rooms[hotelname][roomName].PricePerNight = price;
    }

    public void ChangeRoomDescription(string hotelname, string roomName, string description)
    {
        Rooms[hotelname][roomName].Description = description;
    }

    public Room FindRoomByName(string hotelName, string roomName)
    {
        return Rooms[hotelName][roomName];
    }

    public List<Room> GetAllRoomsInHotel(string hotelName)
    {
        List<Room> AllRoomsInHotel = new List<Room>();
        foreach ((_, Room room) in Rooms[hotelName])
        {
            AllRoomsInHotel.Add(room);
        }
        return AllRoomsInHotel;
    }

    public decimal CountTotalPrice(string hotelName, string roomName, DateTime start, DateTime end)
    {
        return ((end - start).Days * Rooms[hotelName][roomName].PricePerNight);
    }
}