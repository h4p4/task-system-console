using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSystem
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public User(
         int Id,
         string FirstName,
         string Surname,
         string MiddleName,
         string Login,
         string Password,
         string PhoneNumber
        )
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.MiddleName = MiddleName;
            this.Login = Login;
            this.Password = Password;
            this.PhoneNumber = PhoneNumber;
        }
        public User()
        {
        }
    }
}
