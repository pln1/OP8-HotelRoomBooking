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
                        Console.WriteLine($"Готель {HotelName} успішно додано до бази.\n");
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
                        Console.WriteLine("Виберіть поле для внесення змін:\n[1] - Назва готелю\n[2] - Опис готелю\n");
                        int operation = int.Parse(Console.ReadLine());
                        if (operation == 1)
                        {
                            Console.WriteLine("Введіть нову назву готелю: ");
                            string NewName = Console.ReadLine();
                            if (HManager.IsHotelExist(NewName))
                            {
                                Console.WriteLine("Готель з такою назвою вже існує!\n");
                                break;
                            }
                            HManager.ChangeHotelName(HotelName, NewName);
                        }
                        else if (operation == 2)
                        {
                            Console.WriteLine("Введіть новий опис готелю: ");
                            string NewDescription = Console.ReadLine();
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
                            Console.WriteLine($"Кількість місць: {hotel.GetAllRoomsInHotel().Count}\n");
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
                        if (HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Такий номер вже існує.\n");
                            break;
                        }
                        Console.WriteLine("Введіть ціну номера за ніч: \n");
                        decimal RoomPrice = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Введіть опис номеру: \n");
                        string RoomDescription = Console.ReadLine();
                        HManager.FindHotelByName(HotelName).AddRoom(HotelName, RoomName, RoomPrice, RoomDescription);
                        Console.WriteLine($"Кімнату {RoomName} успішно додано до готелю {HotelName}.\n");
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
                        Console.WriteLine("Введіть назву номеру: ");
                        string RoomName = Console.ReadLine();
                        if (!HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Такий номер не існує.\n");
                            break;
                        }
                        HManager.FindHotelByName(HotelName).RemoveRoom(RoomName);
                        Console.WriteLine($"Номер {RoomName} успішно видалено.\n");
                        break;
                    }
                case 9:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Введіть назву номеру: ");
                        string RoomName = Console.ReadLine();
                        if (!HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Такий номер не існує.\n");
                            break;
                        }
                        Console.WriteLine("Виберіть поле для внесення змін:\n[1] - Назва номеру\n[2] - Ціна за добу\n[3] - Опис\n");
                        int operation = int.Parse(Console.ReadLine());
                        if (operation == 1)
                        {
                            Console.WriteLine("Введіть нову назву номеру: ");
                            string NewRoomName = Console.ReadLine();
                            if (HManager.FindHotelByName(HotelName).IsRoomExist(NewRoomName))
                            {
                                Console.WriteLine("Такий номер існує.\n");
                                break;
                            }
                            HManager.FindHotelByName(HotelName).ChangeRoomName(RoomName, NewRoomName);
                        }
                        else if (operation == 2)
                        {
                            Console.WriteLine("Введіть нову ціну номеру за добу: ");
                            decimal NewPrice = decimal.Parse(Console.ReadLine());
                            HManager.FindHotelByName(HotelName).ChangeRoomPrice(RoomName, NewPrice);
                        }
                        else if (operation == 3)
                        {
                            Console.WriteLine("Введіть новий опис номеру: ");
                            string NewDescription = Console.ReadLine();
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
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }

                        Console.WriteLine("Введіть точну назву номеру: ");
                        string RoomName = Console.ReadLine();
                        if (!HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Номер не знайдено.");
                            break;
                        }
                        Room room = HManager.FindHotelByName(HotelName).FindRoomByName(RoomName);
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
                        foreach (Room room in HManager.FindHotelByName(HotelName).GetAllRoomsInHotel())
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
                            Console.WriteLine("Такий клієнт вже зареєстрований.");
                            break;
                        }
                        Console.WriteLine("Введіть ім'я клієнта: ");
                        string ClientName = Console.ReadLine();
                        Console.WriteLine("Введіть прізвище клієнта: ");
                        string ClientSurname = Console.ReadLine();
                        CManager.AddClient(ClientPhone, ClientName, ClientSurname);
                        Console.WriteLine("Кліента успішно додано до бази.\n");
                        break;
                    }
                case 13:
                    {
                        Console.WriteLine("Введіть номер телефону: ");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Такий клієнт не зареєстрований.");
                            break;
                        }
                        CManager.RemoveClient(ClientPhone);
                        Console.WriteLine("Кліента успішно видалено.\n");
                        break;
                    }
                case 14:
                    {
                        Console.WriteLine("Введіть номер телефону: ");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт не зареєстрований.");
                            break;
                        }
                        Console.WriteLine("Виберіть поле для внесення змін:\n[1] - Номер телефону\n[2] - Ім'я\n[3] - Прізвище\n");
                        int operation = int.Parse(Console.ReadLine());
                        if (operation == 1)
                        {
                            Console.WriteLine("Введіть новий номер телефону: ");
                            string NewPhone = Console.ReadLine();
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
                            Console.WriteLine("Введіть нове ім'я: ");
                            string NewName = Console.ReadLine();
                            CManager.ChangeClientName(ClientPhone, NewName);
                        }
                        else if (operation == 3)
                        {
                            Console.WriteLine("Введіть нове прізвище: ");
                            string NewSurname = Console.ReadLine();
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
                        Console.WriteLine("Виберіть тип сортування:\n[1] - За зростанням\n[2] - За спаданням\n");
                        int ascdesc = int.Parse(Console.ReadLine());
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
                        Console.WriteLine("Виберіть тип сортування:\n[1] - За зростанням\n[2] - За спаданням\n");
                        int ascdesc = int.Parse(Console.ReadLine());
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
                        Console.WriteLine("Введіть номер телефону кліента: \n");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт не зареєстрований.");
                            break;
                        }
                        Console.WriteLine("Введіть точну назву готелю: \n");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Введіть точну назву номеру: \n");
                        string RoomName = Console.ReadLine();
                        if (!HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Номер не знайдено.");
                            break;
                        }
                        string format = "dd.MM.yyyy";
                        Console.WriteLine($"Введіть дату заїзду у форматі {format}: ");
                        DateTime StartDate;
                        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out StartDate))
                        {
                            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
                            Console.WriteLine($"Введіть дату заїзду у форматі {format}: ");
                        }
                        if (StartDate.Date < DateTime.Now.Date)
                        {
                            Console.WriteLine("Помилка: Не можна забронювати номер у минулому!");
                            break;
                        }
                        Console.WriteLine($"Введіть дату виїзду у форматі {format}: ");
                        DateTime EndDate;
                        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out EndDate))
                        {
                            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
                            Console.WriteLine($"Введіть дату виїзду у форматі {format}: ");
                        }
                        if (StartDate.Date >= EndDate.Date)
                        {
                            Console.WriteLine("Дата виїзду повинна бути пізніше ніж дата заїзду і не бути одним днем.\n");
                            break;
                        }
                        decimal Price = HManager.FindHotelByName(HotelName).CountTotalPrice(RoomName, StartDate, EndDate);
                        BManager.AddBooking(ClientPhone, HotelName, RoomName, StartDate, EndDate, Price);
                        Console.WriteLine($"Номер {RoomName} у готелі {HotelName} успішно заброньовано!\n");
                        break;
                    }
                case 21:
                    {
                        Console.WriteLine("Введіть номер телефону, на який робилось бронювання: ");
                        string ClientPhone = Console.ReadLine();
                        if (!CManager.IsClientExist(ClientPhone))
                        {
                            Console.WriteLine("Клієнт не зареєстрований.");
                            break;
                        }
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
                        int option = int.Parse(Console.ReadLine());
                        if (option-1 >= 0 && option-1 < bookings.Count)
                        {
                            BManager.RemoveBooking(bookings[option-1]);
                        }
                        else
                        {
                            Console.WriteLine("Неправильна операція.\n");
                        }
                        break;
                    }
                case 22:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        foreach (string RoomName in BManager.FindBookedRoomsInHotel(HotelName))
                        {
                            Room room = HManager.FindHotelByName(HotelName).FindRoomByName(RoomName);
                            Console.WriteLine($"Назва: {room.Name}\n");
                            Console.WriteLine($"Ціна за ніч: {room.PricePerNight}\n");
                            Console.WriteLine($"Опис: {room.Description}\n");
                        }
                        break;
                    }
                case 23:
                    {
                        Console.WriteLine("Введіть точну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        foreach (string RoomName in BManager.FindFreeRoomsInHotel(HotelName, HManager.FindHotelByName(HotelName).GetAllRoomsInHotel()))
                        {
                            Room room = HManager.FindHotelByName(HotelName).FindRoomByName(RoomName);
                            Console.WriteLine($"Назва: {room.Name}\n");
                            Console.WriteLine($"Ціна за ніч: {room.PricePerNight}\n");
                            Console.WriteLine($"Опис: {room.Description}\n");
                        }
                        break;
                    }
                case 24:
                    {
                        Console.WriteLine("Введіть точну назву готелю: \n");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Введіть точну назву номеру: \n");
                        string RoomName = Console.ReadLine();
                        if (!HManager.FindHotelByName(HotelName).IsRoomExist(RoomName))
                        {
                            Console.WriteLine("Номер не знайдено.");
                            break;
                        }
                        string format = "dd.MM.yyyy";
                        Console.WriteLine($"Введіть дату заїзду у форматі {format}: ");
                        DateTime StartDate;
                        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out StartDate))
                        {
                            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
                            Console.WriteLine($"Введіть дату заїзду у форматі {format}: ");
                        }
                        if (StartDate.Date < DateTime.Now.Date)
                        {
                            Console.WriteLine("Помилка: Не можна забронювати номер у минулому!");
                            break;
                        }
                        Console.WriteLine($"Введіть дату виїзду у форматі {format}: ");
                        DateTime EndDate;
                        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out EndDate))
                        {
                            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
                            Console.WriteLine($"Введіть дату виїзду у форматі {format}: ");
                        }
                        if (StartDate.Date >= EndDate.Date)
                        {
                            Console.WriteLine("Дата виїзду повинна бути пізніше ніж дата заїзду і не бути одним днем.\n");
                            break;
                        }
                        Console.WriteLine($"Точна вартість бронювання складатиме {HManager.FindHotelByName(HotelName).CountTotalPrice(RoomName, StartDate, EndDate)}.\n");
                        break;
                    }
                case 25:
                    {
                        Console.WriteLine("Введіть точну назву готелю: \n");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        string format = "dd.MM.yyyy";
                        Console.WriteLine($"Введіть дату початку у форматі {format}: ");
                        DateTime StartDate;
                        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out StartDate))
                        {
                            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
                            Console.WriteLine($"Введіть дату початку у форматі {format}: ");
                        }
                        Console.WriteLine($"Введіть дату кінця у форматі {format}: ");
                        DateTime EndDate;
                        while (!DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out EndDate))
                        {
                            Console.WriteLine($"Помилка! Введіть дату саме у форматі {format} (наприклад, 10.05.2026).\n");
                            Console.WriteLine($"Введіть дату кінця у форматі {format}: ");
                        }
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
                        Console.WriteLine("Введіть точну назву готелю: \n");
                        string HotelName = Console.ReadLine();
                        if (!HManager.IsHotelExist(HotelName))
                        {
                            Console.WriteLine("Готель не знайдено.");
                            break;
                        }
                        Console.WriteLine("Список усіх людей, що забронювали номер у готелі:\n");
                        foreach (string phone in BManager.GetTenant(HotelName))
                        {
                            Client client = CManager.FindClientByPhone(phone);
                            Console.WriteLine($"Телефон: {client.Phone}\n");
                            Console.WriteLine($"Ім'я: {client.Name}\n");
                            Console.WriteLine($"Прізвище: {client.Surname}\n");
                        }
                        break;
                    }
                default:
                    Console.WriteLine("Операція не знайдена!\n");
                    break;
            }
        } while (CurrentOperation != 0);
    }
}