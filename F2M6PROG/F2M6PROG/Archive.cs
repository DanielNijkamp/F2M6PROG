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


namespace F2M6PROG
{
    class Archive
    {

        //permission level
        //Series 1 THROUGH 7
        //Class: safe, euclid, keter, thaumiel, neutralized, explained, apollyon
        //user access level
        //remove, add SCP's

        List<List<SCP>> SCP_Series = new List<List<SCP>>();

        public void AddSCP(SCP scp)
        {

        }
        public void RemoveSCP(SCP scp)
        {

        }
        public void SortA_Z()
        {

        }
        public void Sort0_7()
        {

        }
        public void Get_Specific_SCP(int position)
        {





        }


        public void Fetch_SCP_Library()
        {
            string source = "https://scp-wiki.wikidot.com"; // url to website 
            int scp_series = 1;
            int current_scp_count = 0;
            int cycle_count = 0;
            WebClient wc = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            wc.Proxy = null;

            for (int x = scp_series; x < 8;)
            {
                List<SCP> scp_series_list = new List<SCP>();
                switch (scp_series) 
                {
                    case 1:
                        cycle_count = 1000;
                        current_scp_count = 1;
                        break;
                    case 2:
                        cycle_count = 2000;
                        current_scp_count = 1000;
                        break;
                    case 3:
                        cycle_count = 3000;
                        current_scp_count = 2000;
                        break;
                    case 4:
                        cycle_count = 4000;
                        current_scp_count = 3000;
                        break;
                    case 5:
                        cycle_count = 5000;
                        current_scp_count = 4000;
                        break;
                    case 6:
                        cycle_count = 6000;
                        current_scp_count = 5000;
                        break;
                    case 7:
                        cycle_count = 7000;
                        current_scp_count = 6000;
                        break;

                }// determines scp_series and how much to generate
                for (int scp_count = current_scp_count; scp_count < cycle_count;)
                {
                    var task = GenerateSCP(source,scp_count,doc);
                    try
                    {
                        if (task.Result != null)
                        {
                            Console.WriteLine($"test{scp_count}");
                            scp_series_list.Add(task.Result);
                        }
                        else
                        {
                            scp_series_list.Add(null);
                            Console.WriteLine($"DATA EXPUNGED [{scp_count.ToString().PadLeft(3, '0')} ]");
                        }
                    }
                    catch (AggregateException e) 
                    {
                        Console.WriteLine(e.Message);
                    }
                    

                    /*if (task.Result.Item1.Count == 2) // if values are not null: confirm that SCP class has been generated with values and add SCP to a list
                    {
                        Console.WriteLine($"Retrieved Object [SCP-{scp_count.ToString().PadLeft(3, '0')}");
                        scp_series_list.Add(task.Result.Item2);
                    }
                    else
                    {
                        Console.WriteLine("DATA EXPUNGED");
                        scp_series_list.Add(null);
                    }*/





                    scp_count++;
            }
            SCP_Series.Add(scp_series_list); // add the local SCP list to SCP_SERIES so user can explore the individual series
            Console.WriteLine(SCP_Series.Count); 
            scp_series++;
                    
                }

            }

        public async Task<SCP> GenerateSCP(string source, int scp_count, HtmlDocument doc)
        {
            int security_level = 0;
            Random rnd = new Random();
            List<string> scp_values = new List<string>();
            string scp_link = $"{source}/scp-{scp_count.ToString().PadLeft(3, '0')}";
            Uri link = new Uri(scp_link, UriKind.Absolute);
            using (var client = new HttpClient())
            {
                try
                {
                    var html = await client.GetStringAsync(link);
                    doc.LoadHtml(html);
                }
                catch(HttpRequestException)
                {
                    Console.WriteLine("HTTP ERROR");
                    return null;
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("NULL EXCEPTION ERROR");
                    return null;
                }
                
            }
            HtmlNode page_content_node =  doc.GetElementbyId("page-content"); // page content div
            foreach (HtmlNode node in page_content_node.SelectNodes("//p")) // loops through <p> divs that are children of page content div
            {
                switch (node.InnerText)
                {
                    case string a when a.Contains("Description"):
                        scp_values.Add(node.InnerText);
                        break;
                    case string b when b.Contains("Special Containment Procedures"):
                        scp_values.Add(node.InnerText);
                        break;
                    case string c when c.Contains("Object Class"):
                        scp_values.Add(node.InnerText);
                        break;
                }
            }
            string name = $"SCP-{scp_count.ToString().PadLeft(3, '0')}";
            switch (scp_values.ToArray()[2])
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
            SCP generated_scp = new SCP(name, security_level, scp_values.ToArray()[2], scp_values.ToArray()[1], scp_values.ToArray()[0]);
            return generated_scp;


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
        
       
    

