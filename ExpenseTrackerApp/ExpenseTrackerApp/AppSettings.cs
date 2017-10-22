namespace ExpenseTrackerApp
{
    public static class AppSettings
    {
        public const string MOBILE_CENTER_KEY = ""; // TODO get key

        public const int CONNECTION_TIMEOUT = 3000;

        public const string API_TOKEN = "";

        public const string CRYPTO_PASSWORD = "";


        // AVD
        // 10.0.2.2 = localhost - emulator
        // IIS, not iisexpress 

        // Hyper v 
        // http://briannoyesblog.azurewebsites.net/2016/03/06/calling-localhost-web-apis-from-visual-studio-android-emulator/
        // enable firewall rule for port 80
        // http://169.254.80.80  (Desktop Adapter #2 )

        // Hyper-v : http://169.254.80.80/expensetrackerapi/api/  /  Genymotion http://10.0.3.2/   /  AVD http://10.0.2.2/         
        public const string BASE_API_URL = "";


        public static string CategoryEndpoint = $"{BASE_API_URL}/Categories";
        public static string PaymentTypeEndpoint = $"{BASE_API_URL}/PaymentTypes";
        public static string ExpenseEndpoint = $"{BASE_API_URL}/Expenses";

    }
}
