namespace OP8.Search;

using Models;

public class SearchByHotel : SearchBy<Hotel>
{
    public override List<Hotel> SearchByKeyword(List<Hotel> allHotels, string keyword)
    {
        List<Hotel> foundHotels = new List<Hotel>();

        foreach (var hotel in allHotels)
        {
            if (IsMatch(hotel.Name, keyword))
            {
                foundHotels.Add(hotel);
            }
        }

        return foundHotels;
    }
}