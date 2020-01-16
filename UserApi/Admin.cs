using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi
{
    public class Admin : User
    {
        private static int _AdminCount = 0;
        public Admin(string name, string email, string password, int level)
        {
            Name = name;
            Email = email;
            Password = password;
            Id = ++AdminCount;
            Level = level;
        }

        public static int AdminCount { get => _AdminCount; set => _AdminCount = value; }
        public int Level { get; set; }

        public void ManageAdmin(int Level)
        {
            this.Level = Level;
        }
    }
}
