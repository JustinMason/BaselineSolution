using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Models;
using Core.Security;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;

namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static AccountType  GetAccountType(this IEnumerable<System.Security.Claims.Claim> claims)
        {
            var accountTypeValue = claims
                .Where(x => x.Type == "Core.Security.AccountType")
                .Select(x => x.Value)
                .FirstOrDefault();
            int parsed;
            int.TryParse(accountTypeValue, out parsed);
            return Enum.IsDefined(typeof(AccountType), parsed)
                ? (AccountType) parsed
                : AccountType.None;
        }

        public static string GetValue(this IEnumerable<System.Security.Claims.Claim> claims, string name)
        {
            return claims.Where(c => c.Type == "Core.Security.${name}").Select(c => c.Value).FirstOrDefault();
        }

    }
}
