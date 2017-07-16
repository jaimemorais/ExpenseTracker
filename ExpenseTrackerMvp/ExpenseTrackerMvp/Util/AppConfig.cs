using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace ExpenseTrackerMvp.Util
{
    public class AppConfig
    {

        public static AppConfig Instance { get; } = new AppConfig();
        

        /// <summary>
        /// Get the firebase api key in the config.xml file
        /// </summary>        
        public string GetFirebaseApiKey()
        {
            return GetConfigElement("firebase-api-key");
        }


        public string GetExpenseTrackerApiUrl()
        {
            return GetConfigElement("expensetracker-api-url");
        }

        public string GetExpenseTrackerApiToken()
        {
            return GetConfigElement("expensetracker-api-token");
        }

        public string GetExpenseTrackerCryptoPassword()
        {
            return GetConfigElement("expensetracker-crypto-password");
        }

        public string GetMobileCenterKey()
        {
            return GetConfigElement("mobile-center-key");
        }

        private string GetConfigElement(string element)
        {
            System.Type type = this.GetType();

            //var resource = type.Namespace + "." + Device.OnPlatform("iOS", "Droid", "WinPhone") + ".config.xml";            
            var resource = "ExpenseTrackerMvp.config.xml";

            using (var stream = type.GetTypeInfo().Assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(stream))
            {
                var doc = XDocument.Parse(reader.ReadToEnd());
                return doc.Element("config").Element(element).Value;
            }
        }
    }
}
