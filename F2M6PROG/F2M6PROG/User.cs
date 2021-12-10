using System;
using System.Collections.Generic;
using System.Text;

namespace F2M6PROG
{
    class User
    {
        public string Name;
        public string password { private set; get; }
        public int SecurityClearance;

        public User(int level, string name ,string password)
        {
            this.Name = name;
            this.SecurityClearance = level;
            this.password = password;
        }
    }
}
