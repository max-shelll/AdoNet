using AdoNetLib;
using System.Data;

namespace AdoNetModuleConsole
{
    public class Manager
    {
        private MainConnector connector;
        private DbExecutor dbExecutor;
        private Table userTable;

        public Manager()
        {
            connector = new MainConnector();

            userTable = new Table();
            userTable.Name = "NetworkUser";
            userTable.ImportantField = "Login";
            userTable.Fields.Add("Id");
            userTable.Fields.Add("Login");
            userTable.Fields.Add("Name");
        }

        public void Connect()
        {
            var result = connector.ConnectAsync();

            if (result.Result)
            {
                Console.WriteLine("Подключено успешно!");

                dbExecutor = new DbExecutor(connector);
            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Отключаем БД!");
            connector.DisconnectAsync();
        }

        public void ShowDataUsers()
        {
            var reader = dbExecutor.SelectAllFromTable(userTable.Name);

            if (reader != null)
            {
                var columnList = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    columnList.Add(name);
                }

                for (int i = 0; i < columnList.Count; i++)
                {
                    Console.Write($"{columnList[i]}\t");
                }
                Console.WriteLine();

                while (reader.Read())
                {
                    for (int i = 0; i < columnList.Count; i++)
                    {
                        var value = reader[columnList[i]];
                        Console.Write($"{value}\t");
                    }
                    Console.WriteLine();
                }
            }
        }

        public int DeleteUserByLogin(string value)
        {
            return dbExecutor.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
        }

        public void AddUser(string login, string name)
        {
            dbExecutor.ExecProcedureAdding(name, login);
        }

        public int UpdateUserByLogin(string value, string newvalue)
        {
            return dbExecutor.UpdateByColumn(userTable.Name, userTable.ImportantField, value, userTable.Fields[2], newvalue);
        }
    }
}
