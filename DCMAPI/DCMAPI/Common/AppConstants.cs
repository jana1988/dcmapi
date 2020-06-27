using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMAPI.Common
{
    public partial class AppConstants
    {
        public const string APPSECRET_KEY = "dCM~MAS#24P~Vision-Cypt";
        public const string DEFAULT_LOGFILENAME = "DefaultLogger";
        public const string INFO_LOGFILENAME = "InfoLogger";
        public const string GST_API_LOGFILENAME = "DCMAPILogger";
        
        public static string GenerateRandomNumber(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        //class for message
        public class ReturnMessage
        {
            public const string SUCCESS_MESSAGE = "Success";
            public const string FAILURE_MESSAGE = "Unable to handle operation.";
            public const string LOGIN_FAILURE_MESSAGE = "Unable to authenticate user.";
            public const string INVALID_DBNAME_MESSAGE = "Invalid DB Name provided.";
            public const string NORECORD_FAILURE_MESSAGE = "No records found.";
            public const string VALIDATION_FAILURE_MESSAGE = "{0}: {1} is required.";
            public const string UNAUTHORIZED_ACCESS = "Unauthorized usage detected.";
            
        }
    }
}
