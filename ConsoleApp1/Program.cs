using OP8.Models;
using OP8.Services;
using OP8.Search;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Program
{
    static void Main()
    {
        HotelManager HManager = new HotelManager();

        RoomManager RManager = new RoomManager();

        ClientManager CManager = new ClientManager();

        BookingManager BManager = new BookingManager();

        int CurrentOperation = 0;
        do
        {
            Console.WriteLine(@"Виберіть опцію з меню:
            [0] - Вихід

            ============Управління готелями==============
            [1] - Додати готель
            [2] - Видалити готель
            [3] - Змінити дані готелю
            [4] - Переглянути готель
            [5] - Переглянути всі готелі
            [6] - Пошук готелю за ключовим словом (Назвою)

            ============Управління номерами==============
            [7] - Додати номер до готелю
            [8] - Видалити номер з готелю
            [9] - Змінити дані номеру
            [10] - Переглянути номер
            [11] - Переглянути всі номери у готелі

            ============Управління клієнтами=============
            [12] - Додати клієнта
            [13] - Видалити клієнта
            [14] - Змінити дані клієнта
            [15] - Переглянути дані конкретного клієнта
            [16] - Переглянути дані про всіх клієнтів
            [17] - Відсортувати базу за ім'ям
            [18] - Відсортувати базу за прізвищем
            [19] - Пошук клієнта за ключовим словом (Прізвищем)

            ========Управління замовленнями номерів======
            [20] - Забронювати номер
            [21] - Скасувати бронювання
            [22] - Переглянути заброньовані номери у готелі
            [23] - Переглянути вільні номери у готелі
            [24] - Розрахувати вартість бронювання
            [25] - Переглянути бронювання в готелі за певний термін
            [26] - Переглянути дані клієнтів, що забронювали номер в готелі

            ");

            CurrentOperation = int.Parse(Console.ReadLine());

            switch (CurrentOperation)
            {
                case 0:
                    break;
                case 1:
                    {
                        Console.WriteLine("Введіть унікальну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель з такою назвою вже існує!\n");
                            break;
                        }
                        Console.WriteLine("Введіть опис готелю: ");
                        string HotelDescription = Console.ReadLine();
                        HManager.AddHotel(HotelName, HotelDescription);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введіть унікальну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.\n");
                            break;
                        }
                        HManager.RemoveHotel(HotelName);
                        RManager.RemoveHotel(HotelName);
                        Console.WriteLine($"Готель {HotelName} видалено.\n");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Введіть унікальну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Введіть нову назву готелю: ");
                        string NewName = Console.ReadLine();
                        if (HManager.IsHotelExist(NewName))
                        {
                            Console.WriteLine("Готель з такою назвою вже існує!\n");
                            break;
                        }
                        Console.WriteLine("Введіть новий опис готелю: ");
                        string NewDescription = Console.ReadLine();
                        HManager.ChangeHotelData(HotelName, NewName, NewDescription);
                        RManager.ChangeHotelData(HotelName, NewName);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Введіть точну унікальну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Hotel hotel = HManager.FindHotelByName(HotelName);
                        Console.WriteLine($"Назва: {hotel.Name}\n");
                        Console.WriteLine($"Опис: {hotel.Description}\n");
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Всі готелі:\n");
                        foreach (Hotel hotel in HManager.GetAllHotels())
                        {
                            Console.WriteLine($"Назва: {hotel.Name}\n");
                            Console.WriteLine($"Опис: {hotel.Description}\n");
                        }
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Введіть ключове слово для пошуку: ");
                        string keyword = Console.ReadLine();
                        SearchByHotel SByHotel = new SearchByHotel();
                        Console.WriteLine("Результат пошуку:\n");
                        foreach (Hotel hotel in SByHotel.SearchByKeyword(HManager.Hotels, keyword))
                        {
                            Console.WriteLine($"Назва: {hotel.Name}\n");
                            Console.WriteLine($"Опис: {hotel.Description}\n");
                        }
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Введіть назву нового номеру: ");
                        string RoomName = Console.ReadLine();
                        if (RManager.IsRoomExist(HotelName, RoomName))
                        {
                            Console.WriteLine("Такий номер вже існує.\n");
                            break;
                        }
                        decimal RoomPrice = decimal.Parse(Console.ReadLine());
                        string RoomDescription = Console.ReadLine();
                        RManager.AddRoom(HotelName, RoomName, RoomPrice, RoomDescription);
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Введіть назву нового номеру: ");
                        string RoomName = Console.ReadLine();
                        if (!RManager.IsRoomExist(HotelName, RoomName))
                        {
                            Console.WriteLine("Такий номер не існує.\n");
                            break;
                        }
                        RManager.RemoveRoom(HotelName, RoomName);
                        break;
                    }
                case 9:
                    {
                        int ChangeOperation;
                        break;
                    }
                case 10:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }

                        Console.WriteLine("Введіть точну назву номеру: ");
                        string RoomName = Console.ReadLine();
                        if (!RManager.IsRoomExist(HotelName, RoomName))
                        {
                            Console.WriteLine("Номер не знайдено.");
                            break;
                        }
                        Room room = RManager.FindRoomByName(HotelName, RoomName);
                        Console.WriteLine($"Назва: {room.Name}\n");
                        Console.WriteLine($"Ціна за ніч: {room.PricePerNight}\n");
                        Console.WriteLine($"Опис: {room.Description}\n");
                        break;
                    }
                case 11:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        foreach (Room room in RManager.GetAllRoomsInHotel(HotelName))
                        {
                            Console.WriteLine($"Назва: {room.Name}\n");
                            Console.WriteLine($"Ціна за ніч: {room.PricePerNight}\n");
                            Console.WriteLine($"Опис: {room.Description}\n");
                        }
                        break;
                    }
                case 12:
                    {
                        Console.WriteLine("Введіть номер телефону: ");
                        string ClientPhone = Console.ReadLine();
                        if (CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт вже зареєстрований.");
                            break;
                        }
                        Console.WriteLine("Введіть ім'я клієнта: ");
                        string ClientName = Console.ReadLine();
                        Console.WriteLine("Введіть прізвище клієнта: ");
                        string ClientSurname = Console.ReadLine();
                        CManager.AddClient(ClientPhone, ClientName, ClientSurname);
                        break;
                    }
                case 13:
                    {
                        Console.WriteLine("Введіть номер телефону: ");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт не зареєстрований.");
                            break;
                        }
                        CManager.RemoveClient(ClientPhone);
                        break;
                    }
                case 14:
                    {
                        break;
                    }
                case 15:
                    {
                        Console.WriteLine("Введіть номер телефону: ");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт не зареєстрований.");
                            break;
                        }
                        Client client = CManager.FindClientByPhone(ClientPhone);
                        Console.WriteLine($"Ім'я: {client.Name}\n");
                        Console.WriteLine($"Прізвище: {client.Surname}\n");
                        break;
                    }
                case 16:
                    {
                        Console.WriteLine("Введіть номер телефону: ");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт не зареєстрований.");
                            break;
                        }
                        foreach (Client client in CManager.GetAllClients())
                        {
                            Console.WriteLine($"Телефон: {client.Phone}\n");
                            Console.WriteLine($"Ім'я: {client.Name}\n");
                            Console.WriteLine($"Прізвище: {client.Surname}\n");
                        }
                        break;
                    }
                case 17:
                    {
                        CManager.SortByName();
                        Console.WriteLine("Дані відсортованно.");
                        break;
                    }
                case 18:
                    {
                        CManager.sortBySurname();
                        Console.WriteLine("Дані відсортованно.");
                        break;
                    }
                case 19:
                    {
                        Console.WriteLine("Введіть ключове слово для пошуку: ");
                        string keyword = Console.ReadLine();
                        SearchByClient SByClient = new SearchByClient();
                        Console.WriteLine("Результат пошуку:\n");
                        foreach (Client client in SByClient.SearchByKeyword(CManager.Clients, keyword))
                        {
                            Console.WriteLine($"Телефон: {client.Phone}\n");
                            Console.WriteLine($"Ім'я: {client.Name}\n");
                            Console.WriteLine($"Прізвище: {client.Surname}\n");
                        }
                        break;
                    }
                case 20:
                    {
                        break;
                    }
                case 21:
                    {
                        break;
                    }
                case 22:
                    {
                        break;
                    }
                case 23:
                    {
                        break;
                    }
                case 24:
                    {
                        break;
                    }
                case 25:
                    {
                        break;
                    }
                case 26:
                    {
                        break;
                    }
                default:
                    Console.WriteLine("Операція не знайдена!\n");
                    break;
            }
        } while (CurrentOperation != 0);
    }
}