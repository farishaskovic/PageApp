using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;

namespace PageWebApi.Extensions
{
    public static class OwinContextExtensions
    {
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]

        public static string GetUserId(this IOwinContext ctx)
        {
            var result = "-1";
            var claim = ctx.Authentication.User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (claim != null)
            {
                result = claim.Value;
            }
            return result;
        }
    }
}