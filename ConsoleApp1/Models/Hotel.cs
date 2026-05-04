namespace OP8.Models;

public class Hotel
{
    public string? Name {get; set;}

    public string? Description {get; set;}
    
    public Hotel(string name, string description)
    {
        Name = name;
        Description = description;
    }
}