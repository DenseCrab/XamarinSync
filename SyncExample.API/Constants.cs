using System;
using System.Collections.Generic;
using System.Text;

namespace SyncExample.API
{
    public static class Constants
    {

        public const string GetUnsyncedMessages = "api/Messages/GetUnsyncedMessages/{0}";
        public const string SaveMessages = "api/Messages/SaveMessages/{0}";
        public const string SaveMessage = "api/Messages/SaveMessage/{0}";
        public const string APIEndpoint = "http://169.254.80.80:57038/";

    }
}
