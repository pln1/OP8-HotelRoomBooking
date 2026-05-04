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
}