using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//this file is mapped to client side Permissions/Config.index.js. Any changes need to be made on both files
namespace Core.Security
{
    public enum AccountType
    {
        None = 0 ,
        Admin = 999
    }

    public enum Permissions
    {
        //Account Inqury
        [Description("CanDoA")]
        CanDoA = 1,
        [Description("CandoB")]
        CanDoB = 2,
    }

}

