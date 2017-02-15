using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkInContactManagement.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LinkInContactManagement.Utils
{
    public static class Extensions
    {
        private static Random rnd = new Random(DateTime.Now.Millisecond);
        private static bool _isOnScreenPrimed;

        #region StringExtensions

        public static bool IsNull(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsDate(this string str)
        {
            DateTime d;

            return DateTime.TryParse(str, out d);
        }

        public static bool IsNotNull(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static int ToInt(this string str)
        {
            int result = 0;
            int.TryParse(str.Replace(" ", ""), out result);

            return result;
        }

        public static bool ToBool(this string str)
        {
            if (str.IsNull())
            {
                return false;
            }

            return str.ToLower().Trim() == "true";
        }

        #endregion

        #region SeleniumDriver
        

        public static void SetTextOnElement(this IWebDriver driver, string elementId, string value)
        {
            driver.FindElement(By.Id(elementId)).SendKeys(value);
        }

        public static void SetTextOnElement(this IWebDriver driver, string attributeName, string attributeValue, string elementName, string value)
        {
            string lookFor = $"{elementName}[{attributeName}*='{attributeValue}']";
            driver.FindElement(By.CssSelector(lookFor)).SendKeys(value);
        }

        public static void ClickElement(this IWebDriver driver, string elementId, bool waitForUrlToChange = false)
        {
            var oldUrl = waitForUrlToChange ? driver.Url : null;

            driver.FindElement(By.Id(elementId)).Click();

            if (waitForUrlToChange)
            {
                WaitForUrlToChange(driver, oldUrl);
            }
        }

        public static void ClickElement(this IWebDriver driver, string attributeName, string attrbuteValue, string elementName="", bool waitForUrlToChange = false)
        {
            var oldUrl = waitForUrlToChange ? driver.Url : null;

            string lookFor = $"{elementName}[{attributeName}*='{attrbuteValue}']";
            driver.FindElement(By.CssSelector(lookFor)).Click();

            if (waitForUrlToChange)
            {
                WaitForUrlToChange(driver, oldUrl);
            }
        }

        public static string ExecuteJavaScript(this IWebDriver driver, string script, bool waitForUrlToChange = false)
        {
            var oldUrl = waitForUrlToChange ? driver.Url : "";

            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;

            var result = executor.ExecuteScript(script);

            if (waitForUrlToChange)
            {
                WaitForUrlToChange(driver, oldUrl);
            }
            
            return result == null? null : result.ToString();
        }

        public static void Pause(this IWebDriver driver, double seconds)
        {
            Thread.Sleep((int)(seconds * 1000));
        }

        public static void PauseRandom(this IWebDriver driver, int minSeconds, int maxSeconds)
        {
            var seconds = rnd.Next(minSeconds, maxSeconds);
            Pause(driver, seconds);
        }

        private static void WaitForUrlToChange(IWebDriver driver, string oldUrl)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (watch.Elapsed.TotalSeconds < 90)
            {
                Thread.Sleep(1000);

                if (driver.Url != oldUrl)
                {
                    break;
                }
            }
        }

        public static void HoverOverElement(this IWebDriver driver, string elementId)
        {
            var element = driver.FindElement(By.Id(elementId));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.Pause(.5);
        }

        public static string PutIdOnElement(this IWebDriver driver, string scriptSelector)
        {
            var newId = Guid.NewGuid().ToString().Replace("-", "");
            var script = $"$({scriptSelector}).attr('id', '{newId}');";
            driver.ExecuteJavaScript(script);

            return newId;
        }

        public static bool IsOnScreen(this IWebDriver driver, string elementId)
        {
            PrimeIsOnScreen(driver);
            var script = $"return IsOnScreen($('#{elementId}'));";

            var result = driver.ExecuteJavaScript(script);
            return result.ToBool();
        }

        public static bool DoesElementExistAndIsVisible(this IWebDriver driver, string elementId)
        {
            try
            {
                var element = driver.FindElement(By.Id(elementId));
                return element != null && element.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static bool DoesElementExistAndIsVisible(this IWebDriver driver, string attributeName, string attributeValue, string elementName = "")
        {
            try
            {
                var lookFor = $"{elementName}[{attributeName}*='{attributeValue}']";
                var element = driver.FindElement(By.CssSelector(lookFor));
                return element != null && element.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static bool ScrollToElement(this IWebDriver driver, string elementId)
        {
            var element = driver.FindElement(By.Id("element-id"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();

            return driver.IsOnScreen(elementId);
        }

        private static void PrimeIsOnScreen(IWebDriver driver)
        {
            if (_isOnScreenPrimed)
            {
                return;
            }

            var script = @"IsOnScreen = function (el) 
{
                //special bonus for those using jQuery
                if (typeof jQuery !== 'undefined' && el instanceof jQuery) el = el[0];

                var rect = el.getBoundingClientRect();
                var windowHeight = (window.innerHeight || document.documentElement.clientHeight);
                var windowWidth = (window.innerWidth || document.documentElement.clientWidth);

                return (
                       (rect.left >= 0)
                    && (rect.top >= 0)
                    && ((rect.left + rect.width) <= windowWidth)
                    && ((rect.top + rect.height) <= windowHeight)
                );

            }";

            driver.ExecuteJavaScript(script);
            _isOnScreenPrimed = true;
        }

        #endregion

        #region OtherExtensions

        public static bool AddContactIfContactDoesntCurrentlyExistInList(this List<Contact> contactList, Contact contact)
        {
            if (!contactList.Any(c => c.RawLinkedInName == contact.RawLinkedInName))
            {
                contactList.Add(contact);
                return true;
            }

            return false;
        }


        public static void HideAllColumnsExcpet(this DataGridView dgView, params string[] columnsToShow)
        {
            for (int i = 0; i < dgView.Columns.Count; i++)
            {
                var column = dgView.Columns[i];
                column.Visible = columnsToShow.Any(c => c == column.Name);
            }

            dgView.OrderColumnsHeaders(columnsToShow);
        }

        public static void OrderColumnsHeaders(this DataGridView dgView, params string[] columnNamesInOrder)
        {
            for (int i = 0; i < columnNamesInOrder.Length; i++)
            {
                dgView.Columns[columnNamesInOrder[i]].DisplayIndex = i;
            }
        }

        public static string ToDateString(this DateTime? nullableDateTime, string resultIfNoValueExists = "")
        {
            if (nullableDateTime.HasValue)
            {
                return nullableDateTime.Value.ToString("g");
            }

            return resultIfNoValueExists;
        }

        public static void SetValueFromString(this DateTime? nullableDate, string str)
        {
            if (str.IsDate())
            {
                nullableDate = DateTime.Parse(str);
            }
        }

        public static Color GetHightestUrgencyColor(this List<ToDo> todoList)
        {
            if (todoList.All(t => t.UrgencyColor == Color.Aqua))
            {
                return Color.Aqua;
            }

            if (todoList.Any(t => t.UrgencyColor == Color.DarkRed))
            {
                return Color.DarkRed;
            }

            if (todoList.Any(t => t.UrgencyColor == Color.DarkOrange))
            {
                return Color.DarkOrange;
            }

            return Color.DarkGreen;
        }

        #endregion
    }
}
