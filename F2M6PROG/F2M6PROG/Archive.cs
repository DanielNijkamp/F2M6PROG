using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;


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
            string html;
            string source = "https://scp-wiki.wikidot.com"; // url to website 
            int scp_series = 1;
            int current_scp_count = 0;
            int cycle_count = 0;
            WebClient wc = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            
            for (int x = scp_series; x < 8;) 
            {
                List<SCP> scp_series_list = new List<SCP>();
                switch (scp_series) // determines scp_series and how much to generate
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

                }
                for (int scp_count = current_scp_count; scp_count < cycle_count;)
                {
                    
                    string desc = null;
                    string objectclass = null;
                    string scp_proc = null;

                    //variables set as null for now, values will be assigned later in the function

                    string name = $"SCP-{scp_count.ToString().PadLeft(3, '0')}";
                    html = wc.DownloadString($"{source}/scp-{scp_count.ToString().PadLeft(3, '0')}");
                    //Console.WriteLine($"{source}/scp-{scp_count.ToString().PadLeft(3, '0')}");
                    doc.LoadHtml(html);

                    HtmlNode page_content_node = doc.GetElementbyId("page-content"); // page content div

                    foreach (HtmlNode node in page_content_node.SelectNodes("//p")) // loops through <p> divs that are children of page content div
                    {
                        if (desc == null)
                        {
                            if (node.InnerText.Contains("Description"))
                            {
                                desc = node.InnerText;
                            }
                            else if (node.InnerText.Contains("Special Containment Procedures"))
                            {
                                scp_proc = node.InnerText;
                            }
                            else if (node.InnerText.Contains("Object Class"))
                            {
                                objectclass = node.InnerText;
                            }

                        }
                    }
                    SCP generated_scp = new SCP(name, 5, objectclass, scp_proc, desc);
                    if (desc != null && objectclass != null && scp_proc != null)
                    {
                        Console.WriteLine($"Retrieved Object [{generated_scp.Name}]");
                        scp_series_list.Add(generated_scp);
                    }
                    else
                    {
                        Console.WriteLine("DATA EXPUNGED");
                        scp_series_list.Add(null);
                    }
                    scp_count++;
                }
                SCP_Series.Add(scp_series_list);
                scp_series++;


            }
            
            
            
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
        
       
    

