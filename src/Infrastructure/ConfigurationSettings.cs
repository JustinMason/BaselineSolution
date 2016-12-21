using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Domain.Models;

namespace Infrastructure
{
    ///  <add key="credentials.username" value="test@pearweb.com"/>
    //<add key = "credentials.password" value="304304test!"/>
    //<add key = "address.to" value="support@pearweb.com"/>
    //<add key = "address.from" value="test@pearweb.com"/>
    //<add key = "host.name" value="mail.pearweb.com"/>
    //<add key = "host.port" value="26"/>
    //<add key = "enable.ssl" value="false" />

    public class ConfigurationSettings: IConfigurationSettings
    {
        public string SmtpCredentialsUserName => ConfigurationManager.AppSettings["credentials.username"];

        public string SmtpCredentialsPassword => ConfigurationManager.AppSettings["credentials.password"];

        public string SmtpAddressTo => ConfigurationManager.AppSettings["address.to"];

        public string SmtpAddressFrom => ConfigurationManager.AppSettings["address.from"];

        public string SmtpHostName => ConfigurationManager.AppSettings["host.name"];

        public string SmtpHostPort => ConfigurationManager.AppSettings["host.port"];

        public string SmtpEnableSsl => ConfigurationManager.AppSettings["enable.ssl"];

    }
}
