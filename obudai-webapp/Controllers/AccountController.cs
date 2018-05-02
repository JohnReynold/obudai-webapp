using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace obudai_webapp.Controllers
{
    public class AccountController : ApiController
    {
        static IBalance balanceDataModel = BalanceHandler.GetInstance();
        static ICurrencies currencyDataModel = CurrenciesInterface.GetInstance();
        static ICurrencyRates currencyRateModel = CurrencyRates.GetInstance();

        // POST api/account/{id}
        [SwaggerOperation("Post")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public object Post(string id)
        {
            switch (id)
            {
                case "reset":
                    return balanceDataModel.ResetBalance();
                case "purchase":
                    return this.PurchaseCurrency();
                default:
                    return Request.CreateResponse(HttpStatusCode.NotFound, "not found");
            }
        }
        
        // GET api/account
        [SwaggerOperation("Get")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public double Get()
        {
            return balanceDataModel.GetBalance();
        }

        private object PurchaseCurrency()
        {
            PurchaseData data;
            if (!PurchaseData.TryParse(
                    this.Request.Content.ReadAsStringAsync().Result,
                    out data))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "bad request");
            }
            // USD per unit of CURRENCY
            double rate = currencyRateModel.GetRateFor(data.Symbol);
            double cost = rate * data.Amount;
            balanceDataModel.ChangeBalance(-cost);
            try
            {
                currencyDataModel.ChangeCurrencyAmount(data.Symbol, data.Amount);
            }
            catch (Exception)
            {
                // if it did not succeed, rollback currency amount
                balanceDataModel.ChangeBalance(cost);
                throw;
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
