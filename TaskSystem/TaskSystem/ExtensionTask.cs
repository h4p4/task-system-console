using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TaskSystem
{
    internal static class ExtensionTask
    {
        internal static void ShowAvailableTasks(this List<Task> tasks, List<User> _users, List<Task> _tasks, User user)
        {
            List<Task> availableTasks = new List<Task>();
            Console.WriteLine("\nAll available tasks:\n");
            foreach (var task in tasks)
            {
                if (task.TaskStatus == "Не готово")
                {
                    availableTasks.Add(task);
                    TaskOutput(task);

                }
            }
            Console.WriteLine("\n1. Back to menu.\n2. Choose a task.\n");
            var choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.D1:
                    Program program = new Program();
                    program.Menu(_users, _tasks, user);
                    break;
                case ConsoleKey.D2:
                    tasks.StartTask(user, availableTasks, _users, _tasks);
                    break;
                default:
                    Console.WriteLine("No such task!");
                    tasks.ShowAvailableTasks(_users, _tasks, user);
                    break;
            }
        }
        internal static void StartTask(this List<Task> tasks, User user, List<Task> avaliableTasks, List<User> _users, List<Task> _tasks)
        {
            Console.WriteLine("\nTask number:");
            int taskNumber = Convert.ToInt32(Console.ReadLine());
            int check = 0;
            foreach (var task in avaliableTasks)
            {
                if (taskNumber == task.Id)
                {
                    task.AcceptedUser = user;
                    task.TaskStatus = "Выполняется";
                    check++;
                }
            }
            if (check > 0)
            {
                Console.WriteLine("You have chosen the task. Task's status was changed to \"Выполняется\".\n");
            }
            else
            {
                Console.WriteLine("Task with this number is not availiable.\n");
            }
            Program program = new Program();
            program.Menu(_users, _tasks, user);
        }
        internal static void ShowDoneTaskHistory(this List<Task> tasks, User user, List<User> _users, List<Task> _tasks)
        {
            int check = 0;
            var tasksDone = from task in tasks
                                      where task.AcceptedUser != null
                                      where task.AcceptedUser.Id == user.Id
                                      where task.TaskStatus == "Готово"
                                      select task;
            foreach (var task in tasksDone)
            {
                TaskOutput(task);
                check++;
            }
            if (check == 0)
            {
                Console.WriteLine("You do not have any tasks completed!.\n");
            }

            Program program = new Program();
            program.Menu(_users, _tasks, user);
        }
        internal static void ChangeTaskStatus(this List<Task> tasks, User user, List<User> _users, List<Task> _tasks)
        {
            int check = 0;
            Console.WriteLine("All your tasks: ");
            var tasksByAcceptedUser = from task in tasks
                                      where task.AcceptedUser != null
                                      where task.AcceptedUser.Id == user.Id
                                      select task;
            foreach (var task in tasksByAcceptedUser)
            {
                TaskOutput(task);
                check++;
            }
            if (check == 0)
            {
                Console.WriteLine("You do not have any tasks!\n");
            }
            else
            {
                Console.WriteLine("Enter task number:");
                var taskChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Chose task status:\n1. Не готово\n2. Выполняется\n3. Готово\n\nPress any other button to exit.\n");
                var choiceStatus = Console.ReadKey(true).Key;
                var found = _tasks.FindAll(x => x.Id == taskChoice);
                switch (choiceStatus)
                {
                    case ConsoleKey.D1:
                        TaskStatusLoop("Не готово", null, found);
                        break;
                    case ConsoleKey.D2:
                        TaskStatusLoop("Выполняется", user, found);
                        break;
                    case ConsoleKey.D3:
                        TaskStatusLoop("Готово", user, found);
                        break;
                    default:
                        tasks.ChangeTaskStatus(user, _users, _tasks);
                        break;
                }
            }
            Program program = new Program();
            program.Menu(_users, _tasks, user);
        }
        internal static void FilterTaskByDate(this List<Task> tasks, User user, List<User> _users, List<Task> _tasks)
        {
            Console.WriteLine("All tasks: ");
            var tasksOrderByDate = from task in tasks
                                   orderby task.PublicationDate
                                   select task;
            foreach (var task in tasksOrderByDate)
            {
                TaskOutput(task);
            }
            Program program = new Program();
            program.Menu(_users, _tasks, user);
        }
        internal static void FilterTaskByUser(this List<Task> tasks, User user, List<User> _users, List<Task> _tasks)
        {
            Console.WriteLine("Enter login for search:");
            string userLogin = Console.ReadLine();
            List<Task> tasksByUserCreator = tasks.FindAll(t => t.CreatorUser.Login == userLogin);
            Console.WriteLine("All tasks: ");
            if (tasksByUserCreator.Any())
            {
                foreach (var task in tasksByUserCreator)
                {
                    TaskOutput(task);
                }
            }
            else
            {
                Console.WriteLine("There are not any tasks created by this user.");
            }
            Program program = new Program();
            program.Menu(_users, _tasks, user);
        }


        private static void TaskStatusLoop(string taskStatus, User acceptedUser, List<Task> found)
        {
            foreach (var tsk in found)
            {
                if (found != null)
                {
                    tsk.TaskStatus = taskStatus;
                    tsk.AcceptedUser = acceptedUser;
                }
            }
        }
        private static void TaskOutput(Task task)
        {
                Console.WriteLine("Number: " + task.Id);
                Console.WriteLine("Name: " + task.Title);
                Console.WriteLine("Description: " + task.Description);
                Console.WriteLine("Publication date: " + task.PublicationDate.ToString("dd.MM.yyyy"));
                Console.WriteLine("User creator: " +
                    task.CreatorUser.Surname + " " +
                    task.CreatorUser.FirstName + " " +
                    task.CreatorUser.MiddleName);
            if (task.AcceptedUser !=null)
            {
                Console.WriteLine("User accepted: " +
                    task.AcceptedUser.Surname + " " +
                    task.AcceptedUser.FirstName + " " +
                    task.AcceptedUser.MiddleName);
            }
                Console.WriteLine("Status: " + task.TaskStatus + "\n");
        }
    }
}
