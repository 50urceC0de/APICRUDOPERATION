namespace ExceptionHandling{
    using System;
    using System.Web.Mvc;
    using System.Reflection;
    
    public class ExceptionHandlingMvc : HandleErrorAttribete
    {
        private string _ExceptionGeneratedAt;
        
        public string ExceptionGeneratedAt
        {
            get {return _ExceptionGeneratedAt; }
            set {_ExceptionGeneratedAt = value;}
        }
        
        private bool IsAjax(ExceptionContext filterContext){
            return filterContext.HttpContext.Request.Headers["x-Requestes-With"] == "XMLHttpRequest";
        }
        private MethodInfo FindActualMethod(Type controllerType,string actionName){
            MethodInfo objMethod = null;
            MethodInfo[] methodInfo = controllerType.GetMethods();
            foreach(MethodInfo method in methofInfo){
                if(method.Name == actionName){
                    objMethod=method;
                }
            }
            return objMethod;
        }
        private clsException SaveAndGetCustomException(ExceptionContext filterContext){
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            Type controllerType = filterContext.Controller.GetType();
            var method = FindActualMethod(controllerType,actionName);

            Exception ex = filterContext.Exception;
            cslException objException = new clsException();
            objException.ExceptionDescription = objException.GetExceptionDetails(ex);
            objException.StackTraceDesc = ex.StackTrace;
            pbjException.ControllerName = controller;
            objException.ActionName = actionName;
            objException.MethodName = this._ExceptionGeneratedAt;
            objException.SqlQuery = "";
            if(filterContext.HttpContext.Session["SqlQuery"] != null){
                objException.SqlQuery = filterContext.HttpContext.Session["SqlQuery"].ToString();
            }
            objException.BrowserVersion = objException.GetBrowserDetails(filterContext.HttpContext);
            objException.Host_IP = filterContext.HttpContext.Request.UserHostAddress.ToString();
            objException.WebSiteURL = filterContext.HttpContext.Request.Url.ToString();
            return objException;
        }
        public override void OnException(ExceptionContext filterContext){
            if(filterContext.ExceptionHandled){
                return;
            }
            else{
                  if(IsAjax(filterContext)){
                      clsException objException = SaveAndGetCustomException(filterContext);
                      
                      filterContext.Result = new JsonResult()
                      {
                            Data = filterContext.Exception.Message.JsonRequestBehavior = JsonRequestBehavior.AllowGet
                      };
                      filterContext.ExceptionHandled = true;
                      filterContext.HttpContext.Response.Clear();
                  }
                  else
                  {
                      clsException objException = SaveAndGetCustomException(filterContext);
                      filterContext.Result = new ViewResult
                      {
                          ViewName = "Exception",
                          ViewData = new ViewDataDictionary(objException)
                      };
                  }
            }
            base.OnException(fiterContext);
            filterContext.ExceptionHandled = true;
        }
    }
}
