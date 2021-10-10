using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestEmployee.Filters{
    publlic class HttpParamActionAttribute:ActionNameSelectorAttribute{
        public override bool IsValidName(ControllerContext controllerContext,string actionName,MethodInfo methodInfo)
        {
            if(actionName.Equals(methodInfo.Name,StringComparison.InvariantCultureIgnoreCase))
                return true;

            if(!actionName.Equals(actionName,StringComparison.InvariantCultureIgnoreCase))
                return false;

            var request= controllerContext.RequestContext.HttpContext.Request;
            return request[nethodInfo.Name]!=null;
        }
    }
    
    public class CheckSessionAttribute : ActionFilterAttribute{
        public override void OnActionExecuting(ActionExecutionContext filterContext){
            if(filterContext.HttpContext.Session["User"]==null)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(){{"Controller","Home"},{"action","Logout"}});
            base.OnActionExecuting(filterContext);
        }
    }
}
