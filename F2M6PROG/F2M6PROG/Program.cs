using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace F2M6PROG
{
    /*while (AppRunning)
             
            {

            }*/

    class Program
    {

        static void Main(string[] args)
        {
            bool GettingsCurrentUser = false;
            bool AppRunning = false;
            bool GettingPassword = false;

            GettingsCurrentUser = true;
            Console.WriteLine($"Select which user you want to use {Environment.NewLine}");
            int i = 0;
            foreach (KeyValuePair<string, User> user in GetUsers())
            {
                Console.WriteLine($"User [{user.Value.Name}] Found. Security Clearance is : [{user.Value.SecurityClearance}]");
                i++;
            }
            while (GettingsCurrentUser)
            {
                GetCurrentUser(AskForInput());
                if (Directory.FetchedUser && Directory.currentuser != null)
                {
                    GettingsCurrentUser = false;
                    GettingPassword = true;
                    Console.WriteLine($"Enter password for user {Directory.currentuser.Name} {Environment.NewLine}");
                }

            }
            while (GettingPassword)
            {
                if (Login(Directory.currentuser))
                {
                    GettingPassword = false;
                    AppRunning = true;
                    
                }
            }
            while (AppRunning)
            {

            }



        }
        public static bool Login(User user)
        {
            string input = Console.ReadLine();
            if (input == user.password)
            {
                Console.WriteLine($"Succesfully logged in as {user.Name}");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect password");
                return false;
            }

        }
        public static int AskForInput()
        {
            int maxNumber = GetUsers().Count;
            int input;
            if (int.TryParse(Console.ReadLine(), out input))
            {
                if (input < maxNumber)
                {
                    Directory.FetchedUser = true;
                    Directory.InputUser = input;
                    return input;
                }
                Console.WriteLine("Invalid input: Option not available");
            }
            else
            {
                Console.WriteLine("Invalid input: Give a valid integer");
            }
            Directory.FetchedUser = false;
            return input;
        }




        public static void GetCurrentUser(int userint)
        {
            if (userint < GetUsers().Count)
            {
                if (userint > 0)
                {
                    Directory.currentuser = GetUsers().Values.ElementAt(userint - 1);
                }

            }
        }
        
        public static Dictionary<string, User> GetUsers()
        {
            string[] lines = File.ReadAllLines(@"D:\MA\Projects\F2M6PROG\F2M6PROG\F2M6PROG\Data.txt");
            Dictionary<string, User> Users = new Dictionary<string, User>();
            for (int count = 0; count <= lines.Length - 3;)
            {
                if (!Users.TryAdd(lines[count + 1], new User(int.Parse(lines[count]), lines[count + 1], lines[count + 2])))
                {
                    Console.WriteLine($"Error Retrieving user [{lines[count + 1]}]. Error: User already exists in archive {Environment.NewLine}");
                }
                count += 3;
            }
            return Users;
        }
        
    }
    static class Directory
    {
        public static bool FetchedUser = false;
        public static int InputUser;
        public static User currentuser;
    }
}

