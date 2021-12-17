using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace F2M6PROG
{
    class Program
    {

        static void Main(string[] args)
        {
            /*bool GettingsCurrentUser = false;
            bool AppRunning = false;
            bool GettingPassword = false;
            GettingsCurrentUser = true;

            Archive SCP_Archive = new Archive(); // create archive
            DisplayFetchedUsers(); // displays users found in data,txt
            SCP scp002 = new SCP002("SCP002",3, SCP.ObjectClass.Keter);
            Console.WriteLine($"Select which user you want to use {Environment.NewLine}");
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
                

            }*/

            GetUsers();


        }
        public static bool Login(User user)
        {
            string input = Console.ReadLine();
            if (input == user.Password)
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







        public static Dictionary<int, User> GetUsers()
        {
            Dictionary<int, User> Users = new Dictionary<int, User>();
            
            string filename = @"D:\MA\Projects\F2M6PROG\F2M6PROG\F2M6PROG\Data.json";
            string jsonstring = File.ReadAllText(filename);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonstring);
            int i = 0;
            foreach(User user in Root.DatabaseUsers)
            {
                if (!Users.TryAdd(i, user))
                {
                    Console.WriteLine($"Error Retrieving user [{user.Name}]. Error: User already exists in archive {Environment.NewLine}");
                }
                Console.WriteLine($" Index of: [{i}] Name: [{user.Name}], Level: [{user.SecurityClearance}], Password: [{user.Password}]");
                i++;
                
            }
            return Users;
        }







        public static void DisplayFetchedUsers()
        {
            int i = 0;
            foreach (KeyValuePair<int, User> user in GetUsers())
            {
                Console.WriteLine($"User [{user.Value.Name}] Found. Security Clearance is : [{user.Value.SecurityClearance}]");
                i++;
            }
        }
    }
    static class Directory
    {
        
        public static bool FetchedUser = false;
        public static int InputUser;
        public static User currentuser;
    }
    class Root
    {
        [JsonProperty("Database_Users")]
        public static User[] DatabaseUsers { get; set; }
    }
}

