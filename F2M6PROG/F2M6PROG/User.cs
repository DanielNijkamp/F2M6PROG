using System;
using System.Collections.Generic;
using System.Text;

namespace F2M6PROG
{
    class User
    {
        public string Name;
        public int SecurityClearance;

        public User(string name, int level)
        {
            this.Name = name;
            this.SecurityClearance = level;
        }
    }
}
