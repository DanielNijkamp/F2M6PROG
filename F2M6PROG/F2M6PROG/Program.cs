using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace F2M6PROG
{
    class Program
    {
        static Archive SCP_Archive = new Archive();
        static void Main(string[] args)
        {
            bool GettingsCurrentUser = false;
            bool AppRunning = false;
            bool GettingPassword = false;
            bool GeneratingArchive = false;
            GettingsCurrentUser = true;

           
            Startup();
            DisplayFetchedUsers();
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

            } // gets users and asks which user to login to
            while (GettingPassword)
            {
                if (Login(Directory.currentuser))
                {
                    GettingPassword = false;
                    GeneratingArchive = true;
                    Console.Clear();
                }
            } // ask for password for the user 
            while (GeneratingArchive)
            {
                //if json doesnt exist generate new library
                if (!File.Exists(@"D:\MA\Projects\F2M6PROG\F2M6PROG\F2M6PROG\SCP_DATABASE.json"))
                {
                    Console.WriteLine("SCP_DATABASE.json does not exist would you like to generate it? [Y/N]");
                    Console.WriteLine("Note: Generating will take approx. 5 minutes and requires a internet connection");
                    int input = AskYesOrNo();
                    if (input == 1)
                    {
                        Console.Clear();
                        SCP_Archive.Fetch_SCP_Library();
                        GeneratingArchive = false;
                        AppRunning = true;

                    }
                    else if (input == 0)
                    {
                        Quit();
                    }

                }
            }
            while (AppRunning)
            {
                AskForSCP();
            } //actual app
        }
        public static void Help()
        {
            Console.WriteLine($"List of commands: Help, Quit, GetSCP");
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

        public static void AskForSCP()
        {
            Console.WriteLine($"Would you like to browse a SCP series or access a specific scp?{Environment.NewLine}Type 1 to access a series | Type 2 for a specific SCP");
            int choice = 0;

            bool choosing  = false;
            bool s2 = false;
            bool s3 = false;
            while (!choosing)
            {
                string input = Console.ReadLine();
                if (input.Contains("Quit") || input.Contains("quit"))
                {
                    Quit();
                }
                if (input.Contains("Help") || input.Contains("help"))
                {
                    Help();
                    choosing = true;
                }
                if (input == "1")
                {
                    Console.WriteLine("Select which SCP series you would like to access");
                    foreach(List<SCP> scp_list in SCP_Archive.SCP_Series)
                    {
                        Console.WriteLine($"SCP | Series-{SCP_Archive.SCP_Series.IndexOf(scp_list) +1}");
                    }
                    choosing = true;
                    s2 = true;
                }
                else if (input == "2")
                {
                    Console.WriteLine("Select which SCP file you would like to access");
                    choosing = true;
                    s2 = false;
                    s3 = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }

            //series
            while (s2)
            {
                bool q1 = false;
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice))
                {
                    try
                    {
                        Console.WriteLine($"Series-[{choice}] has [{SCP_Archive.SCP_Series[choice - 1].Count}] SCP files");
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Index was out of range, Please give a valid input");
                        break;
                    }
                    Console.WriteLine($"Select which scp you would like to access");
                    q1 = true;
                    while (q1)
                    {
                        string input2 = Console.ReadLine();
                        int choice2 = 0;
                        if (input2.Contains("Help") || input.Contains("help"))
                        {
                            Help();
                        }
                        else if (input2.Contains("Quit") || input.Contains("quit"))
                        {
                            Quit();
                        }
                        else if (input2.Contains("Exit") || input.Contains("exit"))
                        {
                            q1 = false;
                        }
                            if (int.TryParse(input2, out choice2))
                        {
                            try
                            {
                                DisplaySCP(choice-1, choice2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        
                    }
                }
                else if (input.Contains("Help") || input.Contains("help"))
                {
                    Help();
                }
                else if (input.Contains("Quit") || input.Contains("quit"))
                {
                    Quit();
                }
                else if (input.Contains("Exit") || input.Contains("exit"))
                {
                    choosing = false;
                    s2 = false;
                    s3 = false;
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            //specific scp
            while (s3)
            {
                int series = 0;
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice))
                {
                    if (choice > 0 && choice < 1000)
                    {
                        series = 0;
                    }
                    else if (choice > 1000 && choice < 2000)
                    {
                        series = 1;
                    }
                    else if (choice > 2000 && choice < 3000)
                    {
                        series = 2;
                    }
                    else if (choice > 3000 && choice < 4000)
                    {
                        series = 3;
                    }
                    else if (choice > 4000 && choice < 5000)
                    {
                        series = 4;
                    }
                    else if (choice > 5000 && choice < 6000)
                    {
                        series = 5;
                    }
                    else if (choice > 6000 && choice < 7000)
                    {
                        series = 6;
                    }
                    try
                    {
                        DisplaySCP(series, choice);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                if (input.Contains("Help") || input.Contains("help"))
                {
                    Help();
                }
                else if (input.Contains("Quit") || input.Contains("quit"))
                {
                    Quit();
                }
                else if (input.Contains("Exit") || input.Contains("exit"))
                {
                    choosing = false;
                    s2 = false;
                    s3 = false;
                    return;
                }
                
            }
            

                
            
        }
        public static int AskYesOrNo()
        {
            string console = Console.ReadLine();
            if (console == "Y" || console == "y" || console == "Yes" || console == "yes")
            {
                return 1;
            }
            else if (console == "N" || console == "No" || console == "n" || console == "no")
            {
                return 0;
            }
            else
            {
                Console.WriteLine("Invalid input: Option not available");
                return 0;
            }
            
        }
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
                    if (input <= maxNumber)
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

        public static void DisplaySCP(int series, int scp)
        {
            bool q1 = false;
            SCP current_scp = null;

            string name = $"SCP-{scp.ToString().PadLeft(3, '0')}";
            foreach (SCP item in SCP_Archive.SCP_Series[series])
            {
                if (item != null)
                {
                    if (item.Name == name)
                    {
                        current_scp = item;
                    }
                }
                  
            }
            if (current_scp != null)
            {
                if (current_scp.AccessLevel <= Directory.currentuser.SecurityClearance)
                {
                    Console.Clear();
                    Console.WriteLine($"SCP-{scp.ToString().PadLeft(3, '0')}{Environment.NewLine}[{current_scp.Objectclass}]");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine(current_scp.Description);
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine(current_scp.SCP_procedure);
                    Console.WriteLine("Would you like to exit? [Y/N]");
                    q1 = true;
                    while (q1)
                    {
                        string input = Console.ReadLine();
                        if (input.Contains("Yes") || input.Contains("yes") || input.Contains("y") || input.Contains("Y"))
                        {
                            q1 = false;
                            return;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("You do not have high enough clearance to view this file");
                }

            }
            else
            {
                Console.WriteLine("SCP file does not exist or script was not able to retrieve information about the SCP");
            }
            

        }
        
        public static void GetCurrentUser(int userint)
        {
            if (userint <= GetUsers().Count)
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

