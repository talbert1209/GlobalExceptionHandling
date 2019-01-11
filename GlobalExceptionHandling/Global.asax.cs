using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace GlobalExceptionHandling
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {
            // What just happened?
            Exception exception = Server.GetLastError();

            // exception will always be of type HttpUnhandledException.
            // To get the the exception that caused the error to happen
            // you'll need to look at the exception.InnerException.
            var innerException = exception.InnerException;

            // Handle a different type of exception differently
            if (innerException.GetType() == typeof(ArgumentOutOfRangeException))
            {
                Server.Transfer("Error.aspx");
            }

            Response.Write("<h2>Something Bad Happened...</h2>");
            Response.Write($"<p>{innerException.Message}</p>");

            // You must do this if you want to hide the yellow page of death
            // any existing exceptions after this point will send the end user
            // the exception page
            Server.ClearError();
        }
    }
}