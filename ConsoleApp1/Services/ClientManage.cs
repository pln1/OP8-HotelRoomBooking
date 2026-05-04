namespace OP8.Services;

using OP8.Models;

public class ClientManager
{
    public List<Client> Clients {get; set;} = new List<Client>();

    public bool IsClientExist(string phone)
    {
        foreach (Client client in Clients)
        {
            if (client.Phone == phone)
            {
                return true;
            }
        }
        return false;
    }

    public void AddClient(string phone, string name, string surname)
    {
        Clients.Add(new Client(phone, name, surname));
    }

    public void RemoveClient(string phone)
    {
        foreach (Client client in Clients)
        {
            if (client.Phone == phone)
            {
                Clients.Remove(client);
            }
        }
    }

    public Client FindClientByPhone(string phone)
    {
        foreach (Client client in Clients)
        {
            if (client.Phone == phone)
            {
                return client;
            }
        }

        return new Client("ClientNotFound", "ClientNotFound", "ClientNotFound");
    }

    public List<Client> GetAllClients()
    {
        return Clients;
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