using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi
{
    public abstract class User
    {
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private string _DateRegistered = DateTime.Now.ToString();

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string DateRegistered { get => _DateRegistered; }
    }
}
