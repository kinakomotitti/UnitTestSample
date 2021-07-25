using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSample.Exension
{
    public static class SeleniumDriverExtension
    {
        public static void SendTabKey(this IWebElement element)
        {
            element.SendKeys("\uE004");
        }
        public static void DelayAction(this IWebElement element, int millisecondsDelay)
        {
            Task.Delay(millisecondsDelay).Wait();
        }
    }
}
