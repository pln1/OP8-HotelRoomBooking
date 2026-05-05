using OP8.Models;
using OP8.Services;
using OP8.Search;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        HotelManager HManager = new HotelManager();
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

            CurrentOperation = GetInt();

            switch (CurrentOperation)
            {
                case 0:
                    break;
                case 1:
                    {
                        string HotelName = GetString("Введіть унікальну назву готелю: ");
                        if (HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель з такою назвою вже існує!\n");
                            break;
                        }
                        string HotelDescription = GetString("Введіть опис готелю: ");
                        HManager.AddHotel(HotelName, HotelDescription);
                        Console.WriteLine($"Готель {HotelName} успішно додано до бази.\n");
                        break;
                    }
                case 2:
                    {
                        if (!TryGetHotel(HManager, "Введіть унікальну назву готелю: ", "Готель не знайдено.\n", out string HotelName)) break;
                        HManager.RemoveHotel(HotelName);
                        BManager.RemoveHotel(HotelName);
                        Console.WriteLine($"Готель {HotelName} видалено.\n");
                        break;
                    }
                case 3:
                    {
                        if (!TryGetHotel(HManager, "Введіть унікальну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        int operation = GetInt("Виберіть поле для внесення змін:\n[1] - Назва готелю\n[2] - Опис готелю\n");
                        if (operation == 1)
                        {
                            string NewName = GetString("Введіть нову назву готелю: ");
                            if (HManager.IsHotelExist(NewName))
                            {
                                Console.WriteLine("Готель з такою назвою вже існує!\n");
                                break;
                            }
                            HManager.ChangeHotelName(HotelName, NewName);
                        }
                        else if (operation == 2)
                        {
                            string NewDescription = GetString("Введіть новий опис готелю: ");
                            HManager.ChangeHotelDescription(HotelName, NewDescription);
                        }
                        else
                        {
                            Console.WriteLine("Неправильна операція.\n");
                            break;
                        }
                        Console.WriteLine("Зміни внесено успішно.\n");
                        break;
                    }
                case 4:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну унікальну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        PrintHotel(HManager.FindHotelByName(HotelName));
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Всі готелі:\n");
                        foreach (Hotel hotel in HManager.GetAllHotels())
                        {
                            PrintHotel(hotel, true);
                        }
                        break;
                    }
                case 6:
                    {
                        string keyword = GetString("Введіть ключове слово для пошуку: ");
                        SearchByHotel SByHotel = new SearchByHotel();
                        Console.WriteLine("Результат пошуку:\n");
                        foreach (Hotel hotel in SByHotel.SearchByKeyword(HManager.Hotels, keyword))
                        {
                            PrintHotel(hotel);
                        }
                        break;
                    }
                case 7:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        string RoomName = GetString("Введіть назву нового номеру: ");
                        if (HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Такий номер вже існує.\n");
                            break;
                        }
                        decimal RoomPrice = GetDecimal("Введіть ціну номера за ніч: \n");
                        string RoomDescription = GetString("Введіть опис номеру: \n");
                        HManager.FindHotelByName(HotelName).AddRoom(RoomName, RoomPrice, RoomDescription);
                        Console.WriteLine($"Кімнату {RoomName} успішно додано до готелю {HotelName}.\n");
                        break;
                    }
                case 8:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        if (!TryGetRoom(HManager, HotelName, "Введіть назву номеру: ", "Такий номер не існує.\n", out string RoomName)) break;
                        HManager.FindHotelByName(HotelName).RemoveRoom(RoomName);
                        BManager.RemoveRoom(HotelName, RoomName);
                        Console.WriteLine($"Номер {RoomName} успішно видалено.\n");
                        break;
                    }
                case 9:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        if (!TryGetRoom(HManager, HotelName, "Введіть назву номеру: ", "Такий номер не існує.\n", out string RoomName)) break;
                        int operation = GetInt("Виберіть поле для внесення змін:\n[1] - Назва номеру\n[2] - Ціна за добу\n[3] - Опис\n");
                        if (operation == 1)
                        {
                            string NewRoomName = GetString("Введіть нову назву номеру: ");
                            if (HManager.FindHotelByName(HotelName).IsRoomExist(NewRoomName))
                            {
                                Console.WriteLine("Такий номер існує.\n");
                                break;
                            }
                            HManager.FindHotelByName(HotelName).ChangeRoomName(RoomName, NewRoomName);
                        }
                        else if (operation == 2)
                        {
                            decimal NewPrice = GetDecimal("Введіть нову ціну номеру за добу: ");
                            HManager.FindHotelByName(HotelName).ChangeRoomPrice(RoomName, NewPrice);
                        }
                        else if (operation == 3)
                        {
                            string NewDescription = GetString("Введіть новий опис номеру: ");
                            HManager.FindHotelByName(HotelName).ChangeRoomDescription(RoomName, NewDescription);
                        }
                        else
                        {
                            Console.WriteLine("Неправильна операція.\n");
                        }
                        Console.WriteLine("Зміни внесено успішно.\n");
                        break;
                    }
                case 10:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        if (!TryGetRoom(HManager, HotelName, "Введіть точну назву номеру: ", "Номер не знайдено.", out string RoomName)) break;
                        PrintRoom(HManager.FindHotelByName(HotelName).FindRoomByName(RoomName));
                        break;
                    }
                case 11:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        foreach (Room room in HManager.FindHotelByName(HotelName).GetAllRoomsInHotel())
                        {
                            PrintRoom(room);
                        }
                        break;
                    }
                case 12:
                    {
                        string ClientPhone = GetString("Введіть номер телефону: ");
                        if (CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Такий клієнт вже зареєстрований.");
                            break;
                        }
                        string ClientName = GetString("Введіть ім'я клієнта: ");
                        string ClientSurname = GetString("Введіть прізвище клієнта: ");
                        CManager.AddClient(ClientPhone, ClientName, ClientSurname);
                        Console.WriteLine("Кліента успішно додано до бази.\n");
                        break;
                    }
                case 13:
                    {
                        if (!TryGetClient(CManager, "Введіть номер телефону: ", "Такий клієнт не зареєстрований.", out string ClientPhone)) break;
                        CManager.RemoveClient(ClientPhone);
                        BManager.RemoveClient(ClientPhone);
                        Console.WriteLine("Кліента успішно видалено.\n");
                        break;
                    }
                case 14:
                    {
                        if (!TryGetClient(CManager, "Введіть номер телефону: ", "Клієнт не зареєстрований.", out string ClientPhone)) break;
                        int operation = GetInt("Виберіть поле для внесення змін:\n[1] - Номер телефону\n[2] - Ім'я\n[3] - Прізвище\n");
                        if (operation == 1)
                        {
                            string NewPhone = GetString("Введіть новий номер телефону: ");
                            if (CManager.IsClientExist(NewPhone))
                            {
                                Console.WriteLine("Такий кліент вже зареєстрований.\n");
                                break;
                            }
                            CManager.ChangeClientPhone(ClientPhone, NewPhone);
                            BManager.UpdatePhone(ClientPhone, NewPhone);
                        }
                        else if (operation == 2)
                        {
                            string NewName = GetString("Введіть нове ім'я: ");
                            CManager.ChangeClientName(ClientPhone, NewName);
                        }
                        else if (operation == 3)
                        {
                            string NewSurname = GetString("Введіть нове прізвище: ");
                            CManager.ChangeClientSurname(ClientPhone, NewSurname);
                        }
                        else
                        {
                            Console.WriteLine("Неправильна операція.\n");
                        }
                        Console.WriteLine("Зміни внесено успішно.\n");
                        break;
                    }
                case 15:
                    {
                        if (!TryGetClient(CManager, "Введіть номер телефону: ", "Клієнт не зареєстрований.", out string ClientPhone)) break;
                        Client client = CManager.FindClientByPhone(ClientPhone);
                        Console.WriteLine($"Ім'я: {client.Name}\n");
                        Console.WriteLine($"Прізвище: {client.Surname}\n");
                        break;
                    }
                case 16:
                    {
                        foreach (Client client in CManager.GetAllClients())
                        {
                            PrintClient(client);
                        }
                        break;
                    }
                case 17:
                    {
                        int ascdesc = GetInt("Виберіть тип сортування:\n[1] - За зростанням\n[2] - За спаданням\n");
                        if (ascdesc != 1 && ascdesc != 2)
                        {
                            Console.WriteLine("Неправильна операція.\n");
                            break;
                        }
                        CManager.SortByName(ascdesc);
                        Console.WriteLine("Дані відсортованно.\n");
                        break;
                    }
                case 18:
                    {
                        int ascdesc = GetInt("Виберіть тип сортування:\n[1] - За зростанням\n[2] - За спаданням\n");
                        if (ascdesc != 1 && ascdesc != 2)
                        {
                            Console.WriteLine("Неправильна операція.\n");
                            break;
                        }
                        CManager.SortBySurname(ascdesc);
                        Console.WriteLine("Дані відсортованно.\n");
                        break;
                    }
                case 19:
                    {
                        string keyword = GetString("Введіть ключове слово для пошуку: ");
                        SearchByClient SByClient = new SearchByClient();
                        Console.WriteLine("Результат пошуку:\n");
                        foreach (Client client in SByClient.SearchByKeyword(CManager.Clients, keyword))
                        {
                            PrintClient(client);
                        }
                        break;
                    }
                case 20:
                    {
                        if (!TryGetClient(CManager, "Введіть номер телефону кліента: \n", "Клієнт не зареєстрований.", out string ClientPhone)) break;
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: \n", "Готель не знайдено.", out string HotelName)) break;
                        if (!TryGetRoom(HManager, HotelName, "Введіть точну назву номеру: \n", "Номер не знайдено.", out string RoomName)) break;
                        if (!TryGetBookingDates(out DateTime StartDate, out DateTime EndDate)) break;
                        decimal Price = HManager.FindHotelByName(HotelName).CountTotalPrice(RoomName, StartDate, EndDate);
                        if (!BManager.IsRoomFree(HotelName, RoomName, StartDate, EndDate))
                        {
                            Console.WriteLine("Номер на цей період вже заброньовано!\n");
                            break;
                        }
                        BManager.AddBooking(ClientPhone, HotelName, RoomName, StartDate, EndDate, Price);
                        Console.WriteLine($"Номер {RoomName} у готелі {HotelName} успішно заброньовано!\n");
                        break;
                    }
                case 21:
                    {
                        if (!TryGetClient(CManager, "Введіть номер телефону, на який робилось бронювання: ", "Клієнт не зареєстрований.", out string ClientPhone)) break;
                        List<Booking> bookings = BManager.FindBookingsByPhone(ClientPhone);
                        if (bookings.Count == 0)
                        {
                            Console.WriteLine("За цим номером не знайдено жодного бронювання.\n");
                            break;
                        }
                        Console.WriteLine("Виберіть бронювання зі списку для видалення:\n");
                        for (int i = 0; i < bookings.Count; ++i)
                        {
                            Console.WriteLine(@$"[{i+1}]
                            Готель: {bookings[i].BookedHotelName}
                            Номер: {bookings[i].BookedRoomName}
                            Вартість: {bookings[i].TotalPrice}
                            Період бронювання: {bookings[i].StartDate:dd.MM.yyyy} - {bookings[i].EndDate:dd.MM.yyyy}
                            ");
                        }
                        int option = GetInt();
                        if (option - 1 >= 0 && option - 1 < bookings.Count)
                        {
                            BManager.RemoveBooking(bookings[option - 1]);
                        }
                        else
                        {
                            Console.WriteLine("Неправильна операція.\n");
                        }
                        break;
                    }
                case 22:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        foreach (string RoomName in BManager.FindBookedRoomsInHotel(HotelName))
                        {
                            PrintRoom(HManager.FindHotelByName(HotelName).FindRoomByName(RoomName));
                        }
                        break;
                    }
                case 23:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: ", "Готель не знайдено.", out string HotelName)) break;
                        foreach (string RoomName in BManager.FindFreeRoomsInHotel(HotelName, HManager.FindHotelByName(HotelName).GetAllRoomsInHotel()))
                        {
                            PrintRoom(HManager.FindHotelByName(HotelName).FindRoomByName(RoomName));
                        }
                        break;
                    }
                case 24:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: \n", "Готель не знайдено.", out string HotelName)) break;
                        if (!TryGetRoom(HManager, HotelName, "Введіть точну назву номеру: \n", "Номер не знайдено.", out string RoomName)) break;
                        if (!TryGetBookingDates(out DateTime StartDate, out DateTime EndDate)) break;
                        Console.WriteLine($"Точна вартість бронювання складатиме {HManager.FindHotelByName(HotelName).CountTotalPrice(RoomName, StartDate, EndDate)}.\n");
                        break;
                    }
                case 25:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: \n", "Готель не знайдено.", out string HotelName)) break;
                        DateTime StartDate = GetDate("Введіть дату початку");
                        DateTime EndDate = GetDate("Введіть дату кінця");
                        if (StartDate.Date > EndDate.Date)
                        {
                            Console.WriteLine("Дата кінця повинна бути пізніше ніж дата початку.\n");
                            break;
                        }
                        foreach (Booking booking in BManager.FindBookingsInPeriod(HotelName, StartDate, EndDate))
                        {
                            Console.WriteLine($"Назва кімнати: {booking.BookedRoomName}\n");
                            Console.WriteLine($"Номер кліента: {booking.ClientPhone}\n");
                            Console.WriteLine($"Вартість: {booking.TotalPrice}\n");
                        }
                        break;
                    }
                case 26:
                    {
                        if (!TryGetHotel(HManager, "Введіть точну назву готелю: \n", "Готель не знайдено.", out string HotelName)) break;
                        Console.WriteLine("Список усіх людей, що забронювали номер у готелі:\n");
                        foreach (string phone in BManager.GetTenant(HotelName))
                        {
                            PrintClient(CManager.FindClientByPhone(phone));
                        }
                        break;
                    }
                default:
                    Console.WriteLine("Операція не знайдена!\n");
                    break;
            }
        } while (CurrentOperation != 0);
    }

    static string GetString(string prompt = null)
    {
        if (prompt != null) Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    static int GetInt(string prompt = null)
    {
        if (prompt != null) Console.WriteLine(prompt);
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Помилка! Введіть ціле число.");
            if (prompt != null) Console.WriteLine(prompt);
        }
        return result;
    }

    static decimal GetDecimal(string prompt = null)
    {
        if (prompt != null) Console.WriteLine(prompt);
        decimal result;
        while (!decimal.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Помилка! Введіть коректне числове значення.");
            if (prompt != null) Console.WriteLine(prompt);
        }
        return result;
    }

    static DateTime GetDate(string prompt)
    {
        string format = "dd.MM.yyyy";
        Console.WriteLine($"{prompt} у форматі {format}: ");
        DateTime date;
        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out date))
        {
            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
            Console.WriteLine($"{prompt} у форматі {format}: ");
        }
        return date;
    }

    static bool TryGetBookingDates(out DateTime start, out DateTime end)
    {
        start = GetDate("Введіть дату заїзду");
        if (start.Date < DateTime.Now.Date)
        {
            Console.WriteLine("Помилка: Не можна забронювати номер у минулому!");
            end = DateTime.MinValue;
            return false;
        }
        end = GetDate("Введіть дату виїзду");
        if (start.Date >= end.Date)
        {
            Console.WriteLine("Дата виїзду повинна бути пізніше ніж дата заїзду і не бути одним днем.\n");
            return false;
        }
        return true;
    }

    static bool TryGetHotel(HotelManager hm, string prompt, string errorMsg, out string name)
    {
        name = GetString(prompt);
        if (!hm.IsHotelExist(name))
        {
            Console.WriteLine(errorMsg);
            return false;
        }
        return true;
    }

    static bool TryGetRoom(HotelManager hm, string hotelName, string prompt, string errorMsg, out string roomName)
    {
        roomName = GetString(prompt);
        if (!hm.FindHotelByName(hotelName).IsRoomExist(roomName))
        {
            Console.WriteLine(errorMsg);
            return false;
        }
        return true;
    }

    static bool TryGetClient(ClientManager cm, string prompt, string errorMsg, out string phone)
    {
        phone = GetString(prompt);
        if (!cm.IsClientExist(phone))
        {
            Console.WriteLine(errorMsg);
            return false;
        }
        return true;
    }

    static void PrintHotel(Hotel hotel, bool includeRooms = false)
    {
        Console.WriteLine($"Назва: {hotel.Name}\n");
        if (includeRooms)
        {
            Console.WriteLine($"Кількість місць: {hotel.GetAllRoomsInHotel().Count}\n");
        }
        Console.WriteLine($"Опис: {hotel.Description}\n");
    }

    static void PrintRoom(Room room)
    {
        Console.WriteLine($"Назва: {room.Name}\n");
        Console.WriteLine($"Ціна за ніч: {room.PricePerNight}\n");
        Console.WriteLine($"Опис: {room.Description}\n");
    }

    static void PrintClient(Client client)
    {
        Console.WriteLine($"Телефон: {client.Phone}\n");
        Console.WriteLine($"Ім'я: {client.Name}\n");
        Console.WriteLine($"Прізвище: {client.Surname}\n");
    }
}