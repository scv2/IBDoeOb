using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Web;
using IBDoeService.Entity;
using System.ServiceModel.Security;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Diagnostics;
using System.Linq.Expressions;

namespace IBDoeService
{
    public static class ServiceHandler
    {
        public static ServiceResponse Handle(Action func, bool logRequest = true)
        {
            try
            {
                //Start the timer
                Stopwatch st = new Stopwatch();
                st.Start();

                //Performs the operation
                func();

                // informational log to complete request
                if (logRequest)
                {
                    // Logging.Log(GenerateWebServiceInfoMsg(st));
                }

                return new ServiceResponse(ResponseStatus.OK);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(ex);
            }
        }

        public static ServiceResponse Handle(Func<XElement> func, bool logRequest = true)
        {
            try
            {
                ServiceResponse response = new ServiceResponse(ResponseStatus.OK);

                //Start the timer
                Stopwatch st = new Stopwatch();
                st.Start();

                //Performs the operation
                response.Data = func();

                // informational log to complete request
                if (logRequest)
                {
                    // Logging.Log(GenerateWebServiceInfoMsg(st));
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(ex);
            }
        }

        public static string GenerateWebServiceInfoMsg(Stopwatch st)
        {
            return string.Format("Web Service completed in {0} ms: {1}",
                st.ElapsedMilliseconds, System.Web.HttpContext.Current.Request.Url.PathAndQuery);
        }
    }
}
