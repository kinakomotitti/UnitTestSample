using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumSample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumSample.Exension;

namespace SeleniumSample.Services
{
    public class YahooTransitService : IYahooTransitService
    {
        public string GetFareOfRoute1(string from, string to)
        {
            using (var driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl("https://transit.yahoo.co.jp/");

                var sFrom = driver.FindElement(By.Id("sfrom"));
                sFrom.Clear();
                sFrom.SendKeys(from);
                sFrom.SendTabKey();
                
                var sTo = driver.SwitchTo().ActiveElement();
                sTo.SendKeys(to);

                var searchModule = driver.FindElement(By.Id("searchModuleSubmit"));
                searchModule.Click();
                searchModule.DelayAction(1000);

                var fare = driver.FindElement(By.XPath("//*[@id=\"rsltlst\"]/li[1]/dl/dd/ul/li[2]")).Text;
                return fare;
            }
        }
    }
}