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
        //TODO :https://www.w3.org/TR/webdriver/#keyboard-actions
        public static void SendTabKey(this IWebElement element)
        {
            element.SendKeys("\uE004");
        }

        public static void SendTabKey(this IWebElement element, int number)
        {
            for (int i = 0; i < number; i++)
            {
                element.SendTabKey();
            }
        }
        public static void DelayAction(this IWebElement element, int millisecondsDelay)
        {
            Task.Delay(millisecondsDelay).Wait();
        }
    }
}
