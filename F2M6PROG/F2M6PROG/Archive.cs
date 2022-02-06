
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Threading;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;



namespace F2M6PROG
{
    class Archive
    {
        string source = "https://scp-wiki.wikidot.com";
        //permission level
        //Series 1 THROUGH 7
        //Class: safe, euclid, keter, thaumiel, neutralized, explained, apollyon
        //user access level
        //remove, add SCP's

        public List<List<SCP>> SCP_Series = new List<List<SCP>>();

        public void AddSCP(SCP scp)
        {

        }
        public void RemoveSCP(SCP scp)
        {

        }
        public void Get_Specific_SCP(int position)
        {
            
        }


        public void Fetch_SCP_Library()
        {
            Stopwatch stopwatch = new Stopwatch();
            int scp_series = 1;
            int start_counting_point = 0;
            int cycle_count = 0;

            stopwatch.Start();
            for (int x = scp_series; scp_series < 3;) // 8 
            {
                List<SCP> scp_series_list = new List<SCP>();
                switch (scp_series)
                {
                    case 1:
                        cycle_count = 1000; //1000
                        start_counting_point = 1;
                        break;
                    case 2:
                        cycle_count = 2000; // 2000
                        start_counting_point = 1000; //1000
                        break;
                    case 3:
                        cycle_count = 3000;
                        start_counting_point = 2000;
                        break;
                    case 4:
                        cycle_count = 4000;
                        start_counting_point = 3000;
                        break;
                    case 5:
                        cycle_count = 5000;
                        start_counting_point = 4000;
                        break;
                    case 6:
                        cycle_count = 6000;
                        start_counting_point = 5000;
                        break;
                    case 7:
                        cycle_count = 7000;
                        start_counting_point = 6000;
                        break;

                }// determines scp_series and how much to generate
                Dictionary<int, Task<SCP>> tasklist = new Dictionary<int, Task<SCP>>();
                for (int scp_count = start_counting_point; scp_count < cycle_count;)
                {
                    var task = GenerateSCP(scp_count);
                    tasklist.Add(scp_count, task);
                    Thread.Sleep(0030);
                    scp_count++;
                }
                Task.WaitAll(tasklist.Values.ToArray());
                foreach (KeyValuePair<int, Task<SCP>> entry in tasklist.OrderBy(start_counting_point => cycle_count))
                {
                        scp_series_list.Add(entry.Value.Result);
                        if (entry.Value.Result != null)
                        {
                        Console.WriteLine(entry.Value.Result.Name);
                        }
                    else
                    {
                        Console.WriteLine($"{tasklist.ToList().IndexOf(entry)} = null");
                    }
                        
                }
                SCP_Series.Add(scp_series_list); // add the local SCP list to SCP_SERIES so user can explore the individual series
                Console.WriteLine($"SCP_SERIES LIST COUNT IS: {SCP_Series.Count}");
                scp_series++;
                x++;
            }
            stopwatch.Stop();
            
            Console.WriteLine($"{Environment.NewLine}Completed within: [{stopwatch.Elapsed}] Seconds{Environment.NewLine}");
        }
        private async Task<SCP>GenerateSCP(int scp_count)
        {
                HtmlDocument doc = new HtmlDocument();
                int security_level = 0;
                Random rnd = new Random();
                string desc = null;
                string objectclass = null;
                string proc = null;

                //link
                string scp_link = $"{source}/scp-{scp_count.ToString().PadLeft(3, '0')}";
                Uri link = new Uri(scp_link, UriKind.Absolute);

                //http stuff
                
                HttpClientHandler hch = new HttpClientHandler();
                hch.Proxy = null;
                hch.UseProxy = false;
                hch.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                using (var client = new HttpClient(hch))
                {
                //set Accept headers
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept","text/html,application/xhtml+xml,application/xml,application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; EN; rv:11.0) like Gecko");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
                try
                    {
                        var response = await client.GetStringAsync(link); // get string asynchronously
                        if (response != null)
                        {
                            doc.LoadHtml(response);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine($"[{scp_link}]{exc.Message}");
                        return null;
                    }
                }
                HtmlNode page_content_node = doc.GetElementbyId("page-content"); // page content div
                foreach (HtmlNode node in page_content_node.SelectNodes("//p")) // loops through <p> divs that are children of page content div
                {
                    switch (node.InnerText)
                    {
                        case string a when a.Contains("Description"):
                        desc = node.InnerText;
                            break;
                        case string b when b.Contains("Special Containment Procedures"):
                        proc = node.InnerText;
                            break;
                        case string c when c.Contains("Object Class"):
                        objectclass = node.InnerText;
                            break;
                    }
                }
                string name = $"SCP-{scp_count.ToString().PadLeft(3, '0')}"; // sets a string to be the scp name

                switch (objectclass)
                {
                    case string a when a.Contains("Safe"):
                        security_level = rnd.Next(0, 1);
                        break;
                    case string b when b.Contains("Euclid"):
                        security_level = rnd.Next(1, 3);
                        break;
                    case string c when c.Contains("Keter"):
                        security_level = rnd.Next(2, 4);
                        break;
                    case string d when d.Contains("Thaumiel"):
                        security_level = rnd.Next(3, 5);
                        break;
                    case string e when e.Contains("Apollyon"):
                        security_level = rnd.Next(4, 5);
                        break;
                } // assigns random SCP access level based on object class
                if (desc != null && objectclass != null && proc != null)
                    {
                      SCP generated_scp = new SCP(name, security_level, objectclass, proc, desc);
                      Console.WriteLine($"Name: [{generated_scp.Name}] LV:[{generated_scp.AccessLevel}]");
                      return generated_scp;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error retrieving information about [SCP-{scp_count.ToString().PadLeft(3, '0')}]");
                    Console.ResetColor();
                    return null;
                }
                
            }

        public void ConvertToJson()
        {

        }
            
       
    }
}

          
    
    
                
            
            
                



        /*private List<SCP> SCPS = new List<SCP>();
        public void AddSCP(params SCP[] bookArray)
        {
            foreach (SCP book in bookArray)
            {
                 SCPS.Add(book);
            }
            return;
           
        }
        public SCP GetSCP(int pos)
        {
            return SCPS[pos];
            
        }*/
       
        
        //sort from A to Z
        //sort from objectclass
        //sort from number
        
       
    

