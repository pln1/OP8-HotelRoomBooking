namespace OP8.Services;

using OP8.Models;

public class ClientManager
{
    public List<Client> Clients {get; set;} = new List<Client>();

    public void AddClient(Client newClient)
    {
        Clients.Add(newClient);
    }

    public void RemoveClient(Client removedClient)
    {
        Clients.Remove(removedClient);
    }

    public void SortByName()
    {
        Clients = Clients.OrderBy(s => s.Name).ToList();
    }

    public void sortBySurname()
    {
        Clients = Clients.OrderBy(s => s.Surname).ToList();
    }
}