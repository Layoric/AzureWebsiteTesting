using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using AzureWebsiteTesting.ServiceModel;

namespace AzureWebsiteTesting.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }

        public object Any(WorksOnAzure request)
        {
            var responseStream = new MemoryStream("Test".ToUtf8Bytes());
            var result = new HttpResult(responseStream, "text/html");
            //result.Headers.Add("Content-Length", "4");
            result.StatusCode = HttpStatusCode.Found;
            result.StatusDescription = "Moved Temporarily";
            result.Status = 302;
            result.Location = "/";
            return result;
        }

        public object Any(DoesntWorkOnAzure request)
        {
            var responseStream = new MemoryStream("Test".ToUtf8Bytes());
            var result = new HttpResult(responseStream, "text/html");
            result.Headers.Add("Content-Length", "4");
            result.StatusCode = HttpStatusCode.Found;
            result.StatusDescription = "Moved Temporarily";
            result.Status = 302;
            result.Location = "/";
            return result;
        }
    }


    [Route("/azure/works")]
    public class WorksOnAzure
    {

    }

    [Route("/azure/doesntwork")]
    public class DoesntWorkOnAzure
    {

    }
}
