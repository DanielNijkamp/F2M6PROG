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

        public void AddSCP()
        {

        }
        public void RemoveSCP()
        {

        }
        public void SortA_Z()
        {

        }
        public void Sort0_7()
        {

        }
        public void Get_Specific_SCP()
        {





        }
        
        
        public void Fetch_SCP_Library()
        {
            string html;
            string source = "https://scp-wiki.wikidot.com";
            int scp_series = 0;
            WebClient wc = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            //string content = "";
            /*HtmlDocument document = new HtmlDocument();
            foreach (HtmlNode paragraph in document.DocumentNode.SelectNodes("//p"))
            {
                content += paragraph.InnerText;
            }
            Console.WriteLine(content);*/
            for (int scp_count = 1; scp_count < 5;)
            {
                string desc = null;
                string objectclass = null;
                string name = null;
                string scp_proc = null;
                html = wc.DownloadString($"{source}/scp-{scp_count.ToString().PadLeft(3, '0')}");
                Console.WriteLine($"{source}/scp-{scp_count.ToString().PadLeft(3, '0')}");
                doc.LoadHtml(html);

                HtmlNode page_content_node = doc.GetElementbyId("page-content");

                foreach(HtmlNode node in page_content_node.SelectNodes("//p"))
                {
                    if (desc == null)
                    {
                        if (node.InnerText.Contains("Description"))
                        {
                            desc = node.InnerText;
                        }
                        else if (node.InnerText.Contains("Object Class"))
                        {
                            objectclass = node.InnerText;
                        }
                        else if (node.InnerText.Contains("Item"))
                        {
                            name = node.InnerText;
                        }
                        
                    }
                    
                    
                                        
                }
                if (desc != null)
                {
                    Console.WriteLine(desc);
                }
                if (objectclass != null)
                {
                    Console.WriteLine(objectclass);
                }

                scp_count++;
            }
            /*if (doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div[3]/p[6]/text()") != null)
            {
                desc = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div[3]/p[6]/text()").InnerText;
            }
            else
            {
                desc = "[ERROR]";
            }*/
            
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
        
       
    

