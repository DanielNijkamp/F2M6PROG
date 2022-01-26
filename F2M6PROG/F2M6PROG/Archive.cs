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
            Stopwatch stopwatch = new Stopwatch();
            int scp_series = 1;
            int start_counting_point = 0;
            int cycle_count = 0;
            

            for (int x = scp_series; scp_series < 8;)
            {
                stopwatch.Start();
                List<SCP> scp_series_list = new List<SCP>();
                switch (scp_series) 
                {
                    case 1:
                        cycle_count = 1000;
                        start_counting_point = 1;
                        break;
                    case 2:
                        cycle_count = 2000;
                        start_counting_point = 1000;
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
                for (int scp_count = start_counting_point; scp_count < cycle_count;)
                {
                    try
                    {
                        scp_series_list.Add(GenerateSCP(scp_count).Result);
                    }
                    catch(Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    //Console.WriteLine($"Series: {scp_series} Current:{start_counting_point}, count:{scp_count}");
                    


                    //start task with current scp count
                    //if all tasks are complete add to list in order
                    scp_count++;
                }
            SCP_Series.Add(scp_series_list); // add the local SCP list to SCP_SERIES so user can explore the individual series
            Console.WriteLine($"SCP_SERIES LIST COUNT IS: {SCP_Series.Count}"); 
            scp_series++;
            x++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Completed within: [{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}] Seconds");
        }

        public async Task<SCP> GenerateSCP(int scp_count)
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
                using (var client = new HttpClient(hch))
                {
                    try
                    {
                        var html = await client.GetStringAsync(link);
                        if (html != null)
                        {
                            doc.LoadHtml(html);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (HttpRequestException exc)
                    {
                        Console.WriteLine($"[{scp_link}]{exc.InnerException.Message}");
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
                string name = $"SCP-{scp_count.ToString().PadLeft(3, '0')}";
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
                SCP generated_scp = new SCP(name, security_level, objectclass, proc, desc);
                if (generated_scp != null)
                {
                Console.WriteLine($"Name: [{generated_scp.Name}] LV:[{generated_scp.AccessLevel}] Class: [{generated_scp.Objectclass}]");
                }
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
        
       
    

