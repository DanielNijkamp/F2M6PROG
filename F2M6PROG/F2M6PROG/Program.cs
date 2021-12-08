using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace F2M6PROG
{
    /*while (AppRunning)
             
            {

            }*/

    class Program
    {
        
        static void Main(string[] args)
        {
            bool LoggingIn = false;
            bool AppRunning = false;

            LoggingIn = true;
            Console.WriteLine("Select which user you want to use");
            int i = 0;
            foreach (User user in LoadUsers())
            {
                Console.WriteLine($"User{i} Called: {user.Name}. Security Clearance is : {user.SecurityClearance}");
                i++;
            }
            while (LoggingIn)
            {

            }
            


        }
        public void Login()
        {

        }
        public static Dictionary<string,int> GetUsers()
        {
            string[] lines = File.ReadAllLines(@"D:\MA\Projects\F2M6PROG\F2M6PROG\F2M6PROG\Data.txt");
            Dictionary<string, int> Users = new Dictionary<string, int>();
            for (int count = 0; count <= lines.Length-3;)
            {
                Users.Add(lines[count], int.Parse(lines[count+1]));
                count += 3;
            }
            return Users;
        }
        public static List<User> LoadUsers()
        {
            List<User> UserList = new List<User>();
            foreach(KeyValuePair<string,int> user in GetUsers())
            {
                User newuser = new User(user.Key, user.Value);
                UserList.Add(newuser); 
            }
            return UserList;
        }
    }
}

