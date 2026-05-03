using OP8.Models;
using OP8.Services;
using OP8.Search;
using System.ComponentModel;

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
            [11]- Переглянути всі номери

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
                        Console.WriteLine("Введіть опис готелю: ");
                        string HotelDescription = Console.ReadLine();
                        HManager.AddHotel(HotelName, HotelDescription);
                        break;
                    }

                case 2:
                    {
                        Console.WriteLine("Введіть унікальну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        if (HManager.RemoveHotel(HotelName))
                        {
                            Console.WriteLine($"Готель {HotelName} успішно видалено.\n");
                        }
                        else
                        {
                            Console.WriteLine("Готель не знайдено / не вдалося видалити!\n");
                        }
                        break;
                    }

                case 3:
                    {
                        Console.WriteLine("Введіть унікальну назву готелю: ");
                        string HotelName = Console.ReadLine();
                        Console.WriteLine("Введіть нову назву готелю: ");
                        string NewName = Console.ReadLine();
                        Console.WriteLine("Введіть новий опис готелю: ");
                        string NewDescription = Console.ReadLine();
                        if (HManager.ChangeHotelData(HotelName, NewName, NewDescription))
                        {
                            Console.WriteLine($"Дані про готель успішно оновлено.\n");
                        }
                        else
                        {
                            Console.WriteLine("Не вдалося оновити дані!\n");
                        }
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                case 5:
                    {
                        break;
                    }
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
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
                    break;
                case 20:
                    break;

                default:
                    break;
            }
        } while (CurrentOperation != 0);
    }
}