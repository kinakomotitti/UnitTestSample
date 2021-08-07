using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumSample.Exension;
using SeleniumSample.Interfaces;
using System;

namespace SeleniumSample.Services
{
    public class AngularMaterialDatePicker : IAngularMaterialDatePicker
    {
        public void SetDatePicker(DateTime targetDateTime)
        {
            using (var driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl("https://material.angular.io/components/datepicker/overview");

                var targetPath = "//*[@id=\"datepicker-overview\"]/div/div[2]/datepicker-overview-example/mat-form-field/div/div[1]/div[2]/mat-datepicker-toggle/button";
                var picker = driver.FindElement(By.XPath(targetPath));
                picker.Click();

                var delta = (targetDateTime - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).Days;
                if (delta > 0) 
                    this.SendArrowRightKey(driver, delta);
                else if (delta < 0) 
                    this.SendArrowLeftKey(driver, Math.Abs(delta));

                driver.SwitchTo().ActiveElement().Click();
                driver.Close();
            }
        }

        public void SendArrowLeftKey(ChromeDriver driver, int delta)
        {
            for (int i = 0; i < delta; i++)
            {
                driver.SwitchTo().ActiveElement().SendArrowLeftKey();
            }
        }

        public void SendArrowRightKey(ChromeDriver driver, int delta)
        {
            for (int i = 0; i < delta; i++)
            {
                driver.SwitchTo().ActiveElement().SendArrowRightKey();
            }
        }
    }
}
