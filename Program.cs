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
        static void Main(string[] args)
        {
            ChromeDriver drv;
            ChromeOptions chromeCapabilities = new ChromeOptions();
            chromeCapabilities.EnableMobileEmulation("iPhone X");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            drv = new ChromeDriver(chromeCapabilities);

            drv.Navigate().GoToUrl("https://www.instagram.com/accounts/login/?next=https://www.instagram.com/hasancankayahck/");
            Thread.Sleep(3000);

            var x = drv.FindElements(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']"));
            x[0].SendKeys("username");
            x[1].SendKeys("password");
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
            var a = doc.DocumentNode.SelectNodes("//a[@class='-nal3 ']");
            var followsayi = a[1].FirstChild.InnerText.Replace(".", "");
            int takipedilensayi = Convert.ToInt32(followsayi);

            drv.FindElement(By.XPath("//a[@class=' _81NM2']")).Click();
            Thread.Sleep(3000);
            List<string> takipci = new List<string>();
            Actions action = new Actions(drv);


            string html4 = drv.PageSource;
            var doc4 = new HtmlDocument();
            doc4.LoadHtml(html4);
            Thread.Sleep(3000);
           
            int i = 0;
            do
            {
                //action.SendKeys(Keys.Space).Build().Perform();     bu ya da alttaki satır sayfayı aşağı indirmek için kullanılabilir.
                ((IJavaScriptExecutor)drv).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                Thread.Sleep(1500);
                i++;

            } while (i< 250);
            // i<250 deneme olarak koyuldu. takipcisyaisi/5 şeklinde düzenlenebilir.

            var followers3 = drv.FindElements(By.XPath("//a[@class='FPmhX notranslate  _0imsa ']"));
            foreach (var item in followers3)
            {
                takipci.Add(item.Text);
            }
            Console.ReadLine();
        }        
    }
}

