using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HSH.Domain
{
    public class Config
    {
        public static string AccountSid => WebConfigurationManager.AppSettings["AccountSid"] ??
                                           "ACc2752ee7a89c8d29a0cb2e646307237c";

        public static string AuthToken => WebConfigurationManager.AppSettings["AuthToken"] ??
                                          "cf7702e30628d76784681b87be3f50e6";

        public static string TwilioNumber => WebConfigurationManager.AppSettings["TwilioNumber"] ??
                                             "+353861802320";
    }
}