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
            //string content = "";
            /*HtmlDocument document = new HtmlDocument();
            foreach (HtmlNode paragraph in document.DocumentNode.SelectNodes("//p"))
            {
                content += paragraph.InnerText;
            }
            Console.WriteLine(content);*/
            WebClient wc = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            html = wc.DownloadString("https://scp-wiki.wikidot.com/scp-002");
            doc.LoadHtml(html);

            string desc = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div[3]/p[4]/text()").InnerText;

            Console.WriteLine(desc);

 

        }

                /*for (int scp_count = 0; scp_count < 1000;)
            {
                Console.WriteLine(scp_count.ToString().PadLeft(3,'0'));
                scp_count++;
            }*?
            
            
                
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
        
       
    }
}
