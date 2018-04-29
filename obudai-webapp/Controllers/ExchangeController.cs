using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace obudai_webapp.Controllers
{
    public class ExchangeController : ApiController
    {
        static ICurrencies currencyDataModel = CurrenciesInterface.GetInstance();

        // DELETE api/exchange
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public void Delete()
        {
            currencyDataModel.ResetCurrencies();
        }

        // GET api/exchange/{id}
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public object Get(string id)
        {
            return currencyDataModel.GetCurrencyAmount(id.ToUpper());
        }

    }
}
