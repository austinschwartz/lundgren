using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Lundgren.Ladder
{
    class Ladder : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly string _anthersUrl = "https://www.smashladder.com/log-in";
        private string _username;
        private string _password;

        public Ladder()
        {
            driver = new FirefoxDriver();
            _username = System.Environment.GetEnvironmentVariable("ladderUsername", EnvironmentVariableTarget.Machine);
            _password = System.Environment.GetEnvironmentVariable("ladderPassword", EnvironmentVariableTarget.Machine);
            Login();
        }

        public void Login()
        {
            driver.Navigate().GoToUrl(_anthersUrl);
            var userElement = driver.FindElement(By.Name("username"));
            userElement.SendKeys(_username);
            var passElement = driver.FindElement(By.Name("password"));
            passElement.SendKeys(_password);
            passElement.SendKeys(Keys.Enter);
        }

        public void Quit()
        {
            driver.Quit();
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}
