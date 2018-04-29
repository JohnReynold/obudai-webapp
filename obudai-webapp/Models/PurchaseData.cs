using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using System.Globalization;

namespace obudai_webapp
{
    public class PurchaseData
    {
        static ICurrencies currencyDataModel = CurrenciesInterface.GetInstance();
        public string Symbol { get; set; }
        public double Amount { get; set; }

        public static bool TryParse(string s, out PurchaseData result)
        {
            result = null;

            if(s == null)
            {
                return false;
            }

            Dictionary<string, string> jsonContent;
            try
            {
                jsonContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
            }
            catch (Exception)
            {
                return false;
            }

            if (jsonContent == null
                || !jsonContent.ContainsKey("Symbol")
                || !jsonContent.ContainsKey("Amount"))
            {
                return false;
            }

            double amount;
            // dot vs. comma is annoying
            bool validDouble = double.TryParse(
                jsonContent["Amount"], 
                NumberStyles.Any, 
                CultureInfo.InvariantCulture, 
                out amount
            );
            if (!validDouble)
                return false;

            var currencies = currencyDataModel.GetCurrencyTypes();
            var symbol = jsonContent["Symbol"].ToUpper();
            if (!currencies.Contains(symbol))
                return false;

            result = new PurchaseData() { Symbol = symbol, Amount = amount };
            return true;
        }
    }

    //public class PurchaseDataModelBinder : IModelBinder
    //{
    //    public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
    //    {
    //        PurchaseData result;
    //        if (PurchaseData.TryParse(
    //            actionContext.Request.Content.ReadAsStringAsync().Result, 
    //            out result))
    //        {
    //            bindingContext.Model = result;
    //            return true;
    //        }

    //        bindingContext.ModelState.AddModelError(
    //            bindingContext.ModelName, $"Cannot convert value to {nameof(PurchaseData)}"
    //        );
    //        return false;
    //    }
    //}
}