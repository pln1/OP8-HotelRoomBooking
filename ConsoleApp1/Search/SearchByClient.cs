namespace OP8.Search;

using Models;

public class SearchByClient : SearchBy<Client>
{
    public override List<Client> SearchByKeyword(List<Client> allClients, string keyword)
    {
        List<Client> foundClients = new List<Client>();

        foreach (var client in allClients)
        {
            if (IsMatch(client.Surname, keyword))
            {
                foundClients.Add(client);
            }
        }

        return foundClients;
    }
}