using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TaskSystem
{
    internal class Program
    {
        private int userId;
        private User user;
        internal void Menu(List<User> users, List<Task> tasks, User user)
        {
            Console.WriteLine(
                "\n1. View my profile.\n" +
                "2. Show all logins.\n" +
                "3. Show available tasks.\n" + 
                "4. View my finished tasks.\n" +
                "5. Change task's status.\n" +
                "6. Filter tasks by date.\n" +
                "7. Search tasks by login.\n" +
                "\nPress any other button to log out.");
            var choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    users.ShowProfile(users, tasks, user);
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    users.ShowLogins(users, tasks, user);
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    tasks.ShowAvailableTasks(users, tasks, user);
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    tasks.ShowDoneTaskHistory(user, users, tasks);
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    tasks.ChangeTaskStatus(user, users, tasks);
                    break;
                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    tasks.FilterTaskByDate(user, users, tasks);
                    break;
                case ConsoleKey.NumPad7:
                case ConsoleKey.D7:
                    tasks.FilterTaskByUser(user, users, tasks);
                    break;
                default:
                    LogAndReg(users, tasks);
                    break;
            }
        }
        internal void LogAndReg(List<User> users, List<Task> tasks)
        {
            Program program = new Program();
            Console.WriteLine("1. Log In\n2. Sign Up\n\nPress any other button to exit.");
            var choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.D1:

                    users.LogIn(users, tasks, out user);
                    if (user != null)
                    {
                        userId = (users.FindIndex(x => x.Login == user.Login));
                        user = users[userId];
                        program.Menu(users, tasks, user);
                    }
                    break;
                case ConsoleKey.D2:
                    users.SignUp(tasks, users.Count);
                    break;
                default:
                    ConsoleKey choiceExit;
                        Console.WriteLine("1. Save and exit\n2. Exit without saving.");
                    do
                    {
                        choiceExit = Console.ReadKey(true).Key;
                        switch (choiceExit)
                        {
                            case ConsoleKey.D1:
                                SerializeAndSave(tasks, users);
                                return;
                            case ConsoleKey.D2:
                                return;
                        }
                    } while (choiceExit != ConsoleKey.D1 || choiceExit != ConsoleKey.D2);
                    break;
            }
        }
        public static void Main(string[] args)
        {
            Program program = new Program();
            List<Task> tasks;
            List<User> users;
            //List<User> users = new List<User>();
            //List<Task> tasks = new List<Task>();
            //users.Add(new User(0, "Albert", "Zakourtsev", "Nikolaevich", "qwe", "123", "89138150439"));
            //users.Add(new User(1, "Lexa", "Vitaliev", "Fedorovich", "ewq", "321", "89888132439"));
            //users.Add(new User(2, "Mixa", "Mihailov", "Evgenievich", "qweqwe", "123321", "89138123424"));
            //users.Add(new User(3, "Sanya", "Olegov", "Vitalievich", "EwQ", "122", "89158237313"));
            //users.Add(new User(4, "Valeriy", "Vasiliev", "Alexandrovich", "qqqwwweee", "111222333", "89995220201"));
            //tasks.Add(new Task(0, "Уборка", "Влажная уборка", new DateTime(2022, 1, 1), users[2], null, "Не готово"));
            //tasks.Add(new Task(1, "Уборка", "Влажная уборка", new DateTime(2022, 1, 2), users[3], null, "Не готово"));
            //tasks.Add(new Task(2, "Уборка", "Сухая уборка", new DateTime(2022, 1, 3), users[3], users[2], "Выполняется"));
            //tasks.Add(new Task(3, "Уборка", "Пылесосание", new DateTime(2022, 1, 4), users[0], users[1], "Готово"));
            //tasks.Add(new Task(4, "Уборка", "Еще пылесосание", new DateTime(2022, 1, 5), users[2], users[4], "Готово"));
            //tasks.Add(new Task(5, "Уборка", "Еще немного пылесосания", new DateTime(2022, 1, 6), users[0], users[4], "Готово"));
            ReadAndDeserialize(out tasks, out users);
            program.LogAndReg(users, tasks);
        }
        public static void ReadAndDeserialize(out List<Task> tasks, out List<User> users)
        {
            string taskPath = @".\data\taskList.xml";
            string userPath = @".\data\userList.xml";
            var serializer = new XmlSerializer(typeof(List<Task>));
            using (var reader = new StreamReader(taskPath))
            {
                tasks = (List<Task>)serializer.Deserialize(reader);
            }
            serializer = new XmlSerializer(typeof(List<User>));
            using (var reader = new StreamReader(userPath))
            {
                users = (List<User>)serializer.Deserialize(reader);
            }
        }
        public static void SerializeAndSave(List<Task> tasks, List<User> users)
        {
            string taskPath = @".\data\taskList.xml";
            string userPath = @".\data\userList.xml";
            var serializer = new XmlSerializer(typeof(List<Task>));
            using (var writer = new StreamWriter(taskPath))
            {
                serializer.Serialize(writer, tasks);
            }


            serializer = new XmlSerializer(typeof(List<User>));
            using (var writer = new StreamWriter(userPath))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}
