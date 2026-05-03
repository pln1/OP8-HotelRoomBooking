namespace OP8.Models;

public class Client
{
    public string? Phone {get; set;}

    public string? Name {get; set;}

    public string? Surname {get; set;}

    public Client(string phone, string name, string surname)
    {
        Phone = phone;
        Name = name;
        Surname = surname;
    }
}