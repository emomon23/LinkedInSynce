using System.Collections.Generic;
using LinkInContactManagement.Model;
using LinkInContactManagement.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LinkInContactManagement.LinkedIn
{
    public class LinkedInContactsDriver
    {
        private IWebDriver _driver = new ChromeDriver();
        private int _totalNumberOfConnections = 0;
        private List<Contact> _contacts = new List<Contact>();

        public void Initialize()
        {
            _driver.Navigate().GoToUrl("https://www.linkedin.com/uas/login");

            _driver.SetTextOnElement("session_key-login", "[your linked username]");
            _driver.SetTextOnElement("session_password-login", "[your linked in password]");
            _driver.ClickElement("btn-primary", true);

            _driver.Navigate().GoToUrl("https://www.linkedin.com/mynetwork/invite-connect/connections/");
            GetTotalNumberOfContacts();
            ScrollToTheBottom();
            ReadContactNamesFromPage();
        }

      
        public bool SendMessage(Contact contact,  string body)
        {
            var script = $"$('a[aria-label*=\"Send message to {contact.RawLinkedInName}\"]').click()";
            _driver.ExecuteJavaScript(script);
            _driver.Pause(1);

            if (IsNewMessagePopUpDisplayed())
            {
                _driver.SetTextOnElement("name", "message", "textarea", body);
                _driver.Pause(5);
                _driver.ClickElement("class", "send-button", "button");
                _driver.Pause(5);

                return true;
            }

            return false;
        }

        public void PauseRandom(int minPauseSeconds, int maxPauseSeconds)
        {
            _driver.PauseRandom(minPauseSeconds, maxPauseSeconds);
        }

        private bool IsNewMessagePopUpDisplayed()
        {
            return _driver.DoesElementExistAndIsVisible("class", "modal-content-wrapper", "div");

        }

        public List<Contact> Contacts
        {
            get
            {
                return _contacts;
                
            }
        }

        private int GetCurrentNumberOfContactsSpanElementsDisplayedOnScreen()
        {
            var script = "return $('span[class*=\"mn-person-info\"]').length";
            var number = _driver.ExecuteJavaScript(script).ToInt();
            return number > 0 ? number / 2 : number;
        }

        public void GetTotalNumberOfContacts()
        {
            string script = "return $('h3:contains(\"Connections\")').text()";
            var rawText = _driver.ExecuteJavaScript(script);

            var eachWord = rawText.Split(' ');
            foreach (var word in eachWord)
            {
                if (word.ToInt() > 500)
                {
                    _totalNumberOfConnections = word.ToInt();
                    break;
                }
            }
        }

        private void ScrollToTheBottom()
        {
            var script = "$(window).scrollTop($(document).height())";

            var currentNumberOfContactsDisplayed = GetCurrentNumberOfContactsSpanElementsDisplayedOnScreen();
            var preScrollNumber = 0;
            var retryCount = 0;

            while (true)
            {
                preScrollNumber = currentNumberOfContactsDisplayed;
                _driver.ExecuteJavaScript(script);
                _driver.PauseRandom(1, 3);

                currentNumberOfContactsDisplayed = GetCurrentNumberOfContactsSpanElementsDisplayedOnScreen();
               
                if (preScrollNumber != currentNumberOfContactsDisplayed)
                {
                    retryCount = 0;
                }
                else
                {
                    retryCount += 1;
                }

                if (retryCount > 3)
                {
                    break;
                }
            }
        }

        private void ReadContactNamesFromPage()
        {

            const string baseScript = "return $($('span[class*=\"person\"]')[INDEX]).text()";
            var script = "";

            int duplicateCount = 0;
            List<string> namesSkipped = new List<string>();

            int i = 0;
            var nameString = "";
            int consecutiveEmtpyName = 0;

            while (true)
            {
               
                script = baseScript.Replace("INDEX", i.ToString());
                try
                {
                    nameString = _driver.ExecuteJavaScript(script);
                }
                catch
                {
                    break;
                }

                var contact = BuildContactFromRawNameHtml(nameString);

                if (contact != null)
                {
                    consecutiveEmtpyName = 0;
                    script = baseScript.Replace("INDEX", (i + 1).ToString());
                    contact.Title = _driver.ExecuteJavaScript(script).Replace("\r", "").Replace("\n", "").Trim();
                    var added = _contacts.AddContactIfContactDoesntCurrentlyExistInList(contact);
                }
                else
                {
                    consecutiveEmtpyName += 1;
                    namesSkipped.Add(nameString);
                }

                i += 2;

                if (consecutiveEmtpyName > 4)
                {
                    break;
                }
            }
        }

       
        private Contact BuildContactFromRawNameHtml(string rawNameHtml)
        {
            //Mike Emo
            //Mike Robert Emo -- This one can't really hanlde, will end up "Mike" "Robert"
            //Mike Robert Erikson -- This works, will end up 'Mike Erickson'
            //Mike R Emo
            //Mike R. Emo
            //Mike 'the man' Emo
            //Mike "The Man!" Emo
            //Mike Emo, PHD, CMS, WTF -- Returns Mike Emo
            //Mike Cant really handle this one Emo -- returns null

            if (rawNameHtml.IsNull())
            {
                return null;
            }

            rawNameHtml = rawNameHtml.Replace("\r", "").Replace("\n", "").Trim();

            var nameParts = rawNameHtml.Split(' ');

            if (nameParts.Length < 2)
            {
                return null;
            }

            if (nameParts.Length > 3)
            {
                if (!(nameParts[1].EndsWith(",") || nameParts[2] == "-"))
                {
                    return null;
                }
            }

            int lastNameIndexToUse = 1;

            if (nameParts.Length == 3)
            {
                var np2 = nameParts[1];
                if (np2.Contains("\"") || np2.Contains("'") || np2.Length < 3 || (nameParts[2].Length > 3 && nameParts[2].Contains(",") == false))
                {
                    lastNameIndexToUse = 2;
                }
            }
         
            return new Contact() {FirstName = nameParts[0], LastName = nameParts[lastNameIndexToUse], RawLinkedInName = rawNameHtml};
        }

    }
}
