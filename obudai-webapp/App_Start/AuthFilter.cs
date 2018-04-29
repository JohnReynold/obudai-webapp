using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace obudai_webapp
{
    public class AuthFilter : IActionFilter
    {

        public bool AllowMultiple
        {
            get
            {
                return true;
            }
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> token;
            bool headerExists = actionContext.Request.Headers.TryGetValues(
                "X-Access-Token", out token
            );
            if (!actionContext.Request.IsLocal() // don't ask key if request is from localhost
                && (!headerExists || token.First() != SecretAppData.GetApiKey()))
            {
                return actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized, "unauthorized"
                );
            }
            return await continuation();
        }
    }
}