using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSystem
{
    internal static class ExtensionUser
    {
        internal static void SignUp(this List<User> users, List<Task> _tasks, int usersCount)
        {
            Console.WriteLine("Enter login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter surname: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter middle name: ");
            string middleName = Console.ReadLine();
            Console.WriteLine("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
            var us = users.FirstOrDefault(x => x.Login == login);
            if (!string.IsNullOrWhiteSpace(login) ||
                !string.IsNullOrWhiteSpace(password) ||
                !string.IsNullOrWhiteSpace(name) ||
                !string.IsNullOrWhiteSpace(surname) ||
                !string.IsNullOrWhiteSpace(middleName) ||
                !string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (us == null)
                {
                    users.Add(new User(usersCount, name, surname, middleName, login, password, phoneNumber));
                }
                else if (us != null)
                {
                    Console.WriteLine("User already exists.");
                }
            } else Console.WriteLine("You must fill all fields!");
            
            Program program = new Program();
            program.LogAndReg(users, _tasks);
        }
        internal static void LogIn(this List<User> users, List<User> _users, List<Task> _tasks, out User user)
        {
            Console.WriteLine("Enter login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            var foundUser = users.FindAll(s => s.Login == login && s.Password == password);
            var count = foundUser.Count();
            if (count != 0)
            {
                user = foundUser[0];
                Console.WriteLine(foundUser.FirstOrDefault().FirstName + " " + foundUser.FirstOrDefault().MiddleName + ", you successfully loged in.");
            }
            else
            {
                user = null;
                Console.WriteLine("Wrong login or password.");
                Program program = new Program();
                program.LogAndReg(_users, _tasks);
            }
        }
        internal static void ShowLogins(this List<User> users, List<User> _users, List<Task> _tasks, User user)
        {
            foreach (var u in users)
            {
                if (u.Login != null && u.FirstName != null)
                {
                    Console.WriteLine("User: " + u.FirstName + "\nLogin: " + u.Login + "\n");
                }
            }
            Program program = new Program();
            program.Menu(_users, _tasks, user);
        }
        internal static void ShowProfile(this List<User> users, List<User> _users, List<Task> _tasks, User user)
        {
            foreach (var u in users.Where(x => x.Login == user.Login))
            {
                if (
                u.Login != null &&
                u.FirstName != null &&
                u.Surname != null &&
                u.MiddleName != null &&
                u.PhoneNumber != null
                )
                {
                    Console.WriteLine(
                        "User Id: #" + u.Id +
                        "\nLogin: " + u.Login +
                        "\nName: " + u.FirstName +
                        "\nSurname: " + u.Surname +
                        "\nMiddle Name: " + u.MiddleName +
                        "\nPhone Number: " + u.PhoneNumber +
                        "\n"
                        );
                    Program program = new Program();
                    program.Menu(_users, _tasks, user);
                }
                else Console.WriteLine("Your data is not filled completely");
            }
        }
    }
}
