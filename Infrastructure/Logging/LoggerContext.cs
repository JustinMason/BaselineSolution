using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Serilog.Context;

namespace Infrastructure.Logging
{
    public class LoggerContext
    {

        public static IDisposable DecorateWithUser()
        {
            if (HttpContext.Current != null 
                && HttpContext.Current.User != null 
                && HttpContext.Current.User.Identity != null 
                && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                return LogContext.PushProperty("User", HttpContext.Current.User.Identity.Name);
            return LogContext.PushProperty("User", "No user authenticated.");
        }
    }
}
