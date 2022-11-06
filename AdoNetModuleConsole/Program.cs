using AdoNetLib;
using System.Data;

namespace AdoNetModuleConsole
{
    internal class Program
    {
        static Manager manager = new Manager();

        static void Main(string[] args)
        {
            Console.WriteLine("Список команд для работы консоли:");
            Console.WriteLine(Commands.stop + ": прекращение работы");
            Console.WriteLine(Commands.add + ": добавление данных");
            Console.WriteLine(Commands.delete + ": удаление данных");
            Console.WriteLine(Commands.update + ": обновление данных");
            Console.WriteLine(Commands.show + ": просмотр данных\n");

            manager.Connect();

            string command;
            do
            {
                Console.WriteLine("\nВведите команду:");

                command = Console.ReadLine();

                Console.WriteLine();

                switch (command)
                {
                    case nameof(Commands.add): Add(); break;
                    case nameof(Commands.update): Update(); break;
                    case nameof(Commands.delete): Delete(); break;
                    case nameof(Commands.stop): manager.Disconnect(); break;
                    case nameof(Commands.show): manager.ShowDataUsers(); break;
                }

            }
            while (command != nameof(Commands.stop));
        }

        static void Add()
        {
            Console.WriteLine("Введите логин для добавления:");

            var login = Console.ReadLine();

            Console.WriteLine("Введите имя для добавления:");

            var name = Console.ReadLine();

            manager.AddUser(login, name);

            manager.ShowDataUsers();
        }

        static void Delete()
        {
            Console.WriteLine("Введите логин для удаления:");

            var count = manager.DeleteUserByLogin(Console.ReadLine());

            Console.WriteLine("Количество удаленных строк " + count);

            manager.ShowDataUsers();
        }

        static void Update()
        {
            Console.WriteLine("Введите логин для обновления:");

            var login = Console.ReadLine();

            Console.WriteLine("Введите имя для обновления:");

            var name = Console.ReadLine();

            var count = manager.UpdateUserByLogin(login, name);

            Console.WriteLine("Строк обновлено" + count);

            manager.ShowDataUsers();
        }
    }
}