using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Lundgren.Ladder
{
    class Ladder
    {
        IWebDriver driver;
        String anthersURL = "https://www.smashladder.com/log-in";
        String username;
        String password;

        public Ladder()
        {
            driver = new FirefoxDriver();
            username = System.Environment.GetEnvironmentVariable("ladderUsername", EnvironmentVariableTarget.Machine);
            password = System.Environment.GetEnvironmentVariable("ladderPassword", EnvironmentVariableTarget.Machine);
            login();
        }

        public void login()
        {
            driver.Navigate().GoToUrl(anthersURL);
            IWebElement userElement = driver.FindElement(By.Name("username"));
            userElement.SendKeys(username);
            IWebElement passElement = driver.FindElement(By.Name("password"));
            passElement.SendKeys(password);
            passElement.SendKeys(Keys.Enter);
        }

        public void quit()
        {
            driver.Quit();
        }
    }
}
