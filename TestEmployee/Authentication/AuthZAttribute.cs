using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace TestEmployee.Authentication
{
    public class AuthZAttribute:AuthorizeAttribute
    {
        private readonly bool _localReq;
        public AuthZAttribute(bool reqReq)
        {
            _localReq = reqReq;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
            {
                return _localReq;

            }
            else
            {
                return true;
            }
        }
    }
}