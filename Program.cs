using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Threading;
using OpenQA.Selenium.Edge;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

namespace InstagramCrawler
{
    class Program
    {
        public partial class Post
        {
            public string shortcode { get; set; }
            public string postContent { get; set; }
            public string __typename { get; set; }
            public string username { get; set; }
            public string engagementType { get; set; }
            public string date { get; set; }
            public string accountName { get; set; }
            public string comment { get; set; }


        }
        public partial class LikeComment
        {
            public int likes { get; set; }
            public int comments { get; set; }
        }

        static SqlConnection baglanti;
        static SqlCommand komut;
        static SqlDataReader reader;


        static void Main(string[] args)
        {
            ChromeDriver drv;

            ChromeOptions chromeCapabilities = new ChromeOptions();
            chromeCapabilities.EnableMobileEmulation("iPhone X");

            baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=LAPTOP-086R77C4;Initial Catalog=InstagramCrawl;Integrated Security=SSPI";


            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            drv = new ChromeDriver(chromeCapabilities);

            drv.Navigate().GoToUrl("https://www.instagram.com/accounts/login/?next=https://www.instagram.com/hasancankayahck/");
            Thread.Sleep(3000);

            var x = drv.FindElements(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']"));
            x[0].SendKeys("hesapdene541");
            x[1].SendKeys("753951ab");
            string girishtml = drv.PageSource;
            var giris = new HtmlDocument();
            giris.LoadHtml(girishtml);
            var b = giris.DocumentNode.SelectNodes("//button[@class='sqdOP  L3NKy   y3zKF     ']");
            drv.FindElement(By.XPath(b[1].XPath)).Click();
            Thread.Sleep(5000);
            drv.FindElement(By.XPath("//button[@class='sqdOP yWX7d    y3zKF     ']")).Click();



            string html = drv.PageSource;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var followersayisi = doc.DocumentNode.SelectSingleNode("//a[@class=' _81NM2']/span").Attributes["title"].Value.Replace(".", "");
            int takipcisayi = Convert.ToInt32(followersayisi);
            //var a = doc.DocumentNode.SelectNodes("//a[@class='-nal3 ']");
            //var followsayi = a[1].FirstChild.InnerText.Replace(".", "");
            //int takipedilensayi = Convert.ToInt32(followsayi);



            drv.FindElement(By.XPath("//a[@class=' _81NM2']")).Click();
            Thread.Sleep(3000);
            List<string> takipci = new List<string>();
            List<string> takipci1 = new List<string>();
            Actions action = new Actions(drv);

            //List<string> takipedilen = new List<string>();
            //List<string> takipedilen1 = new List<string>();


            string html4 = drv.PageSource;
            var doc4 = new HtmlDocument();
            doc4.LoadHtml(html4);
            Thread.Sleep(3000);
            //drv.FindElement(By.XPath("//div[@class='isgrP']")).Click();

            //for (int i = 0; i < takipcisayi / 5; i++)
            //{

            //    if (takipci.Count() == 1200)
            //    {
            //        break;
            //    }
            //    try
            //    {
            //        action.SendKeys(Keys.Space).Build().Perform();
            //        var followers3 = drv.FindElements(By.XPath("//a[@class='FPmhX notranslate  _0imsa ']"));

            //        Thread.Sleep(100);


            //        //string html6 = drv.PageSource;
            //        //var doc6 = new HtmlDocument();
            //        //doc6.LoadHtml(html6);
            //        //HtmlNodeCollection followers3 = doc6.DocumentNode.SelectNodes("//span[@class='Jv7Aj mArmR MqpiF  ']");
            //        foreach (var item in followers3)
            //        {
            //            takipci.Add(item.Text);
            //            takipci = takipci.Distinct().ToList();                        
            //        }


            //    }
            //    catch (Exception)
            //    {

            //    }


            //    //if (takipci.Count() == 0)
            //    //{

            //    //}
            //    //else
            //    //{

            //    //    foreach (var item in followers3)
            //    //    {
            //    //        int sayac = 0;

            //    //        foreach (var takip in takipci1)
            //    //        {
            //    //            if (takip == item.InnerText)
            //    //            {
            //    //                sayac++;

            //    //            }

            //    //        }
            //    //        if (sayac == 0)
            //    //        {
            //    //            takipci.Add(item.InnerText);
            //    //        }

            //    //    }
            //    //}

            //}
            int i = 0;
            do
            {

                //action.SendKeys(Keys.Space).Build().Perform();
                ((IJavaScriptExecutor)drv).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");


                Thread.Sleep(1500);
                i++;


            } while (i< 250);
            var followers3 = drv.FindElements(By.XPath("//a[@class='FPmhX notranslate  _0imsa ']"));




            foreach (var item in followers3)
            {
                takipci.Add(item.Text);
            }
            Console.ReadLine();

            //drv.FindElement(By.XPath("//div[@class='WaOAr']/button[@class='wpO6b ']")).Click();
            //drv.FindElement(By.XPath(a[1].XPath)).Click();
            //drv.FindElement(By.XPath("//div[@class='isgrP']")).Click();

            //for (int i = 0; i < takipedilensayi * 2; i++)
            //{
            //    if (takipedilen.Count() == takipedilensayi)
            //    {
            //        break;
            //    }

            //    Actions action = new Actions(drv);
            //    action.SendKeys(Keys.Space).Build().Perform();
            //    Thread.Sleep(3000);
            //    string html6 = drv.PageSource;
            //    var doc6 = new HtmlDocument();
            //    doc6.LoadHtml(html6);
            //    Thread.Sleep(5000);
            //    HtmlNodeCollection followers3 = doc6.DocumentNode.SelectNodes("//span[@class='Jv7Aj mArmR MqpiF  ']");
            //    if (takipedilen.Count() == 0)
            //    {
            //        foreach (var item in followers3)
            //        {
            //            takipedilen.Add(item.InnerText);
            //            takipedilen1 = takipedilen;

            //        }
            //    }
            //    else
            //    {

            //        foreach (var item in followers3)
            //        {
            //            int sayac = 0;

            //            foreach (var takip in takipedilen1)
            //            {
            //                if (takip == item.InnerText)
            //                {
            //                    sayac++;

            //                }

            //            }
            //            if (sayac == 0)
            //            {
            //                takipedilen.Add(item.InnerText);
            //            }

            //        }
            //    }

            //}

            //foreach (var item in takipedilen)
            //{
            //    int count = 0;

            //    foreach (var item2 in takipci)
            //    {
            //        if (item == item2)
            //        {
            //            count++;


            //        }


            //    }
            //    if (count == 0)
            //    {
            //        try
            //        {
            //            drv.Navigate().GoToUrl("https://www.instagram.com/" + item);
            //            Thread.Sleep(3000);
            //            drv.FindElement(By.XPath("//button[@class='_5f5mN    -fzfL     _6VtSN     yZn4P   ']")).Click();
            //            drv.FindElement(By.XPath("//button[@class='aOOlW -Cab_   ']")).Click();
            //            Thread.Sleep(3000);

            //        }
            //        catch (Exception)
            //        {

            //        }

            //    }


            //}


            //string gonderisayisi = drv.FindElement(By.XPath("//span[@class='g47SY ']")).Text.Trim();
            //int a = Convert.ToInt32(gonderisayisi);
            //for (int i = 0; i < a / 8; i++)
            //{
            //    string html = drv.PageSource;
            //    var doc = new HtmlDocument();
            //    doc.LoadHtml(html);

            //    HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("//div[@class='Nnq7C weEfm']");
            //    foreach (var item in htmlNodes)
            //    {
            //        HtmlNodeCollection htmlNodes2 = item.ChildNodes;
            //        foreach (var photo in htmlNodes2)
            //        {
            //            Post post = new Post();
            //            LikeComment counts = new LikeComment();
            //            string photolink = "www.instagram.com" + photo.FirstChild.Attributes["href"].Value;
            //            post.shortcode = photo.FirstChild.Attributes["href"].Value.Substring(photo.FirstChild.Attributes["href"].Value.IndexOf("p/") + 2).Replace("/", "");
            //            Thread.Sleep(5000);
            //            drv2 = new EdgeDriver();
            //            Thread.Sleep(5000);
            //            drv2.Navigate().GoToUrl(photolink);
            //            Thread.Sleep(5000);
            //            string html2 = drv2.PageSource;
            //            var doc2 = new HtmlDocument();
            //            doc2.LoadHtml(html2);
            //            int b = 1;



            //            var countlike = doc2.DocumentNode.SelectSingleNode("//div[@class='Nm9Fw']/button[@class='sqdOP yWX7d     _8A5w5    ']/span");
            //            if (countlike != null)
            //            {
            //                b = Convert.ToInt32(countlike.InnerText.Trim());
            //            }
            //            counts.likes = b;


            //            var date = doc2.DocumentNode.SelectSingleNode("//a[@class='c-Yi7']/time");
            //            post.date = date.Attributes["datetime"].Value;
            //            post.date = post.date.Substring(0, 10);


            //            var scripts = doc2.DocumentNode.SelectNodes("//script[@type='text/javascript']");
            //            string script = scripts[21].InnerHtml.ToString();
            //            string script2 = script.Substring(script.IndexOf(',') + 31);
            //            script2 = script2.Substring(0, script2.Length - 4);
            //            Post posts = JsonConvert.DeserializeObject<Post>(script2);
            //            dynamic array = JsonConvert.DeserializeObject(script2);
            //            post.shortcode = posts.shortcode;
            //            post.__typename = posts.__typename;
            //            post.accountName = "glo_azerbaijan";
            //            HtmlNode comments = doc2.DocumentNode.SelectSingleNode("//div[@class='EtaWk']/ul");
            //            int sayac = 0;
            //            foreach (var comment in comments.ChildNodes)
            //            {

            //                if (comment.Name == "ul")
            //                {
            //                    sayac++;
            //                    counts.comments = sayac;
            //                    post.username = comment.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.InnerText;
            //                    post.comment = comment.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].ChildNodes[1].InnerText;
            //                    post.engagementType = "comment"; komut = new SqlCommand();
            //                    komut.Connection = baglanti;
            //                    komut.CommandText = "INSERT INTO AccountEngagementCrawler (postID,postType,username,engagementType,accountName,comment,date) VALUES ('" + post.shortcode + "','" + post.__typename + "','" + post.username + "','" + post.engagementType + "','" + post.accountName + "',N'" + post.comment + "','" + post.date + "')";
            //                    baglanti.Open();
            //                    reader = komut.ExecuteReader();
            //                    baglanti.Close();

            //                }
            //            }


            //            komut.Connection = baglanti;
            //            komut.CommandText = "INSERT INTO AccountSummaryEngagement (postID,postType,countofLike,AccountName,countofComment,publishDate) VALUES ('" + post.shortcode + "','" + post.__typename + "','" + counts.likes + "','" + post.accountName + "','" + counts.comments + "','" + post.date + "')";
            //            baglanti.Open();
            //            reader = komut.ExecuteReader();
            //            baglanti.Close();

            //            try
            //            {
            //                drv2.FindElement(By.XPath("//div[@class='Nm9Fw']/button[@class='sqdOP yWX7d     _8A5w5    ']")).Click();
            //                Thread.Sleep(3000);
            //                string html3 = drv2.PageSource;
            //                var doc3 = new HtmlDocument();
            //                doc3.LoadHtml(html3);
            //                Thread.Sleep(3000);




            //                HtmlNode htmlNodes3 = doc3.DocumentNode.SelectSingleNode("//div[@class='pbNvD  fPMEg    ']");
            //                var sayi = htmlNodes3.FirstChild.ChildNodes[1].FirstChild.FirstChild.ChildNodes.Count();
            //                var actions = new Actions(drv2);
            //                drv2.FindElement(By.XPath("//div[@class='pbNvD  fPMEg    ']/div/div[@class='                    Igw0E     IwRSH      eGOV_        vwCYk                                                                            i0EQd                                   ']/div")).Click();


            //                while (sayi < b)
            //                {
            //                    sayi = sayi + 1;
            //                    //htmlNodes3.FirstChild.ChildNodes[1].FirstChild.FirstChild.ChildNodes.Count();
            //                    string html4 = drv2.PageSource;
            //                    var doc4 = new HtmlDocument();
            //                    doc4.LoadHtml(html4);

            //                    HtmlNode htmlNodes5 = doc4.DocumentNode.SelectSingleNode("//div[@class='pbNvD  fPMEg    ']");
            //                    HtmlNodeCollection htmlNodes4 = htmlNodes5.FirstChild.ChildNodes[1].FirstChild.FirstChild.ChildNodes;



            //                    foreach (var item2 in htmlNodes4)
            //                    {
            //                        post.username = item2.ChildNodes[1].FirstChild.InnerText;
            //                        post.engagementType = "like";
            //                        post.comment = null;
            //                        komut = new SqlCommand();
            //                        komut.Connection = baglanti;
            //                        komut.CommandText = "select username from AccountEngagementCrawler";
            //                        baglanti.Open();
            //                        reader = komut.ExecuteReader();
            //                        List<string> users = new List<string>();
            //                        int count = 0;


            //                        while (reader.Read())
            //                        {
            //                            users.Add(reader[0].ToString());

            //                        }
            //                        baglanti.Close();

            //                        if (users.Count() == 0)
            //                        {
            //                            komut.CommandText = "INSERT INTO AccountEngagementCrawler (postID,postType,username,engagementType,accountName,comment,date) VALUES ('" + post.shortcode + "','" + post.__typename + "','" + post.username + "','" + post.engagementType + "','" + post.accountName + "','" + post.comment + "','" + post.date + "')";
            //                            baglanti.Open();
            //                            reader = komut.ExecuteReader();
            //                            baglanti.Close();
            //                        }
            //                        else
            //                        {
            //                            foreach (var user in users)
            //                            {
            //                                if (user == post.username)
            //                                {
            //                                    count++;
            //                                }
            //                            }
            //                            if (count == 0)
            //                            {

            //                                komut.CommandText = "INSERT INTO AccountEngagementCrawler (postID,postType,username,engagementType,accountName,comment,date) VALUES ('" + post.shortcode + "','" + post.__typename + "','" + post.username + "','" + post.engagementType + "','" + post.accountName + "','" + post.comment + "','" + post.date + "')";
            //                                baglanti.Open();
            //                                reader = komut.ExecuteReader();
            //                                baglanti.Close();
            //                            }
            //                        }

            //                    }
            //                    Actions action = new Actions(drv2);
            //                    action.SendKeys(Keys.Space).Build().Perform();

            //                }

            //            }
            //            catch (Exception)
            //            {
            //            }



            //        }
            //    }
            //    Thread.Sleep(3000);
            //}








        }









    }
}

