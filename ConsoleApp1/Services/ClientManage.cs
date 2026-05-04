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
                break;
            }
        }
    }

    public void ChangeClientPhone(string oldPhone, string newPhone)
    {
        foreach (Client client in Clients)
        {
            if (client.Phone == oldPhone)
            {
                client.Phone = newPhone;
                break;
            }
        }
    }

    public void ChangeClientName(string phone, string name)
    {
        foreach (Client client in Clients)
        {
            if (client.Phone == phone)
            {
                client.Name = name;
                break;
            }
        }
    }

    public void ChangeClientSurname(string phone, string surname)
    {
        foreach (Client client in Clients)
        {
            if (client.Phone == phone)
            {
                client.Surname = surname;
                break;
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

    public void SortByName(int option)
    {
        if (option == 1)
        {
            Clients = Clients.OrderBy(s => s.Name).ToList();
        }
        else
        {
            Clients = Clients.OrderByDescending(s => s.Name).ToList();
        }
    }

    public void SortBySurname(int option)
    {
        if (option == 1)
        {
            Clients = Clients.OrderBy(s => s.Surname).ToList();
        }
        else
        {
            Clients = Clients.OrderByDescending(s => s.Surname).ToList();
        }
    }
}