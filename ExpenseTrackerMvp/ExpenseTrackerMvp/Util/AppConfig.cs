using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.Util
{
    public class AppConfig
    {
        private static AppConfig instance;

        private AppConfig() { }

        public static AppConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppConfig();
                }
                return instance;
            }
        }


        /// <summary>
        /// Get the firebase api key in the config.xml file
        /// </summary>        
        public string GetFirebaseApiKey()
        {
            System.Type type = this.GetType();
            var resource = type.Namespace + "." + 
                Device.OnPlatform("iOS", "Droid", "WinPhone") + ".config.xml";


            TODO bug aqui

            using (var stream = type.GetTypeInfo().Assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(stream))
            {
                var doc = XDocument.Parse(reader.ReadToEnd());
                return doc.Element("config").Element("firebase-api-key").Value;
            }
        }
    }
}
