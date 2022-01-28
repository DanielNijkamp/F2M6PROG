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
            bool GettingsCurrentUser = false;
            bool AppRunning = false;
            bool GettingPassword = false;
            GettingsCurrentUser = true;
            Archive SCP_Archive = new Archive();
            Startup();
            DisplayFetchedUsers();
            Console.WriteLine($"Select which user you want to use {Environment.NewLine}");

            //if json doesnt exist generate new library
            SCP_Archive.Fetch_SCP_Library();



            while (GettingsCurrentUser)
            {
                GetCurrentUser(AskForInput());
                if (Directory.FetchedUser && Directory.currentuser != null)
                {
                    GettingsCurrentUser = false;
                    GettingPassword = true;
                    Console.WriteLine($"Enter password for user {Directory.currentuser.Name} {Environment.NewLine}");
                }

            } // gets users and asks which user to login to
            while (GettingPassword)
            {
                if (Login(Directory.currentuser))
                {
                    GettingPassword = false;
                    AppRunning = true;
                    
                }
            } // ask for password for the user 
            while (AppRunning)
            {
                
            } //actual app
        }
        public static bool Login(User user)
        {
            string input = Console.ReadLine();
            
            if (input == "Quit" || input == "quit" || input == "Exit" || input == "exit")
            {
                Quit();
                return false;
            }
            else
            {
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
        } //login user
        public static void Quit()
        {
            bool q1 = false;
            Console.WriteLine("Are you sure you want to quit? Y/N");
            while (!q1) 
            {
                string input = Console.ReadLine();
                if (input == "Yes" || input == "Y" || input == "y")
                {
                    Environment.Exit(1);
                }
                if (input == "No" || input == "N" || input == "n")
                {
                    q1 = true;
                    
                }
                else
                {
                    Console.WriteLine("Invalid input: Please give a valid input");
                }
            }
                

        } //ask to close application
        public static int AskForInput()
        {
            int maxNumber = GetUsers().Count;
            int input;
            string console = Console.ReadLine();
            if (console == "Quit" || console == "quit" || console == "Exit" || console == "exit")
            {
                Quit();
                return 0;
            }
            else
            {
                if (int.TryParse(console, out input))
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
        } // ask which user to use
        public static void GetCurrentUser(int userint)
        {
            if (userint < GetUsers().Count)
            {
                if (userint > 0)
                {
                    Directory.currentuser = GetUsers().Values.ElementAt(userint - 1);
                }

            }
        } // get user index in users dictionary
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
                i++;
                
            }
            return Users;
        } // get users from .JSON file and return dictionary
        public static void DisplayFetchedUsers()
        {
            int i = 0;
            foreach (KeyValuePair<int, User> user in GetUsers())
            {
                Console.WriteLine($"User [{user.Value.Name}] Found. Security Clearance is : [{user.Value.SecurityClearance}]");
                i++;
            }
        } // Display every user found in GetUsers()
        public static void Startup()
        {
            string scp_logo = @"
                                                
                /mhhhhhhhhhhhhm/                
            ```.ms            sd.```            
         ```:sdy+`    `mm`    `+yds-```         
       ```+ds:       `.NN``       :yd+```       
      ` /mo`    .+ydNNoMNoMNdy+.    `om/ `      
    ```hh.   `+dMMms+:-NN.:+smMMd+`   .hh```    
   ``.ms    /mMNo.    `NN`    .oNMm:    sd```   
   ``dy    sMMs`     :NMMN:     `yMMo    yd `   
  ` +m`   oMM+        /NN:        oMM+   `N+ `  
  ` mo   .NMh          //          hMN`   sd `  
  ``N/   /MM/                      +MM/   /N``  
  ``N/   /MM/  `-:://:    ://::-`  +MM/   /N``  
  -sd-   .NMh  .dMMMy`    `yMMMd.  hMN`   :ms.  
 .Ny      +hysmNhsN/        +NshNmsyh+      yN. 
  -m+   .+hNmyy.  `          `  .yymNy+.   om-  
   .ds  /ho-.mNNo.            -sNMm.-oh/  yd.   
    `hh`     `/dMMmy+:----/+ymMNd/`     `hh`    
     `yd.       .+ydNMMNNMMNdy+.       .dy`     
       om/+ss:       `.--.`      `:ss+/mo       
        /+:.-sdy+-`          `:+ydo-.:+/        
            ```.:oyhhhyyyyhhhyo:.```            
                ````````````````  
";
            string scp_text = @"
  ######  ###### #######       #### #######    ###### ###  ## #### ###  ## ######              
 ###  ## ###  ## ## #  ##     #####  ###  ##  ###  ## ###  ##  ### ###  ## ###  ##             
 ####    ###     # ##  ##    ## ###  ###  ##  ###     ###  ##  ### ###  ## ###                 
  #####  ###      ######    ##  ###  ######   ###     #######  ### ###  ## #####               
    #### ###      ###      ########  ### ##   ###     ###  ##  ### ###  ## ###                 
 ##  ### ###  ##  ###      ##   ###  ###  ##  ###  ## ###  ##  ###  #####  ###  ##             
  #####   #####   ###      ##   ###  ###  ##   #####  ###  ##  ###   ###   ######              
            ";
            Console.WriteLine(scp_logo);
            Console.WriteLine(scp_text);
            
        } //shows scp logo
    }
    static class Directory
    {
        
        public static bool FetchedUser = false;
        public static int InputUser;
        public static User currentuser;
    } //store important information
    class Root
    {
        [JsonProperty("Database_Users")]
        public static User[] DatabaseUsers { get; set; }
    } //json stuff
}

